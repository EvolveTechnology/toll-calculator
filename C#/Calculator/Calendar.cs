using System;

namespace TollFeeCalculator
{
    public class Calendar
    {
        public Calendar()
        {
        }

        public static Boolean IsTollFreeDate(DateTime t)
        {
            // I know this method is pretty ugly, but I felt building a more flexible and robust solution wasn't worth the effort.
            // In a "real" project, I would probably hunt down some sort of script that calculated public holidays in the country
            // and use that, rather than reinventing the wheel and write one myself.

            int year = t.Year;
            int month = t.Month;
            int day = t.Day;

            if (t.DayOfWeek == DayOfWeek.Saturday || t.DayOfWeek == DayOfWeek.Sunday) return true;

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
