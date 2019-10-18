using System;
using System.Linq;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        private const int MaxFee = 60;

        /// <summary>
        /// Calculate the total toll fee for one day.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <param name="dates">Date and time of all passes on one day.</param>
        /// <returns>The total toll fee for that day.</returns>
        public int GetTotalTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));
            if (dates == null)
                throw new ArgumentNullException(nameof(dates));
            if (dates.Length == 0)
                throw new ArgumentException($"{nameof(dates)} cannot be empty");
            if (!AreAllDatesSameDay(dates))
                throw new ArgumentException("All dates must be the same year, month and day");

            var sortedDates = dates.OrderBy(d => d);
            var startTime = DateTime.MinValue;
            var totalFee = 0;
            var highestFee = 0;

            for (int i = 0; i < dates.Length; i++)
            {
                if (totalFee >= MaxFee)
                    break;

                var date = dates[i];
                var fee = GetTollFee(vehicle, date);

                if (fee > 0)
                {
                    if (date <= startTime.AddHours(1)) //Still within one hour
                    {
                        highestFee = Math.Max(highestFee, fee);
                    }
                    else //New hour
                    {
                        totalFee += highestFee;
                        startTime = date;
                        highestFee = fee;
                    }
                }

                if (i == dates.Length - 1) //Special case for last passing
                    totalFee += highestFee;
            }

            return totalFee < MaxFee ? totalFee : MaxFee;
        }

        private bool AreAllDatesSameDay(DateTime[] dates)
        {
            if (dates.Length <= 1)
            {
                return true;
            }
            else
            {
                var currentDate = dates[0].Date;
                return !dates.Any(d => !d.Date.Equals(currentDate));
            }
        }

        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            var vehicleType = vehicle.GetVehicleType().ToString();
            return Enum.TryParse<TollFreeVehicle>(vehicleType, true, out _);
        }

        private int GetTollFee(IVehicle vehicle, DateTime date)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
                return 0;

            var intervals = new FeeIntervalRepository().GetIntervals();

            foreach (var i in intervals)
            {
                if (i.start <= date.TimeOfDay && date.TimeOfDay < i.end)
                    return i.fee;
            }

            return 0;
        }

        private bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return true;

            var datesRepository = new FreeDatesRepository();
            var freeDays = datesRepository.GetFeeFreeDays();
            var freeMonths = datesRepository.GetFeeFreeMonths();

            return freeDays.Any(d => d.Year == date.Year && d.Month == date.Month && d.Day == date.Day)
                || freeMonths.Any(m => m.Year == date.Year && m.Month == date.Month);
        }
    }
}