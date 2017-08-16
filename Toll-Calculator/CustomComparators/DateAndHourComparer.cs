using System;
using System.Collections.Generic;

namespace Toll_Calculator.CustomComparators
{
    public class DateAndHourComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            var xAsDateAndHours = GetDateAndHour(x);
            var yAsDateAndHours = GetDateAndHour(y);

            return xAsDateAndHours == yAsDateAndHours;
        }

        private static DateTime GetDateAndHour(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month,
                                dateTime.Day, dateTime.Hour,
                                0, 0);
        }

        public int GetHashCode(DateTime obj)
        {
            return GetDateAndHour(obj).GetHashCode();
        }
    }
}
