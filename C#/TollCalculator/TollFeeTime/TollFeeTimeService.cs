using System;
using System.Collections.Generic;

namespace TollFeeCalculator.TollFeeTime
{
    public class TollFeeTimeService : ITollFeeTimeService
    {
        public List<FeeTime> GetTollFeeTimes()
        {
            return new List<FeeTime> {
                new FeeTime(new TimeSpan(6,0,0),new TimeSpan(6,29,0),8),
                new FeeTime(new TimeSpan(6,30,0),new TimeSpan(6,59,0),13),
                new FeeTime(new TimeSpan(7,0,0),new TimeSpan(7,59,0),18),
                new FeeTime(new TimeSpan(8,0,0),new TimeSpan(8,29,0),13),
                new FeeTime(new TimeSpan(8,30,0),new TimeSpan(14,59,0),8),
                new FeeTime(new TimeSpan(15,0,0),new TimeSpan(15,29,0),13),
                new FeeTime(new TimeSpan(15,30,0),new TimeSpan(16,59,0),18),
                new FeeTime(new TimeSpan(17,0,0),new TimeSpan(17,59,0),13),
                new FeeTime(new TimeSpan(18,0,0),new TimeSpan(18,29,0),8),
        };
        }

    }
}
