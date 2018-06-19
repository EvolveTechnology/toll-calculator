using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{
    public class TollFeeLookup
    {
        private static readonly Dictionary<Tuple<TimeSpan, TimeSpan>, int> FeeTable = new Dictionary<Tuple<TimeSpan, TimeSpan>, int>
        {
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(0, 0, 0), new TimeSpan(5, 59, 59)), Settings.FEE_LOW },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59)), Settings.FEE_MEDIUM },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59)), Settings.FEE_HIGH },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59)), Settings.FEE_HIGHEST },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59)), Settings.FEE_HIGH },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59)), Settings.FEE_MEDIUM },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 59)), Settings.FEE_HIGH },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59)), Settings.FEE_HIGHEST },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59)), Settings.FEE_HIGH },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 59)), Settings.FEE_MEDIUM },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(18, 30, 0), new TimeSpan(23, 59, 59)), Settings.FEE_LOW }
        };

        public TollFeeLookup()
        {
        }

        public static int Fee(TimeSpan t)
        {

            IEnumerable<int> output = from row in FeeTable
                                      where t >= row.Key.Item1 &&
                                            t <= row.Key.Item2
                                      select row.Value;

            return output.FirstOrDefault();

        }

    }
}
