using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_Calculator.Helpers
{
    public class DateComparer : IEqualityComparer<DateTime>
    {
        public bool Equals(DateTime x, DateTime y)
        {
            var xDate = GetDate(x);
            var yDate = GetDate(y);

            return xDate == yDate;
        }

        private static DateTime GetDate(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month,
                                dateTime.Day, 0,
                                0, 0);
        }

        public int GetHashCode(DateTime obj)
        {
            return GetDate(obj).GetHashCode();
        }
    }
}
