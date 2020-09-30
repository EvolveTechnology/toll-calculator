using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TollCalculator.Data.Entities;
using TollCalculator.Helper;

namespace TollCalculator.Data.Services
{
    public class TollCalculatorService : ITollCalculatorService
    {
        public decimal GetTollFee(Vehicle vehicle, DateTime[] dates)
        {
            // TODO: DateTime format [Validation]
            // TODO: All the datetimes in array should be in the same day [Validation]
            // Validation for the inputs
            if ((vehicle == null) || (dates == null) || (dates.Length == 0))
                throw new Exception("Vehicle and Dates are required");

            var intervalStart = dates[0];
            decimal tempFee = GetTollFee(intervalStart, vehicle);
            decimal totalFee = 0;
            foreach (var date in dates.OrderBy(d => d.Date))
            {
                decimal nextFee = GetTollFee(date, vehicle);
                TimeSpan minutes = new TimeSpan(date.Ticks - intervalStart.Ticks);
                
                if (minutes.TotalMinutes <= 60)
                {
                    if (totalFee > 0)
                        totalFee -= tempFee;
                    if (nextFee >= tempFee)
                        tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    intervalStart = date;
                    tempFee = nextFee;
                    totalFee += nextFee;
                }
            }

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        public decimal GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (TollCalculatorHelper.IsTollFreeDate(date) || TollCalculatorHelper.IsTollFreeVehicle(vehicle))
                return 0;
            return TollCalculatorHelper.GetDurationFee(date);
        }
    }
}
