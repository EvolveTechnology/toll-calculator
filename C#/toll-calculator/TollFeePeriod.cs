using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace toll_calculator
{
    public class TollFeePeriod : ITollFeePeriod
    {
        private readonly ITimeTable _timeTable;

        public TollFeePeriod(ITimeTable timeTable)
        {
            _timeTable = timeTable;
        }
        public int GetHighestFeeInPeriod(IEnumerable<DateTime> dates)
        {
            return dates.Select(date => _timeTable.GetFeeAtTimeStamp(date)).OrderByDescending(x => x).First();
        }
    }



}
