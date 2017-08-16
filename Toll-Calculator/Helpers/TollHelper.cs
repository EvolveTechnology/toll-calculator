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

        public static List<EligibleDate> GetHourlySums(List<EligibleDate> dates)
        {
            return dates.GroupBy(i => i.DateTime, new DateAndHourComparer())
                .Select(g => g.First(i => i.Fee == g.Max(m => m.Fee)))
                .Distinct().ToList();
        }

        public static List<DailySum> GetDailySums(List<EligibleDate> dates)
        {
            return dates.GroupBy(x => x.DateTime, new DateComparer()).Select(g => new DailySum(g.Sum(i => i.Fee))).Distinct().ToList();
        }

        public static IEnumerable<TollFeePeriod> GetFeePeriods()
        {
            return new List<TollFeePeriod>
            {
                new TollFeePeriod(8, new TimeSpan(6,0,0), new TimeSpan(6,29,59)),
                new TollFeePeriod(13, new TimeSpan(6,30,0), new TimeSpan(6,59,59)),
                new TollFeePeriod(18, new TimeSpan(7,0,0), new TimeSpan(7,59,59)),
                new TollFeePeriod(13, new TimeSpan(8,0,0), new TimeSpan(8,29,59)),
                new TollFeePeriod(8, new TimeSpan(8,30,0), new TimeSpan(14,59,59)),
                new TollFeePeriod(13, new TimeSpan(15,0,0), new TimeSpan(15,29,59)),
                new TollFeePeriod(18, new TimeSpan(15,30,0), new TimeSpan(16,59,59)),
                new TollFeePeriod(13, new TimeSpan(17,0,0), new TimeSpan(17,59,59)),
                new TollFeePeriod(8, new TimeSpan(18,0,0), new TimeSpan(18,29,59)),
                new TollFeePeriod(0, new TimeSpan(18,30,0), new TimeSpan(5,59,59))
            };
        }
    }
}
