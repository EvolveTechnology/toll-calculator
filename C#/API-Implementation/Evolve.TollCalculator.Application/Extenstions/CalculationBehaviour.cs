using Evolve.TollCalculator.Core.Common;
using Evolve.TollCalculator.Core.Extensions;
using System;
using System.Linq;

namespace Evolve.TollCalculator.Application.Extenstions
{
    public class CalculationBehaviour
    {
        /// <summary>
        /// Get the Fee for the date range and the type of the vehicle
        /// </summary>
        /// <param name="vehicle">Type of Vehicle</param>
        /// <param name="dates">Dates as an array</param>
        /// <returns>Total Fee Amount for the time range</returns>
        public int GetTollFee(Vehicle vehicle, DateTime[] dates)
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
        /// Get the individual fee by the date and the type of the vehicle
        /// </summary>
        /// <param name="date"></param>
        /// <param name="vehicle"></param>
        /// <returns>Fee Amount</returns>
        public int GetTollFeeByDate(DateTime date, Vehicle vehicle)
        {
            if (HolidayBehaviour.IsTollFreeDate(date) || VehicleBehaviour.IsTollFreeVehicle(vehicle)) { return 0; }

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
