using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculator.Lib.Models;

namespace TollCalculator.Lib
{ 
    public static class TollCalculator
    {
        public static int GetTollFee(VehicleType vehicleType, DateTime[] dates)
        {
            var datesGroupedByDay = GetDatesGroupedByDay(dates);
            return datesGroupedByDay.Sum(g => GetDailyFee(g, vehicleType));
        }

        private static List<SameDayGroup> GetDatesGroupedByDay(DateTime[] dates)
        {
            var groups = new List<SameDayGroup>();

            foreach (var date in dates)
            {
                var existingGroup = groups.Find(g => g.IsSameDay(date));
                var hasExisting = existingGroup != null;

                if (hasExisting)
                    existingGroup.AddDate(date);
                else
                {
                    var newGroup = new SameDayGroup(date);
                    groups.Add(newGroup);
                }
            }

            return groups;
        }

        private static int GetDailyFee(SameDayGroup group, VehicleType vehicleType)
        {
            try
            {
                CheckIsTollFreeVehicle(vehicleType);
                CheckIsTollFreeDay(group);
                CheckIsTollFreeHoliday(group);

                var datesGroupedByHour = GetDatesGroupedByHour(group.Dates);
                var hourGroupsTotal = datesGroupedByHour.Sum(GetHourlyFee);

                return GetFeeInsideDailyLimit(hourGroupsTotal);
            }
            catch (FeeExemptedException)
            {
                return 0;
            }
        }

        private static void CheckIsTollFreeVehicle(VehicleType vehicleType)
        {
            var isTolledVehicle = TollRules.TollVehicleTypes.Contains(vehicleType);

            if (!isTolledVehicle)
                throw new FeeExemptedException();
        }

        private static void CheckIsTollFreeDay(SameDayGroup group)
        {
            var isTollFreeDay = TollRules.TollFreeDays.Contains(group.DayOfWeek);

            if (isTollFreeDay)
                throw new FeeExemptedException();
        }

        private static void CheckIsTollFreeHoliday(SameDayGroup group)
        {
            var isHoliday = IsPublicHoliday(group);

            if (isHoliday)
                throw new FeeExemptedException();
        }

        private static List<SameHourGroup> GetDatesGroupedByHour(List<DateTime> dates)
        {
            var groups = new List<SameHourGroup>();

            foreach (var date in dates)
            {
                var existingGroup = groups.Find(g => g.IsSameHour(date));
                var hasExisting = existingGroup != null;

                if (hasExisting)
                    existingGroup.AddDate(date);
                else
                {
                    var newGroup = new SameHourGroup(date);
                    groups.Add(newGroup);
                }
            }

            return groups;
        }

        private static int GetHourlyFee(SameHourGroup group)
        {
            var hourlyFees = group.Dates.Select(date => GetSingleFee(date)).ToList();
            return hourlyFees.Max();
        }

        private static int GetSingleFee(in DateTime date)
        {
            var rushHourFee = GetRushHourFee(date);
            var isRushHour = rushHourFee != null;

            if (isRushHour)
                return rushHourFee.Fee;
            else
                return TollRules.DefaultFee;
        }

        private static RushHourFee GetRushHourFee(DateTime date)
        {
            return TollRules.RushHourFees.FirstOrDefault(rushHourFee => rushHourFee.IsInsideRange(date));
        }

        private static int GetFeeInsideDailyLimit(int fee)
        {
            var isInsideLimit = fee <= TollRules.DailyFeeMax;

            if (isInsideLimit)
                return fee;
            else
                return TollRules.DailyFeeMax;
        }

        private static bool IsPublicHoliday(SameDayGroup group)
        {
            /*
             * NOTE (this is a TODO, that should normally go to a separate issue in the issue-tracker and not in the code):
             * There is an obvious mistake here, and this method is lacking dynamic public holidays,
             * e.g. Pingstdagen (sjunge söndagen efter påsk)
             *
             * To fix this either:
             * 1) Implement logic that can calculate the dynamic holidays for a year based on the formulas
             * available for Swedish public holidays (e.g. https://sv.wikipedia.org/wiki/Helgdagar_i_Sverige)
             * 2) Start using a third-party library that solves this, e.g. https://www.nuget.org/packages/Nager.Date/,
             * However, such a library should be vetted in the sense if the license complies with ours and if we can trust
             * the code or not.
             *
             * Once dynamic holdays are added to the code, the unit tests should also reflect test cases with dynamic holidays.
             */

            return SwedishHolidays.StaticPublicHolidays.Any(holiday => holiday.IsEqualToDate(group.Month, group.Day));
        }

        private class FeeExemptedException : Exception
        {
        }
    }
}