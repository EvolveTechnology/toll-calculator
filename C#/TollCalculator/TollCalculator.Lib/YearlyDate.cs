using System;

namespace TollCalculator.Lib
{
    public class YearlyDate
    {
        public int Day { get; set; }
        public int Month { get; set; }

        public YearlyDate(int month, int day)
        {
            Month = month;
            Day = day;
        }

        public bool IsEqualToDate(DateTime date)
        {
            var isSameDay = date.Day == Day;
            var isSameMonth = date.Month == Month;

            return isSameDay && isSameMonth;
        }
    }
}