#include "toll_fee_calculator/toll_fee_calculator.h"
#include "toll_fee_calculator/vehicle.h"

#include <date/date.h>

#include <gtest/gtest.h>

#include <vector>

using namespace date;
using namespace std::chrono_literals;

struct TimeOfDayAndFee
{
    date::time_of_day<std::chrono::seconds> time_;
    int                                     fee_;
};

class GetTollFeeTest :
    public testing::TestWithParam<std::tuple<date::year_month_day, TimeOfDayAndFee>>
{
}; 

TEST_P(GetTollFeeTest, CorrectCalculation)
{
    const date::year_month_day& date = std::get<0>(GetParam());
    const TimeOfDayAndFee& timeOfDayAndFee = std::get<1>(GetParam());
    const int fee = timeOfDayAndFee.fee_;
    toll_fee_calculator::date_time dateTime{ date, timeOfDayAndFee.time_ };
    std::vector dateTimes{ dateTime };

    ASSERT_EQ(toll_fee_calculator::get_fee(toll_fee_calculator::vehicle::Car, dateTimes), fee);
}

class GetTollFreeTest :
    public testing::TestWithParam<std::tuple<date::year_month_day, TimeOfDayAndFee>> {
};

TEST_P(GetTollFreeTest, CorrectCalculation)
{
    const date::year_month_day& date = std::get<0>(GetParam());
    const TimeOfDayAndFee& timeOfDayAndFee = std::get<1>(GetParam());
    toll_fee_calculator::date_time dateTime{ date, timeOfDayAndFee.time_ };
    std::vector dateTimes{ dateTime };

    ASSERT_EQ(toll_fee_calculator::get_fee(toll_fee_calculator::vehicle::Car, dateTimes), 0);
}

const date::year_month_day dates[] =
{
    2019_y / October / 9
};

const date::year_month_day tollFreeDates[] =
{
    2013_y / January / 1, // New years day
    2013_y / March / 28,  // Easter thursday // See https://sv.wikipedia.org/wiki/P%C3%A5skdagen for calculations
    2013_y / March / 29,  // Easter friday
    2013_y / April / 1,   // Easter saturday (?)
    2013_y / April / 30,  // Day before may first
    2013_y / May / 1,     // May first
    2013_y / May / 8,     // Day before kristi flygare
    2013_y / May / 9,     // Kristi flygare (39 days after easter day)
    2013_y / June / 5,      // Day before national day
    2013_y / June / 6,      // National day
    2013_y / June / 21,     // Day before midsummer day
    2013_y / November / 1,  // Day before halloween
    2013_y / December / 24, // Christmas eve
    2013_y / December / 25, // Christmas day
    2013_y / December / 26, // Two days after christmas
    2013_y / December / 31, // New years eve

    2019_y / January / 1, // New years day
    2019_y / April / 18,  // Easter thursday // See https://sv.wikipedia.org/wiki/P%C3%A5skdagen for calculations
    2019_y / April / 19,  // Easter friday
    2019_y / April / 22,   // Easter saturday (?)
    2019_y / April / 30,  // Day before may first
    2019_y / May / 1,     // May first
    2019_y / May / 29,     // Day before kristi flygare
    2019_y / May / 30,     // Kristi flygare (39 days after easter day)
    2019_y / June / 5,      // Day before national day
    2019_y / June / 6,      // National day
    2019_y / June / 21,     // Day before midsummer day
    2019_y / November / 1,  // Day before halloween
    2019_y / December / 24, // Christmas eve
    2019_y / December / 25, // Christmas day
    2019_y / December / 26, // Two days after christmas
    2019_y / December / 31, // New years eve
};

const TimeOfDayAndFee timeOfDays[] =
{
    { date::make_time( 0h+  0min +  0s ),  0,  },
    { date::make_time( 5h+ 59min + 59s ),  0,  },
    { date::make_time( 6h+  0min +  0s ),  8,  },
    { date::make_time( 6h+ 29min + 59s ),  8,  },
    { date::make_time( 6h+ 30min +  0s ),  13, },
    { date::make_time( 6h+ 59min + 59s ),  13, },
    { date::make_time( 7h+  0min +  0s ),  18, },
    { date::make_time( 7h+ 59min + 59s ),  18, },
    { date::make_time( 8h+  0min +  0s ),  13, },
    { date::make_time( 8h+ 29min + 59s ),  13, },
    { date::make_time( 8h+ 30min +  0s ),  8,  },
    { date::make_time( 14h+ 59min + 59s ), 8,  },
    { date::make_time( 15h+ 0min + 0s ),   13, },
    { date::make_time( 15h+ 29min + 59s ), 13, },
    { date::make_time( 15h+ 30min + 0s ),  18, },
    { date::make_time( 16h+ 59min + 59s ), 18, },
    { date::make_time( 17h+ 0min + 0s ),   13, },
    { date::make_time( 17h+ 59min + 59s ), 13, },
    { date::make_time( 18h+ 0min + 0s ),   8,  },
    { date::make_time( 18h+ 29min + 59s ), 8,  },
    { date::make_time( 18h+ 30min + 0s ),  0,  },
    { date::make_time( 23h+ 59min + 59s ), 0,  },
};

INSTANTIATE_TEST_SUITE_P(Instance,
    GetTollFeeTest,
    testing::Combine(
        testing::ValuesIn(dates),
        testing::ValuesIn(timeOfDays)
    )
);

INSTANTIATE_TEST_SUITE_P(Instance,
    GetTollFreeTest,
    testing::Combine(
        testing::ValuesIn(tollFreeDates),
        testing::ValuesIn(timeOfDays)
    )
);

class GetFee :
    public testing::TestWithParam<TimeOfDayAndFee> {
};

TEST_P(GetFee, Test)
{
    auto&& getValue = [](const int& i) { return i; };

    std::vector<toll_fee_calculator::date_time> passage{ toll_fee_calculator::date_time{ 2019_y / date::September / 12, GetParam().time_ } };
    int hourlyFee = toll_fee_calculator::get_fee(toll_fee_calculator::vehicle::Car, passage);

    ASSERT_EQ(hourlyFee, GetParam().fee_);
}

INSTANTIATE_TEST_SUITE_P(Instance,
    GetFee,
    testing::ValuesIn(timeOfDays)
);

