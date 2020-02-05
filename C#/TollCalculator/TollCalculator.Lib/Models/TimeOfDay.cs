using System;

namespace TollCalculator.Lib.Models
{
    public class TimeOfDay
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        public TimeOfDay(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public DateTime ToDateTime(DateTime reference)
        {
            return new DateTime(reference.Year, reference.Month, reference.Day, Hour, Minute, reference.Second,
                reference.Millisecond, reference.Kind);
        }
    }
}