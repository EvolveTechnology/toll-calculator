using System;

namespace TollFeeCalculator.HolidayLookup
{
    public enum Month
    {
        NotSet = 0,
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    /// <summary>
    /// Implements a public holiday lookup for Sweden for holidays with fixed dates.
    /// </summary>
    public abstract class SwedenHolidayLookup : IHolidayLookup
    {
        // a boolean matrix where a cell at row i and col j has a value of true if day j of month i is a public holiday in Sweden
        protected readonly bool[,] holidayCalendar = new bool[13,32]; // 12 months, max 31 days per month, offset by 1 for easier indexing

        protected SwedenHolidayLookup()
        {
            // set public holidays with fixed dates
            holidayCalendar[(int) Month.January, 1] = true; // new year's day
            holidayCalendar[(int) Month.January, 6] = true; // 13th day of christmas
            holidayCalendar[(int) Month.May, 1] = true; // labor day
            holidayCalendar[(int) Month.June, 6] = true; // national day
            holidayCalendar[(int) Month.December, 24] = true; // christmas eve
            holidayCalendar[(int) Month.December, 25] = true; // christmas day
            holidayCalendar[(int) Month.December, 26] = true; // second day of christmas
            holidayCalendar[(int) Month.December, 31] = true; // new year's eve
        }

        public virtual bool IsPublicHoliday(DateTime date)
        {
            return holidayCalendar[date.Month, date.Day];
        }
    }
}
