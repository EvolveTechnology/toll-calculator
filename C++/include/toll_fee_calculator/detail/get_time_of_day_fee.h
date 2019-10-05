#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_GET_TIME_OF_DAY_FEE_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_GET_TIME_OF_DAY_FEE_H

#include "time_of_day_range.h"

#include <date/date.h>

#include <range/v3/algorithm/find_if.hpp>
#include <range/v3/view/transform.hpp>

#include <array>
#include <chrono>

namespace toll_fee_calculator::detail
{
    using namespace std::chrono_literals;

    struct time_of_day_range_to_fee_mapping
    {
        time_of_day_range range;
        int               fee;
    };

    inline constexpr std::array<time_of_day_range_to_fee_mapping, 5> time_of_day_range_to_fee = {
        time_of_day_range_to_fee_mapping{ time_of_day_range{ date::time_of_day<std::chrono::seconds>{  7h         }, date::time_of_day<std::chrono::seconds>{  8h         }}, 18 },
        time_of_day_range_to_fee_mapping{ time_of_day_range{ date::time_of_day<std::chrono::seconds>{ 15h + 30min }, date::time_of_day<std::chrono::seconds>{ 17h         }}, 18 },
        time_of_day_range_to_fee_mapping{ time_of_day_range{ date::time_of_day<std::chrono::seconds>{  6h + 30min }, date::time_of_day<std::chrono::seconds>{  8h + 30min }}, 13 },
        time_of_day_range_to_fee_mapping{ time_of_day_range{ date::time_of_day<std::chrono::seconds>{ 15h         }, date::time_of_day<std::chrono::seconds>{ 18h         }}, 13 },
        time_of_day_range_to_fee_mapping{ time_of_day_range{ date::time_of_day<std::chrono::seconds>{  6h         }, date::time_of_day<std::chrono::seconds>{ 18h + 30min }},  8 },
    };

    inline int get_time_of_day_fee(date::time_of_day<std::chrono::seconds> time_of_day)
    {
        auto tod_range = time_of_day_range_to_fee
            | ranges::views::transform([](const time_of_day_range_to_fee_mapping& mapping) { return mapping.range; });

        auto it = ranges::find_if(tod_range, [time_of_day](const time_of_day_range& rng) { return rng.contains(time_of_day); });

        return it != tod_range.end() ? it.base()->fee : 0;
    }

}

#endif
