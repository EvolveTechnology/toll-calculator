using System;
using DateLibrary;
using Toll_Calculator.Interfaces;

namespace Toll_Calculator
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

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            var intervalStart = dates[0];
            var totalFee = 0;
            foreach (var date in dates)
            {
                var nextFee = GetPeriodFee(vehicle, date);
                var tempFee = GetPeriodFee(vehicle, intervalStart);

                var diffInMillies = date.Millisecond - intervalStart.Millisecond;
                var minutes = diffInMillies/1000/60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private static bool IsTollFreeVehicle(IVehicle vehicle)
        {
            return vehicle != null && vehicle.IsTollFree();
        }

        public int GetPeriodFee(IVehicle vehicle, DateTime date)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            var hour = date.Hour;
            var minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        private static bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || date.IsHoliday();
        }


    }
}