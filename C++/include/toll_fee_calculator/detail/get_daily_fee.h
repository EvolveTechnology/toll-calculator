#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_GET_DAILY_FEE_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_GET_DAILY_FEE_H

#include "get_hourly_fee.h"
#include "get_time_of_day_fee.h"

#include "../date_time.h"

#include <date/date.h>

#include <range/v3/numeric/accumulate.hpp>
#include <range/v3/view/drop_while.hpp>
#include <range/v3/view/group_by.hpp>
#include <range/v3/view/take_while.hpp>
#include <range/v3/view/transform.hpp>

#include <algorithm>
#include <chrono>
#include <functional>

namespace toll_fee_calculator::detail
{
    struct get_daily_fee
    {
        template <typename Rng>
        int operator()(const Rng& rng)
        {
            assert(!rng.empty());

            // rng consist of all passages within a single day, the total fee for that day is
            // the sum of fees for each started hour, capped at 60 SEK.

            auto to_time = [](const date_time& dt)
            {
                return dt.time();
            };

            auto fee_is_zero = [](const date::time_of_day<std::chrono::seconds>& time) {
                return get_time_of_day_fee(time) == 0;
            };

            auto within_one_hour = [](const date::time_of_day<std::chrono::seconds>& lhs, const date::time_of_day<std::chrono::seconds>& rhs) {
                return rhs.to_duration() - lhs.to_duration() < 1h;
            };

            auto first_passage_fee_greater_than_zero = [](const auto& rng) {
                return get_time_of_day_fee(*rng.begin()) > 0;
            };

            auto passagesWithFeeGroupedByHour = rng
                | ranges::views::transform(to_time)
                | ranges::views::drop_while(fee_is_zero)
                | ranges::views::group_by(within_one_hour)
                | ranges::views::take_while(first_passage_fee_greater_than_zero);

            return std::min(
                ranges::accumulate(
                    passagesWithFeeGroupedByHour,
                    0,
                    std::plus{},
                    get_hourly_fee{}),
                60);
        }
    };
}

#endif
