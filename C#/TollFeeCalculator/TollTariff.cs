using System;
using System.Linq;
using Nager.Date;

namespace TollFeeCalculator
{
    public class TollTariff : ITollTariff
    {
        public int TollIntervalInMinutes => 60;

        public int MaxFeePerDay => 60;

        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date) || vehicle.IsTollFree) return 0;

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

        private static bool IsTollFreeDate(DateTime date)
        {
            var se = CountryCode.SE;
            if (date.Month == 7) return true;
            if (date.Month == 4 && date.Day == 30) return true;
            if (date.Month == 6 && date.Day == 05) return true;
            if (DateSystem.IsWeekend(date, se)) return true;
            if (DateSystem.IsPublicHoliday(date, se)) return true;

            if (DateSystem.IsPublicHoliday(date.AddDays(1), se))
            {
                var holiday = DateSystem.GetPublicHoliday(date.Year, se).First(h => h.Date == date.Date.AddDays(1));
                return new[] { "Ascension Day", "All Saints' Day", "Good Friday" }.Any(name => name == holiday.Name);
            }

            return false;
        }
    }
}