﻿using System;
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

            //Use time spans instead
            int hour = date.Hour;
            int minute = date.Minute;

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

        private bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2013)
            {
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }
            return false;
        }
    }
}