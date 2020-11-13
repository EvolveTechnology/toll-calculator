using System;

namespace toll_calculator
{
    public class TollZone
    {
        public TollZone(TimeSpan start, TimeSpan end, int fee)
        {
            Start = start;
            End = end;
            Fee = fee;
        }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
        public int Fee { get; set; }

        public bool IsValidZone(DateTime time)
        {
            var timeSpan = new TimeSpan(time.Hour, time.Minute, 0);
            return (Start <= timeSpan && timeSpan <= End);
        }
    }   
}
