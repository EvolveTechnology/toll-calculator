#include "toll_fee_calculator/detail/get_time_of_day_fee.h"

#include <gtest/gtest.h>

using namespace date;
using namespace std::chrono_literals;

struct time_of_day_to_fee_mapping
{
    date::time_of_day<std::chrono::seconds> time_;
    int                                     fee_;
};

class get_time_of_day_fee_test :
    public testing::TestWithParam<time_of_day_to_fee_mapping>
{
};

TEST_P(get_time_of_day_fee_test, Test)
{
    ASSERT_EQ(toll_fee_calculator::detail::get_time_of_day_fee(GetParam().time_), GetParam().fee_);
}

const time_of_day_to_fee_mapping timeOfDays[] =
{
    { date::make_time(0h + 0min + 0s),  0,  },
    { date::make_time(5h + 59min + 59s),  0,  },
    { date::make_time(6h + 0min + 0s),  8,  },
    { date::make_time(6h + 29min + 59s),  8,  },
    { date::make_time(6h + 30min + 0s),  13, },
    { date::make_time(6h + 59min + 59s),  13, },
    { date::make_time(7h + 0min + 0s),  18, },
    { date::make_time(7h + 59min + 59s),  18, },
    { date::make_time(8h + 0min + 0s),  13, },
    { date::make_time(8h + 29min + 59s),  13, },
    { date::make_time(8h + 30min + 0s),  8,  },
    { date::make_time(14h + 59min + 59s), 8,  },
    { date::make_time(15h + 0min + 0s),   13, },
    { date::make_time(15h + 29min + 59s), 13, },
    { date::make_time(15h + 30min + 0s),  18, },
    { date::make_time(16h + 59min + 59s), 18, },
    { date::make_time(17h + 0min + 0s),   13, },
    { date::make_time(17h + 59min + 59s), 13, },
    { date::make_time(18h + 0min + 0s),   8,  },
    { date::make_time(18h + 29min + 59s), 8,  },
    { date::make_time(18h + 30min + 0s),  0,  },
    { date::make_time(23h + 59min + 59s), 0,  },
};

INSTANTIATE_TEST_SUITE_P(Instance,
    get_time_of_day_fee_test,
    testing::ValuesIn(timeOfDays)
);
