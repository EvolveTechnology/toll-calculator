using System;
using System.Collections.Generic;
using System.Linq;
using Toll_Calculator.Helpers;
using Toll_Calculator.Interfaces;
using Toll_Calculator.Models;
namespace Toll_Calculator
{
    public class TollCalculator
    {
        private static IEnumerable<TollFeePeriod> TollFeePeriods => TollHelper.GetFeePeriods();

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (vehicle == null || vehicle.IsTollFree() || dates == null || dates.Length < 1) return 0;
            var eligibleDates = TollHelper.GetEligibleDates(dates, TollFeePeriods);

            if (!eligibleDates.Any())
                return 0;

            var hourlySums = TollHelper.GetHourlySums(eligibleDates);
            var dailySums = TollHelper.GetDailySums(hourlySums);

            return dailySums.Any() ? dailySums.Sum(x => x.Sum) : 0;
        }
    }
}