using Evolve.TollCalculator.Extensions;
using Evolve.TollCalculator.Models;
using System;
using System.Linq;

namespace Evolve.TollCalculator
{
    public class TollCalculator
    {
        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */
        public int GetTollFeeByDate(Vehicle vehicle, DateTime[] dates)
        {
            int totalFee = 0;
            if (dates.Length > 0)
            {
                var datesSorted = dates.OrderBy(d => d).ToList();
                DateTime intervalStart = datesSorted[0];

                foreach (DateTime date in datesSorted)
                {
                    int nextFee = GetTollFeeByDate(date, vehicle);
                    int tempFee = GetTollFeeByDate(intervalStart, vehicle);

                    TimeSpan span = date - intervalStart;
                    int diffInMinutes = (int)span.TotalMinutes;

                    if (diffInMinutes <= 60)
                    {
                        if (totalFee > 0)
                        {
                            totalFee -= tempFee;
                        };

                        if (nextFee >= tempFee)
                        {
                            tempFee = nextFee;
                        };

                        totalFee += tempFee;
                    }
                    else
                    {
                        totalFee += nextFee;
                    }

                    intervalStart = date;
                }
            }

            if (totalFee > 60) { totalFee = 60; };
            return totalFee;
        }

        /// <summary>
        /// Get Toll fee by 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public int GetTollFeeByDate(DateTime date, Vehicle vehicle)
        {
            if (HolidayExtension.IsTollFreeDate(date) || VehicleExtension.IsTollFreeVehicle(vehicle)) 
            { 
                return 0; 
            }

            if (date.IsBetween(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59))
            || date.IsBetween(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0))
            || date.IsBetween(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 0)))
            {
                return 8;
            }
            else if (date.IsBetween(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0))
                || date.IsBetween(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0))
                || date.IsBetween(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0))
                || date.IsBetween(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 0)))
            {
                return 13;
            }
            else if (date.IsBetween(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0))
                || date.IsBetween(new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 0)))
            {
                return 18;
            }
            else
            {
                return 0;
            }
        }
    }
}
