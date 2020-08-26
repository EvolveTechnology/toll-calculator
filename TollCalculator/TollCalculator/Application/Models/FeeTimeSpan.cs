using System;

namespace TollCalculator.Application.Models
{
    internal class FeeTimeSpan
    {
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int Fee { get; set; }

        public FeeTimeSpan(TimeSpan start, TimeSpan end, int fee)
        {
            Start = start;
            End = end;
            Fee = fee;
        }
    }
}