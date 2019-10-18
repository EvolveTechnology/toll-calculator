using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
    public class FeeIntervalRepository
    {
        public IEnumerable<(TimeSpan start, TimeSpan end, int fee)> GetIntervals()
        {
            return new []
            {
                (new TimeSpan(6, 0, 0), new TimeSpan(6, 30, 0), 8),
                (new TimeSpan(6, 30, 0), new TimeSpan(7, 0, 0), 13),
                (new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0), 18),
                (new TimeSpan(8, 0, 0), new TimeSpan(8, 30, 0), 13),
                (new TimeSpan(8, 30, 0), new TimeSpan(15, 0, 0), 8),
                (new TimeSpan(15, 0, 0), new TimeSpan(15, 30, 0), 13),
                (new TimeSpan(15, 30, 0), new TimeSpan(17, 0, 0), 18),
                (new TimeSpan(17, 0, 0), new TimeSpan(18, 0, 0), 13),
                (new TimeSpan(18, 0, 0), new TimeSpan(18, 30, 0), 8)
            };
        }
    }
}
