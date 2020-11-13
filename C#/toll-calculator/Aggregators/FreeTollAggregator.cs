using System;
using System.Collections.Generic;

namespace toll_calculator
{
    public class FreeTollAggregator : ITollFeeAggregator
    {
        public int GetTotalToll(List<DateTime> tollTimeStamp)
        {
            return 0;
        }
    }



}
