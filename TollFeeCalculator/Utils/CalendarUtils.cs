using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator.Utils
{
    public static class CalendarUtils
    {
        public static DateTime CalculateEasterDay(int year)
        {
            // Gauss Easter formula, code copied from here (second hit on Google): https://codereview.stackexchange.com/questions/193847/find-easter-on-any-given-year
            // The variable names don't have meaningful names because the numbers themselves don't really have a distinct meaning.
            // The formula takes into account astronomical movements and such things, it is outside the scope of this assignment to fully understand it.
            // The actual output will be tested in a unit test against some known Easter Day dates to verify that the formula is correct. 
            
            // Edit: Note that in my first attempt I used the code from here (first hit on Google): https://www.geeksforgeeks.org/how-to-calculate-the-easter-date-for-a-given-year-using-gauss-algorithm/
            // Interestingly, this code only passed 8 out of 10 unit tests. So I had to switch to the current algorithm which passes all 10 unit tests.

            int a = year % 19;
            int b = year / 100;
            int c = (b - (b / 4) - ((8 * b + 13) / 25) + (19 * a) + 15) % 30;
            int d = c - (c / 28) * (1 - (c / 28) * (29 / (c + 1)) * ((21 - a) / 11));
            int e = d - ((year + (year / 4) + d + 2 - b + (b / 4)) % 7);
            int month = 3 + ((e + 40) / 44);
            int day = e + 28 - (31 * (month / 4));

            return new DateTime(year, month, day);
        }

        public static DateTime CalculateMidsummerDay(int year)
        {
            var potentialDates = new List<DateTime>()
        {
            new DateTime(year, 6, 20),
            new DateTime(year, 6, 21),
            new DateTime(year, 6, 22),
            new DateTime(year, 6, 23),
            new DateTime(year, 6, 24),
            new DateTime(year, 6, 25),
            new DateTime(year, 6, 26),
        };

            foreach (var date in potentialDates)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                    return date;
            }

            // Logically unreachable code since one of the potential dates must be a Saturday, but the compiler doesn't know that.
            return potentialDates.First();
        }

        public static DateTime CalculateAllSaintsDay(int year)
        {
            var potentialDates = new List<DateTime>()
        {
            new DateTime(year, 10, 31),
            new DateTime(year, 11, 1),
            new DateTime(year, 11, 2),
            new DateTime(year, 11, 3),
            new DateTime(year, 11, 4),
            new DateTime(year, 11, 5),
            new DateTime(year, 11, 6),
        };

            foreach (var date in potentialDates)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                    return date;
            }

            // Logically unreachable code since one of the potential dates must be a Saturday, but the compiler doesn't know that.
            return potentialDates.First();
        }
    }
}
