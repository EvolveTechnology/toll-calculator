#include "toll_fee_calculator/detail/get_daily_fee.h"

#include "toll_fee_calculator/date_time.h"

#include <gtest/gtest.h>

#include <vector>

using namespace date;
using namespace std::chrono_literals;

struct time_of_day_to_fee_mapping
{
    std::vector<toll_fee_calculator::date_time> rng_;
    int                                         fee_;
};

class get_daily_fee_test :
    public testing::TestWithParam<time_of_day_to_fee_mapping>
{
};

TEST_P(get_daily_fee_test, Test)
{
    ASSERT_EQ(toll_fee_calculator::detail::get_daily_fee{}(GetParam().rng_), GetParam().fee_);
}

const time_of_day_to_fee_mapping timeOfDays[] =
{
    // Two passages within same hour gets charged once
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(6h)          }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(6h + 59min + 59s)  } }, 13 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(6h + 30min)  }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(7h + 29min + 59s)  } }, 18 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(7h)          }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(7h + 59min + 59s)  } }, 18 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h)          }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h + 59min + 59s)  } }, 13 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h + 30min)  }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(9h + 29min + 59s)  } }, 8 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(15h)         }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(15h + 59min + 59s) } }, 18 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(15h + 30min) }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(16h + 29min + 59s) } }, 18 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(17h)         }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(17h + 59min + 59s) } }, 13 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(18h)         }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(18h + 59min + 59s) } }, 8 },

    // Two passages not within same hour gets charged twice
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(6h)          }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(7h + 59min + 59s)  } }, 26 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(6h + 30min)  }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h + 29min + 59s)  } }, 26 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(7h)          }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h + 59min + 59s)  } }, 26 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h)          }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(9h + 59min + 59s)  } }, 21 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h + 30min)  }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(10h + 29min + 59s)  } }, 16 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(15h)         }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(16h + 59min + 59s) } }, 31 },
    { { { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(15h + 30min) }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(17h + 29min + 59s) } }, 31 },

    // Many passages over an entire day gets capped at 60 SEK
    {
        {
            { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(6h)  }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(6h + 59min + 59s)  }, // 13
            { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(7h)  }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(7h + 59min + 59s)  }, // 18
            { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h)  }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(8h + 59min + 59s)  }, // 13
            { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(15h) }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(15h + 59min + 59s) }, // 18
            { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(17h) }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(17h + 59min + 59s) }, // 18
            { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(18h) }, { 2019_y / 10 / 5, date::time_of_day<std::chrono::seconds>(18h + 59min + 59s) }, // 8
        },
        60
    }
};

INSTANTIATE_TEST_SUITE_P(Instance,
    get_daily_fee_test,
    testing::ValuesIn(timeOfDays)
);
