using System;

namespace TollCalculator.Tests.Utils
{
    public static class DateUtils
    {
        public static DateTime ParseDateAndTime(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}