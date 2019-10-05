#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_TOLL_FEE_CALCULATOR_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_TOLL_FEE_CALCULATOR_H

#include "date_time.h"
#include "vehicle.h"

#include "detail/get_daily_fee.h"
#include "detail/is_toll_free_date.h"

#include <range/v3/numeric/accumulate.hpp>
#include <range/v3/view/group_by.hpp>
#include <range/v3/view/filter.hpp>

#include <functional>

namespace toll_fee_calculator
{
    template <typename Rng>
    int get_fee(vehicle the_vehicle, const Rng& rng)
    {
        if (is_toll_free_vehicle(the_vehicle))
        {
            return 0;
        }

        // rng consists of all passages for the given vehicle,
        // the total fee for that vehicle is the sum of fees for each day.
        auto on_same_day = [](const date_time& lhs, const date_time& rhs) {
            return lhs.date() == rhs.date();
        };

        auto is_not_toll_free_date = [](const auto& rng) {
            return !detail::is_toll_free_date(rng.begin()->date());
        };

        auto passagesGroupedPerDay = rng
            | ranges::views::group_by(on_same_day)
            | ranges::views::filter(is_not_toll_free_date);

        return ranges::accumulate(
            passagesGroupedPerDay,
            0,
            std::plus{},
            detail::get_daily_fee{});
    }
}

#endif
