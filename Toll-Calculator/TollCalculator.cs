using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DateLibrary;
using Toll_Calculator.Helpers;
using Toll_Calculator.Interfaces;
using Toll_Calculator.Models;
namespace Toll_Calculator
{
    public class TollCalculator
    {
        private static IEnumerable<TollFeePeriod> TollFeePeriods => GetFeePeriods();

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (vehicle == null || vehicle.IsTollFree()) return 0;
            var eligibleDates = TollHelper.GetEligibleDates(dates, TollFeePeriods);

            if (!eligibleDates.Any())
                return 0;

            //Get the highest price for each hour (also separated by day, month, etc)
            var hourSums = eligibleDates.GroupBy(i => i.DateTime, new DateAndHourComparer())
                   .Select(g => g.First(i => i.Fee == g.Max(m => m.Fee)))
                   .Distinct().ToList();

            var daySums = hourSums.GroupBy(x => x.DateTime, new DateComparer()).Select(g => new DailySum(g.Sum(i => i.Fee))).Distinct().ToList();
            return daySums.Any() ? daySums.Sum(x=>x.Sum) : 0;
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

    public class DailySum
    {
        public DailySum(int value)
        {
            if (value > 60)
                value = 60;

            Sum = value;
        }

        public int Sum { get; set; }
    }
}