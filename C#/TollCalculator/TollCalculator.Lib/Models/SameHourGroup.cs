using System;
using System.Collections.Generic;

namespace TollCalculator.Lib.Models
{
    public class SameHourGroup
    {
        public int Hour { get; }
        public List<DateTime> Dates { get; }

        public SameHourGroup(DateTime initialDate)
        {
            Hour = initialDate.Hour;
            Dates = new List<DateTime>() { initialDate };
        }

        public void AddDate(DateTime date)
        {
            Dates.Add(date);
        }

        public bool IsSameHour(DateTime date)
        {
            return date.Hour == Hour;
        }
    }
}