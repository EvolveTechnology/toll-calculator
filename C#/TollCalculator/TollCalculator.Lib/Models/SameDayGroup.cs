using System;
using System.Collections.Generic;

namespace TollCalculator.Lib.Models
{
    public class SameDayGroup
    {
        public int Day { get; }
        public int Month { get; }
        public int Year { get; }
        public DayOfWeek DayOfWeek { get; }
        
        public List<DateTime> Dates { get; }

        public SameDayGroup(DateTime initialDate)
        {
            Day = initialDate.Day;
            Month = initialDate.Month;
            Year = initialDate.Year;
            DayOfWeek = initialDate.DayOfWeek;
            
            Dates = new List<DateTime>() { initialDate };
        }

        public void AddDate(DateTime date)
        {
            Dates.Add(date);
        }

        public bool IsSameDay(DateTime date)
        {
            return Day == date.Day && Month == date.Month && Year == date.Year;
        }
    }
}