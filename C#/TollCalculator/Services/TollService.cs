using System;
using System.Collections.Generic;
using System.Globalization;
using TollCalculator.Interfaces;
using TollCalculator.Models;
using TollCalculator.Repository;
using System.Linq;

namespace TollCalculator.Services
{
    public class TollService : ITollService
    {
        private readonly ITollRepository _tollRepository;
        public TollService(ITollRepository tollRepository)
        {
            _tollRepository = tollRepository;
        }

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int CalculateFee(Vehicle vehicle, List<DateTime> dates)
        {
            var datesAscending = dates.OrderBy(d => d).ToList();
            var dateIncrement = 0;
            int totalFee = 0;
            var intervalStart = datesAscending[0];
            int tempFeeSameHour = 0;

            foreach (DateTime date in datesAscending)
            {
                if (DateTime.Compare(dates.Last(), date) != 0)
                {
                    dateIncrement++;
                }

                int nextFee = GetTollFee(datesAscending[dateIncrement], vehicle);
                int tempFee = tempFeeSameHour != 0 ? tempFeeSameHour : GetTollFee(intervalStart, vehicle);

                var difference = datesAscending[dateIncrement] - intervalStart;

                if (difference.Hours < 1 && difference.Minutes <= 60)
                {
                    if (totalFee > 0 && nextFee >= tempFee)
                    {
                        totalFee -= tempFee;
                        totalFee = tempFee;
                    }
                    else
                    {
                        totalFee += nextFee;
                        tempFeeSameHour = nextFee;
                    }

                }
                else
                {
                    totalFee += tempFee;
                    tempFeeSameHour = 0;
                    intervalStart = datesAscending[dateIncrement];
                }

                if (totalFee > 60)
                {
                    return totalFee = 60;
                }
            }
            return totalFee;
        }

        private int GetTollFee(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || vehicle.IsTollFree()) return 0;

            int hour = date.Hour;
            int minute = date.Minute;


            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && hour <= 14 && minute >= 0 && minute <= 59) return 8; //8:30 - 15:00
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }


        private int GetTollFeev2(DateTime date, Vehicle vehicle)
        {
            if (IsTollFreeDate(date) || vehicle.IsTollFree())
                return 0;

            var time = new TimeSpan(date.Hour, date.Minute, date.Second);

            TimeSpan tollPeriodStart = new TimeSpan(06, 00, 00);
            TimeSpan tollPeriodEnd = new TimeSpan(18, 30, 00);

            var feePeriods = _tollRepository.GetTollFeePeriods();

            if (time < tollPeriodStart || time >= tollPeriodEnd)
            {
                return 0;
            }

            foreach (FeePeriod feePeriod in feePeriods)
            {
                foreach (var (startTime, endTime) in feePeriod.Period)
                {
                    if (time >= startTime && time < endTime)
                    {
                        return feePeriod.Price;
                    }
                }
            }
            return 0;
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