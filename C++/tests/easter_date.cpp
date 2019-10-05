#include "toll_fee_calculator/detail/is_toll_free_date.h"

#include <gtest/gtest.h>

using namespace date::literals;

class EasterDate :
    public testing::TestWithParam<date::year_month_day>
{
};

TEST_P(EasterDate, EasterDay)
{
    ASSERT_TRUE(toll_fee_calculator::detail::get_easter_day(GetParam().year()) == GetParam());
}

TEST_P(EasterDate, MaundyThursday)
{
    date::year_month_day easterDay = toll_fee_calculator::detail::get_easter_day(GetParam().year());
    date::year_month_day maundyThursday = date::sys_days{ easterDay } -date::days{ 3 };

    ASSERT_TRUE(toll_fee_calculator::detail::is_easter_date(easterDay, maundyThursday));
}

TEST_P(EasterDate, GoodFriday)
{
    date::year_month_day easterDay = toll_fee_calculator::detail::get_easter_day(GetParam().year());
    date::year_month_day goodFriday = date::sys_days{ easterDay } -date::days{ 2 };

    ASSERT_TRUE(toll_fee_calculator::detail::is_easter_date(easterDay, goodFriday));
}

TEST_P(EasterDate, EasterMonday)
{
    date::year_month_day easterDay = toll_fee_calculator::detail::get_easter_day(GetParam().year());
    date::year_month_day easterMonday = date::sys_days{ easterDay } + date::days{ 1 };

    ASSERT_TRUE(toll_fee_calculator::detail::is_easter_date(easterDay, easterMonday));
}

INSTANTIATE_TEST_SUITE_P(Instance,
    EasterDate,
    testing::Values(
        2000_y / date::April / 23,
        2001_y / date::April / 15,
        2002_y / date::March / 31,
        2003_y / date::April / 20,
        2004_y / date::April / 11,
        2005_y / date::March / 27,
        2006_y / date::April / 16,
        2007_y / date::April / 8,
        2008_y / date::March / 23,
        2009_y / date::April / 12,
        2010_y / date::April / 4,
        2011_y / date::April / 24,
        2012_y / date::April / 8,
        2013_y / date::March / 31,
        2014_y / date::April / 20,
        2015_y / date::April / 5,
        2016_y / date::March / 27,
        2017_y / date::April / 16,
        2018_y / date::April / 1,
        2019_y / date::April / 21,
        2020_y / date::April / 12,
        2021_y / date::April / 4,
        2022_y / date::April / 17,
        2023_y / date::April / 9,
        2024_y / date::March / 31,
        2025_y / date::April / 20,
        2026_y / date::April / 5,
        2027_y / date::March / 28,
        2028_y / date::April / 16,
        2029_y / date::April / 1,
        2030_y / date::April / 21,
        2031_y / date::April / 13,
        2032_y / date::March / 28,
        2033_y / date::April / 17,
        2034_y / date::April / 9,
        2035_y / date::March / 25,
        2036_y / date::April / 13,
        2037_y / date::April / 5,
        2038_y / date::April / 25,
        2039_y / date::April / 10,
        2040_y / date::April / 1,
        2041_y / date::April / 21,
        2042_y / date::April / 6,
        2043_y / date::March / 29,
        2044_y / date::April / 17,
        2045_y / date::April / 9,
        2046_y / date::March / 25,
        2047_y / date::April / 14,
        2048_y / date::April / 5,
        2049_y / date::April / 18,
        2050_y / date::April / 10,
        2051_y / date::April / 2,
        2052_y / date::April / 21,
        2053_y / date::April / 6,
        2054_y / date::March / 29,
        2055_y / date::April / 18,
        2056_y / date::April / 2,
        2057_y / date::April / 22,
        2058_y / date::April / 14,
        2059_y / date::March / 30,
        2060_y / date::April / 18,
        2061_y / date::April / 10,
        2062_y / date::March / 26,
        2063_y / date::April / 15,
        2064_y / date::April / 6,
        2065_y / date::March / 29,
        2066_y / date::April / 11,
        2067_y / date::April / 3,
        2068_y / date::April / 22,
        2069_y / date::April / 14,
        2070_y / date::March / 30,
        2071_y / date::April / 19,
        2072_y / date::April / 10,
        2073_y / date::March / 26,
        2074_y / date::April / 15,
        2075_y / date::April / 7,
        2076_y / date::April / 19,
        2077_y / date::April / 11,
        2078_y / date::April / 3,
        2079_y / date::April / 23,
        2080_y / date::April / 7,
        2081_y / date::March / 30,
        2082_y / date::April / 19,
        2083_y / date::April / 4,
        2084_y / date::March / 26,
        2085_y / date::April / 15,
        2086_y / date::March / 31,
        2087_y / date::April / 20,
        2088_y / date::April / 11,
        2089_y / date::April / 3,
        2090_y / date::April / 16,
        2091_y / date::April / 8,
        2092_y / date::March / 30,
        2093_y / date::April / 12,
        2094_y / date::April / 4,
        2095_y / date::April / 24,
        2096_y / date::April / 15,
        2097_y / date::March / 31,
        2098_y / date::April / 20,
        2099_y / date::April / 12
    ));
