using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{

    public class FeeRangeCollection
    {
        public static List<TimespanFeeRange> Range => new List<TimespanFeeRange> //can get from a db or external webservice
        {
            new TimespanFeeRange(new TimeSpan(06, 00, 00), new TimeSpan(06, 29, 59), 8), // if (line 1)
            new TimespanFeeRange(new TimeSpan(06, 30, 00), new TimeSpan(06, 59, 59), 13), // if (line 2)
            new TimespanFeeRange(new TimeSpan(07, 00, 00), new TimeSpan(07, 59, 59), 18), // if (line 3)
            new TimespanFeeRange(new TimeSpan(08, 00, 00), new TimeSpan(08, 29, 59), 13), // if (line 4)
            new TimespanFeeRange(new TimeSpan(08, 30, 00), new TimeSpan(08, 59, 59), 8), // if (line 5)
            new TimespanFeeRange(new TimeSpan(09, 30, 00), new TimeSpan(09, 59, 59), 8), // if (line 5)
            new TimespanFeeRange(new TimeSpan(10, 30, 00), new TimeSpan(10, 59, 59), 8), // if (line 5)
            new TimespanFeeRange(new TimeSpan(11, 30, 00), new TimeSpan(11, 59, 59), 8), // if (line 5)
            new TimespanFeeRange(new TimeSpan(12, 30, 00), new TimeSpan(12, 59, 59), 8), // if (line 5)
            new TimespanFeeRange(new TimeSpan(13, 30, 00), new TimeSpan(13, 59, 59), 8), // if (line 5)
            new TimespanFeeRange(new TimeSpan(14, 30, 00), new TimeSpan(14, 59, 59), 8), // if (line 5)
            new TimespanFeeRange(new TimeSpan(15, 00, 00), new TimeSpan(15, 29, 59), 13), // if (line 6)
            new TimespanFeeRange(new TimeSpan(15, 29, 59), new TimeSpan(15, 59, 59), 18), // if (line 7)
            new TimespanFeeRange(new TimeSpan(16, 00, 00), new TimeSpan(16, 59, 59), 18), // if (line 7)
            new TimespanFeeRange(new TimeSpan(17, 00, 00), new TimeSpan(17, 59, 59), 13), // if (line 8)
            new TimespanFeeRange(new TimeSpan(18, 00, 00), new TimeSpan(18, 29, 59), 8), // if (line 9)
        };
    }
}
