#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DATE_TIME_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DATE_TIME_H

#include <date/date.h>

#include <chrono>

namespace toll_fee_calculator
{
    struct date_time {
        date_time(const date::year_month_day& date, const date::time_of_day<std::chrono::seconds>& time)
            : date_(date)
            , time_(time)
        {
            if (!date.ok())
            {
                throw std::runtime_error("Invalid date");
            }
        }

        date::year_month_day date() const
        {
            return date_;
        }

        date::time_of_day<std::chrono::seconds> time() const
        {
            return time_;
        }

    private:
        const date::year_month_day date_{};
        const date::time_of_day<std::chrono::seconds> time_{};
    };
}

#endif
