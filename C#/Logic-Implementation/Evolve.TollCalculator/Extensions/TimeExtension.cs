using System;

namespace Evolve.TollCalculator.Extensions
{
    public static class TimeExtension
    {
        public static bool IsBetween(this DateTime now, TimeSpan start, TimeSpan end)
        {
            var time = now.TimeOfDay;
            // Scenario 1: If the start time and the end time are in the same day.
            if (start <= end)
            {
                return time >= start && time <= end;
            }
            // Scenario 2: The start time and end time is on different days.
            return time >= start || time <= end;
        }
    }
}
