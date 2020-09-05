using System;

namespace TollFeeCalculator
{
    public static class TimeExtensions
    {
        public static bool IsInRange(this TimeSpan time, int fromHours, int fromMinutes, int toHours, int toMinutes)
        {
            return time >= new TimeSpan(fromHours, fromMinutes, 0) && time <= new TimeSpan(toHours, toMinutes, 0);
        }
    }
}