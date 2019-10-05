#ifndef TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_IS_TOLL_FREE_DATE_H
#define TOLL_FEE_CALCULATOR_INCLUDE_TOLL_FEE_CALCULATOR_DETAIL_IS_TOLL_FREE_DATE_H

#include <date/date.h>

namespace toll_fee_calculator::detail
{

    inline bool is_weekend(const date::year_month_day& date)
    {
        date::weekday weekday{ date };

        return weekday == date::Saturday ||
            weekday == date::Sunday;
    }

    inline bool is_fixed_date_holiday(const date::year_month_day& date)
    {
        using namespace date::literals;

        return (date == date.year() / jan / 1) ||   // New years day
            (date == date.year() / jan / 5)    ||   // Day before Epiphany
            (date == date.year() / jan / 6)    ||   // Epiphany
            (date == date.year() / apr / 30)   ||   // Day before International Worker's Day
            (date == date.year() / may / 1)    ||   // International Worker's Day
            (date == date.year() / jun / 5)    ||   // Day before National Day of Sweden
            (date == date.year() / jun / 6)    ||   // National Day of Sweden
            (date.month() == date::July)       ||   // Month of July
            (date == date.year() / dec / 24)   ||   // Christmas Eve
            (date == date.year() / dec / 25)   ||   // Christmas Day
            (date == date.year() / dec / 26)   ||   // Boxing Day
            (date == date.year() / dec / 31);       // New Years Eve
    }

    inline date::year_month_day get_easter_day(const date::year& year)
    {
        // Source: https://en.wikipedia.org/wiki/Computus

        const int a = static_cast<int>(year) % 19;
        const int b = static_cast<int>(year) % 4;
        const int c = static_cast<int>(year) % 7;

        // Note: Probably not valid for negative years
        const int k = static_cast<int>(year) / 100;
        const int p = (13 + 8 * k) / 25;
        const int q = k / 4;

        const int M = (15 - p + k - q) % 30;
        const int N = (4 + k - q) % 7;

        const int d = (19 * a + M) % 30;
        const int e = (2 * b + 4 * c + 6 * d + N) % 7;

        if (d + e > 9)
        {
            const int day = d + e - 9;

            if (day == 25)
            {
                if (d == 28 && e == 6 && (11 * M + 11) % 30 < 19)
                {
                    return year / date::April / 18;
                }
            }
            else if (day == 26)
            {
                if (d == 29 && e == 6)
                {
                    return year / date::April / 19;
                }
            }

            return year / date::April / (d + e - 9);
        }

        return year / date::March / (22 + d + e);
    }

    inline bool is_easter_date(const date::year_month_day& easterDay, const date::year_month_day& date)
    {
        return (date == date::sys_days{ easterDay } - date::days{ 3 }) || // Maundy Thursday
            (date == date::sys_days{ easterDay } -date::days{ 2 }) ||    // Good Friday
            (date == date::sys_days{ easterDay } +date::days{ 1 });      // Easter Monday
    }

    inline bool is_ascension_date(const date::year_month_day& easterDay, const date::year_month_day& date)
    {
        // Ascension Day is the 6th thursday after Easter Day

        // Easter Day is always a Sunday
        // Advance 5 weeks plus 4 days (Sunday -> Thursday)
        date::year_month_day ascensionDay = date::sys_days{ easterDay } +date::weeks{ 5 } +date::days{ 4 };

        return (date == ascensionDay) ||
            (date == date::sys_days{ ascensionDay } -date::days{ 1 });
    }

    inline bool is_easter_or_ascension_date(const date::year_month_day& date)
    {
        // As an optimization, calculate the year's Easter day once, and use it
        // to check both Easter dates and Ascension dates

        date::year_month_day easterDay = get_easter_day(date.year());

        return is_easter_date(easterDay, date) || is_ascension_date(easterDay, date);
    }

    inline bool is_midsummers_eve(const date::year_month_day& date)
    {
        // Midsummers Eve is the Friday between June 19 and June 25
        // Start with June 19th, then advance with the number of days needed to reach Friday

        date::year_month_day midsummersEve =
            date::sys_days{ date.year() / date::June / 19 } +
            (date::Friday - date::weekday{ date.year() / date::June / 19 });

        return date == midsummersEve;
    }

    inline bool is_all_saints_day(const date::year_month_day& date)
    {
        // All Saints' Day is the Friday between October 30 and November 5
        // Start with October 30th, then advance with the number of days needed to reach Friday

        date::year_month_day allSaintsDay =
            date::sys_days{ date.year() / date::October / 30 } +
            (date::Friday - date::weekday{ date.year() / date::October / 30 });

        return date == allSaintsDay;
    }

    inline bool is_variable_date_holiday(const date::year_month_day& date)
    {
        return is_easter_or_ascension_date(date) ||
            is_midsummers_eve(date) ||
            is_all_saints_day(date);
    }

    inline bool is_toll_free_date(const date::year_month_day& date)
    {
        return is_weekend(date) ||
            is_fixed_date_holiday(date) ||
            is_variable_date_holiday(date);
    }

}

#endif
