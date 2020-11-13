using System;
using System.Collections.Generic;

namespace toll_calculator
{
    public interface ITollFeePeriod
    {
        public int GetHighestFeeInPeriod(IEnumerable<DateTime> dates);
    }
}
