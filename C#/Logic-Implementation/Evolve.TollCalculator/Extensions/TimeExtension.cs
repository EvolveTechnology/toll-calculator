using System;

namespace Evolve.TollCalculator.Extensions
{
    public static class TimeExtension
    {
        public static bool IsBetween(this DateTime now, TimeSpan start, TimeSpan end)
        {
            var time = now.TimeOfDay;
            if (start <= end)
            {
                return time >= start && time <= end;
            }
            return time >= start || time <= end;
        }
    }
}
