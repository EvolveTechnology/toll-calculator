#include "toll_fee_calculator/detail/is_toll_free_date.h"

#include <gtest/gtest.h>

using namespace date;

TEST(IsTollFreeDate, Monday)
{
    date::year_month_day monday = 2019_y / September / 9;

    ASSERT_FALSE(toll_fee_calculator::detail::is_toll_free_date(monday));
}

TEST(IsTollFreeDate, Tuesday)
{
    date::year_month_day tuesday = 2019_y / September / 10;

    ASSERT_FALSE(toll_fee_calculator::detail::is_toll_free_date(tuesday));
}

TEST(IsTollFreeDate, Wednesday)
{
    date::year_month_day wednesday = 2019_y / September / 11;

    ASSERT_FALSE(toll_fee_calculator::detail::is_toll_free_date(wednesday));
}

TEST(IsTollFreeDate, Thursday)
{
    date::year_month_day thursday = 2019_y / September / 12;

    ASSERT_FALSE(toll_fee_calculator::detail::is_toll_free_date(thursday));
}

TEST(IsTollFreeDate, Friday)
{
    date::year_month_day friday = 2019_y / September / 13;

    ASSERT_FALSE(toll_fee_calculator::detail::is_toll_free_date(friday));
}

TEST(IsTollFreeDate, Saturday)
{
    date::year_month_day saturday = 2019_y / September / 14;

    ASSERT_TRUE(toll_fee_calculator::detail::is_toll_free_date(saturday));
}

TEST(IsTollFreeDate, Sunday)
{
    date::year_month_day sunday = 2019_y / September / 15;

    ASSERT_TRUE(toll_fee_calculator::detail::is_toll_free_date(sunday));
}

class TollFreeDate :
    public testing::TestWithParam<std::tuple<date::year, std::tuple<date::month, date::day>>>
{
};

TEST_P(TollFreeDate, Test)
{
    date::year_month_day day{ std::get<0>(GetParam()), std::get<0>(std::get<1>(GetParam())), std::get<1>(std::get<1>(GetParam())) };

    ASSERT_TRUE(toll_fee_calculator::detail::is_toll_free_date(day));
}

INSTANTIATE_TEST_SUITE_P(FixedDate,
    TollFreeDate,
    testing::Combine(
        testing::Values(
            2011_y,
            2012_y,
            2013_y,
            2014_y,
            2015_y,
            2016_y,
            2017_y,
            2018_y,
            2019_y,
            2020_y),
        testing::Values(
            std::tuple{ January, 1 },
            std::tuple{ January, 5 },
            std::tuple{ January, 6 },
            std::tuple{ April, 30 },
            std::tuple{ May, 1 },
            std::tuple{ June, 5 },
            std::tuple{ June, 6 },
            std::tuple{ December, 24 },
            std::tuple{ December, 25 },
            std::tuple{ December, 26 },
            std::tuple{ December, 31 })));

INSTANTIATE_TEST_SUITE_P(AllDates2019,
    TollFreeDate,
    testing::Combine(
        testing::Values(
            2019_y),
        testing::Values(
            std::tuple{ January, 1 },
            std::tuple{ April, 18 },
            std::tuple{ April, 19 },
            std::tuple{ April, 22 },
            std::tuple{ April, 30 },
            std::tuple{ May, 1 },
            std::tuple{ May, 29 },
            std::tuple{ May, 30 },
            std::tuple{ June, 5 },
            std::tuple{ June, 6 },
            std::tuple{ June, 21 },
            std::tuple{ December, 24 },
            std::tuple{ December, 25 },
            std::tuple{ December, 26 },
            std::tuple{ December, 31 })));

INSTANTIATE_TEST_SUITE_P(AllDates2017,
    TollFreeDate,
    testing::Combine(
        testing::Values(
            2017_y),
        testing::Values(
            std::tuple{ January, 5 },
            std::tuple{ January, 6 },
            std::tuple{ April, 13 },
            std::tuple{ April, 14 },
            std::tuple{ April, 17 },
            std::tuple{ May, 1 },
            std::tuple{ May, 24 },
            std::tuple{ May, 25 },
            std::tuple{ June, 5 },
            std::tuple{ June, 6 },
            std::tuple{ June, 23 },
            std::tuple{ November, 3 },
            std::tuple{ December, 25 },
            std::tuple{ December, 26 })));

TEST(IsTollFreeDate, MonthOfJuly)
{
    for (int day = 1; day <= 31; ++day)
    {
        date::year_month_day dayInJuly = 2019_y / July / day;

        ASSERT_TRUE(toll_fee_calculator::detail::is_toll_free_date(dayInJuly));
    }
}

TEST(IsTollFreeDate, MaundyThursday)
{
    date::year_month_day maundyThursday = 2019_y / April / 18;

    ASSERT_TRUE(toll_fee_calculator::detail::is_toll_free_date(maundyThursday));
}

