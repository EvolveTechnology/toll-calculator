#include "toll_fee_calculator/detail/get_hourly_fee.h"

#include <gtest/gtest.h>

#include <vector>

using namespace date;
using namespace std::chrono_literals;

struct time_of_day_to_fee_mapping
{
    std::vector<date::time_of_day<std::chrono::seconds>> rng_;
    int                                                  fee_;
};

class get_hourly_fee_test :
    public testing::TestWithParam<time_of_day_to_fee_mapping>
{
};

TEST_P(get_hourly_fee_test, Test)
{
    ASSERT_EQ(toll_fee_calculator::detail::get_hourly_fee{}(GetParam().rng_), GetParam().fee_);
}

const time_of_day_to_fee_mapping timeOfDays[] =
{
    { { date::time_of_day<std::chrono::seconds>(6h), date::time_of_day<std::chrono::seconds>(6h + 59min + 59s) }, 13 },
    { { date::time_of_day<std::chrono::seconds>(6h + 30min), date::time_of_day<std::chrono::seconds>(7h + 29min + 59s) }, 18 },
    { { date::time_of_day<std::chrono::seconds>(7h), date::time_of_day<std::chrono::seconds>(7h + 59min + 59s) }, 18 },
    { { date::time_of_day<std::chrono::seconds>(8h), date::time_of_day<std::chrono::seconds>(8h + 59min + 59s) }, 13 },
    { { date::time_of_day<std::chrono::seconds>(8h + 30min), date::time_of_day<std::chrono::seconds>(9h + 29min + 59s) }, 8 },
    { { date::time_of_day<std::chrono::seconds>(15h), date::time_of_day<std::chrono::seconds>(15h + 59min + 59s) }, 18 },
    { { date::time_of_day<std::chrono::seconds>(15h + 30min), date::time_of_day<std::chrono::seconds>(16h + 29min + 59s) }, 18 },
    { { date::time_of_day<std::chrono::seconds>(17h), date::time_of_day<std::chrono::seconds>(17h + 59min + 59s) }, 13 },
    { { date::time_of_day<std::chrono::seconds>(18h), date::time_of_day<std::chrono::seconds>(18h + 59min + 59s) }, 8 },
};

INSTANTIATE_TEST_SUITE_P(Instance,
    get_hourly_fee_test,
    testing::ValuesIn(timeOfDays)
);
