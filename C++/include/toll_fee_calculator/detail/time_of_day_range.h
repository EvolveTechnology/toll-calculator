#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_TIME_OF_DAY_RANGE_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_TIME_OF_DAY_RANGE_H

#include <date/date.h>

#include <chrono>

namespace toll_fee_calculator::detail
{

    class time_of_day_range
    {
    public:
        constexpr time_of_day_range() noexcept
        {
        }

        constexpr time_of_day_range(date::time_of_day<std::chrono::seconds> begin, date::time_of_day<std::chrono::seconds> end) noexcept
            : begin_(begin)
            , end_(end)
        {
        }

        constexpr bool contains(date::time_of_day<std::chrono::seconds> time_of_day) const
        {
            return begin_.to_duration() <= time_of_day.to_duration() &&
                time_of_day.to_duration() < end_.to_duration();
        }

    private:
        date::time_of_day<std::chrono::seconds> begin_{};
        date::time_of_day<std::chrono::seconds> end_{};
    };

}

#endif
