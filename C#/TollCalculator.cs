using PublicHoliday;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace TollCalculator
{
    public class TollCalculator
    {
        IPublicHolidays publicHolidays;

        public TollCalculator()
        {
            publicHolidays = new SwedenPublicHoliday();
        }

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(Vehicle vehicle, DateTime[] dates)
        {
            if (IsTollFreeVehicle(vehicle))
                return 0;

            int sum = 0;

            // Group all dates by day, also removing all Toll Free Days
            var datesGroupedByDay = dates.GroupBy(x => x.Date).Where(x => !IsTollFreeDate(x.First()));

            foreach (var item in datesGroupedByDay)
            {
                var daySum = 0;

                // Group by hour
                var datesGroupedByHour = item.GroupBy(x => x.Hour);
                foreach(var hour in datesGroupedByHour)
                {
                    // Calculate sum for this hour
                    daySum += hour.Select(x => GetTollFee(x)).Max();
                }

                // Max per day is 60
                if (daySum > 60)
                    daySum = 60;

                sum += daySum;
            }

            return sum;
        }

        Dictionary<Vehicle, bool> TollFreeLookup = null;

        public bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (TollFreeLookup == null)
            {
                var t = typeof(Vehicle);
                TollFreeLookup = new Dictionary<Vehicle, bool>();
                foreach(var v in t.GetEnumNames())
                {
                    var field = t.GetField(v);
                    var isTollFree = field.GetCustomAttribute(typeof(TollFreeAttribute)) != null;
                    TollFreeLookup[(Vehicle)Enum.Parse(t,v)] = isTollFree;
                }
            }

            return TollFreeLookup[vehicle];
        }

        public int GetTollFee(DateTime date)
        {
            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30) return 13;
            else if (hour == 7) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && minute >= 30) return 8;
            else if (hour >= 9 && hour <= 14) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 30 || hour == 16) return 18;
            else if (hour == 17) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        private Boolean IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            return publicHolidays.IsPublicHoliday(date);
        }
    }
}