using System;
using System.Globalization;
using System.Linq;
using TollCalculatorApp.Models;

namespace TollCalculatorApp
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
            if (dates == null)
            {
                throw new NullReferenceException("Dates can't be null");
            }

            if (IsTollFreeVehicle(vehicle) || dates.Length == 0)
                return 0;

            int totalFee = 0;
            var orderedDates = dates.OrderBy(d => d).ToArray();

            DateTime intervalStart = orderedDates[0];

            foreach (DateTime date in orderedDates)
            {
                if (totalFee > 60)
                {
                    totalFee = 60;
                    return totalFee;
                }

                int nextFee = CalculateTollFee(date);
                int tempFee = CalculateTollFee(intervalStart);

                long diffInMillies = (long)(date - intervalStart).TotalMilliseconds;
                long minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0)
                        totalFee -= tempFee;
                    if (nextFee >= tempFee)
                        tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                    intervalStart = date;
                }

                //intervalStart = date;
            }

            return totalFee;
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null)
                throw new NullReferenceException("Vehicle can't be null");

            return Enum.TryParse(vehicle.VehicleType.ToString(), out TollFreeVehicles _);
        }

        public int CalculateTollFee(DateTime date)
        {
            var TollToPay = 0;

            if (IsTollFreeDate(date))
                return TollToPay;

            int hour = date.Hour;
            int minute = date.Minute;

            TimeSpan time = new TimeSpan(hour, minute, 0);

            // I'm using TimeSpan for better readability

            if (time >= new TimeSpan(6, 0, 0) && time < new TimeSpan(6, 29, 0)
                   || (time >= new TimeSpan(8, 30, 0) && time <= new TimeSpan(14, 59, 0))
                   || (time >= new TimeSpan(18, 0, 0) && time <= new TimeSpan(18, 29, 0)))
            {
                TollToPay = 8;
                return TollToPay;
            }
            else if (time >= new TimeSpan(6, 30, 0) && time <= new TimeSpan(6, 59, 0)
                    || (time >= new TimeSpan(15, 0, 0) && time <= new TimeSpan(15, 29, 0))
                    || (time >= new TimeSpan(8, 0, 0) && time <= new TimeSpan(8, 29, 0))
                    || (time >= new TimeSpan(17, 0, 0) && time <= new TimeSpan(17, 59, 0)))
            {
                TollToPay = 13;
                return TollToPay;
            }
            else if (time >= new TimeSpan(7, 0, 0) && time <= new TimeSpan(7, 59, 0)
                     || (time >= new TimeSpan(15, 0, 0) && time <= new TimeSpan(16, 59, 0)))
            {
                TollToPay = 18;
                return TollToPay;
            }

            return TollToPay;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (IsWeekEndorJuly(date.DayOfWeek, date.Month))
            {
                return true;
            }

            if (date.Year == DateTime.Now.Year)
            {
                var tollFreeDays = TollFreeDays.GetTollFreeDays()
                                               .Where(tfd => tfd.Month == date.Month &&
                                                tfd.Day.Contains(date.Day));

                return tollFreeDays.Any();
            }
            else
            {
                throw new ArgumentOutOfRangeException($"This Date {date} is not from current year");
            }
        }

        private bool IsWeekEndorJuly(DayOfWeek dayOfWeek, int month)
        {
            return dayOfWeek == DayOfWeek.Saturday
                    || dayOfWeek == DayOfWeek.Sunday
                    || month == 7;
        }
    }
}