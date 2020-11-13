using System;
using System.Collections.Generic;

namespace toll_calculator
{
    public interface ITollFeeAggregator
    {
        public int GetTotalToll(List<DateTime> tollTimeStamp);
    }



}
