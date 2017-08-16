using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DateLibrary;
using Toll_Calculator.Interfaces;
using Toll_Calculator.Models;

namespace Toll_Calculator.Helpers
{
    public class TollHelper
    {
        public static List<EligibleDate> GetEligibleDates(DateTime[] dates, IEnumerable<TollFeePeriod> tollFeePeriods)
        {
            //removes all toll free time periods and all toll free dates
            return (from date in dates
                where !IsTollFreeDate(date) && tollFeePeriods.Any(x => x.Start <= date.TimeOfDay && x.End >= date.TimeOfDay && x.TollFee > 0)
                select new EligibleDate(date, tollFeePeriods.First(x => x.Start <= date.TimeOfDay && x.End >= date.TimeOfDay).TollFee)).OrderBy(x => x.DateTime).ToList();
        }

        public static bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.IsHoliday();
        }

        public static bool IsTollFreeVehicle(IVehicle vehicle)
        {
            return vehicle != null && vehicle.IsTollFree();
        }
    }
}
