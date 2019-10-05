#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_GET_HOURLY_FEE_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_GET_HOURLY_FEE_H

#include "get_time_of_day_fee.h"

#include <range/v3/numeric/accumulate.hpp>

#include <algorithm>

namespace toll_fee_calculator::detail
{
    struct get_hourly_fee
    {
        template <typename Rng>
        int operator()(const Rng& rng)
        {
            assert(!rng.empty());

            // rng consists of all passages within one hour, the total fee for that hour is
            // the highest fee for a single passage within that hour
            return ranges::accumulate(
                rng,
                0,
                [](int lhs, int rhs) { return std::max(lhs, rhs); },
                get_time_of_day_fee);
        }
    };
}

#endif
