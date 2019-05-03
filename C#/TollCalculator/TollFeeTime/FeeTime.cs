using System;

namespace TollFeeCalculator.TollFeeTime
{
    public class FeeTime
    {
        public FeeTime(TimeSpan start, TimeSpan end, int amount)
        {
            Start = start;
            End = end;
            Amount = amount;
        }
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public int Amount { get; }
    }
}
