using System;

namespace TollFeeCalculator
{
    public class TimespanFeeRange
    {
        public TimespanFeeRange(TimeSpan from, TimeSpan to, int fee)
        {
            if (to < from)
                throw new InvalidOperationException("'To' parameter should be bigger than 'From'");

            From = from;
            To = to;
            Fee = fee;
        }

        public TimeSpan From { get; }
        public TimeSpan To { get; }
        public int Fee { get; }
    }
}
