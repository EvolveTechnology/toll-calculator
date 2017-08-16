using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_Calculator.Helpers
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
