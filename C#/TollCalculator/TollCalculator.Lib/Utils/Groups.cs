using System;
using System.Collections.Generic;
using TollCalculator.Lib.Models;

namespace TollCalculator.Lib.Utils
{
    public static class Groups
    {
        public static List<SameHourGroup> GetDatesGroupedByHour(List<DateTime> dates)
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
        
        public static List<SameDayGroup> GetDatesGroupedByDay(DateTime[] dates)
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
    }
}