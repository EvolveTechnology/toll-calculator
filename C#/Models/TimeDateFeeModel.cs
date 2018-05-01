using System;

namespace TollFeeCalculator.Models
{
    public class TimeDateFeeModel
    {
        public int StartHour { get; set; } = 0;
        public int StartMinute { get; set; } = 0;
        public int EndHour { get; set; } = 0;
        public int EndMinute { get; set; } = 0;
        public int MinuteOffset { get; set; } = 0;
        public int Fee { get; set; }

        public bool IsInTimeSpan(DateTime time)
        {
            return ((time.Hour == StartHour && time.Minute >= StartMinute)
                    || time.Hour > StartHour)
                    && (time.Hour <= EndHour && time.Minute <= EndMinute)
                    && time.Minute >= MinuteOffset;
        }
    }
}
