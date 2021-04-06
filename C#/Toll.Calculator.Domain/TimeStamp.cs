using System;

namespace Toll.Calculator.Domain
{
    public class TimeStamp
    {
        public int Hour { get; set; }
        public int Minute { get; set; }

        public TimeStamp(DateTime dateTime)
        {
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
        }

        public static bool operator <=(TimeStamp t1, TimeStamp t2)
        {
            if (t1.Hour < t2.Hour) return true;
            if (t1.Hour == t2.Hour && t1.Minute <= t2.Minute) return true;

            return false;
        }

        public static bool operator >=(TimeStamp t1, TimeStamp t2)
        {
            if (t1.Hour > t2.Hour) return true;
            if (t1.Hour == t2.Hour && t1.Minute >= t2.Minute) return true;

            return false;
        }
    }
}