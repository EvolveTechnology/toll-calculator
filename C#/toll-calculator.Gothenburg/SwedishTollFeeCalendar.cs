using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TollCalculator.Contracts.Calendar;

namespace TollCalculator.Gothenburg
{
    /// <summary>
    /// Represents toll fee calendar for Sweden.
    /// </summary>
    /// <remarks>Applicable between 2005 and 2099 (assuming rules and holidays are not changed).</remarks>
    public class SwedishTollFeeCalendar : ITollFeeCalendar
    {
        private static ConcurrentDictionary<int, DateTime> EasterDays = new ConcurrentDictionary<int, DateTime>();

        private static (int month, int day)[] FixedTollFreeDates => new[]
        {
            // New Year
            (month: 01, day: 01),
            // Epiphany
            (month: 01, day: 05),
            (month: 01, day: 06),
            // Valpurgis
            (month: 04, day: 30),
            // 1st of May
            (month: 05, day: 01),
            // National Day of Sweden
            (month: 06, day: 05),
            (month: 06, day: 06),
            // Christmas
            (month: 12, day: 24),
            (month: 12, day: 25),
            (month: 12, day: 26),
            // New Year
            (month: 12, day: 31)
        };

        private static CultureInfo SwedishCulture => CultureInfo.CreateSpecificCulture("sv-SE");

        public CultureInfo Culture => SwedishCulture;

        public Calendar Calendar => SwedishCulture.Calendar;

        public bool IsTollFree(DateTime date)
        {
            if (date.Year < 2005 || date.Year > 2099)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(date), date, "Years before 2005 and after 2099 not supported.");
            }

            if (Calendar.GetDayOfWeek(date) == DayOfWeek.Saturday ||
                Calendar.GetDayOfWeek(date) == DayOfWeek.Sunday)
            {
                return true;
            }

            if (Calendar.GetMonth(date) == 7)
            {
                return true;
            }

            if (FixedTollFreeDates
                .Any(tuple => tuple.month == date.Month && tuple.day == date.Day))
            {
                return true;
            }

            if (IsEaster(date))
            {
                return true;
            }

            if (IsAscension(date))
            {
                return true;
            }

            if (IsMidsummer(date))
            {
                return true;
            }

            if (IsAllSaintsDay(date))
            {
                return true;
            }

            return false;
        }
        
        private bool IsAllSaintsDay(DateTime date)
        {
            return Calendar.GetDayOfWeek(date) == DayOfWeek.Friday &&
               ((date.Month == 10 && date.Day >= 30) ||
                (date.Month == 11 && date.Day < 6));
        }

        private bool IsMidsummer(DateTime date)
        {
            return Calendar.GetDayOfWeek(date) == DayOfWeek.Friday &&
                date.Month == 6 && date.Day >= 19 && date.Day < 26;
        }

        private bool IsAscension(DateTime date)
        {
            DateTime ascension = GetEasterDay(date.Year) + TimeSpan.FromDays(39);
            TimeSpan difference = date.Date - ascension;

            return difference >= TimeSpan.FromDays(-1) &&
                difference <= TimeSpan.FromDays(0);
        }

        private bool IsEaster(DateTime date)
        {
            DateTime easter = GetEasterDay(date.Year);
            TimeSpan difference = date.Date - easter;

            return difference >= TimeSpan.FromDays(-3) &&
                difference <= TimeSpan.FromDays(1);
        }

        private DateTime GetEasterDay(int year) => EasterDays.GetOrAdd(year, CalculateEasterDay);

        public IEnumerable<DateTime> TollFreeDaysForYear(int year)
        {
            if (year < 2005 || year > 2099)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(year), year, "Years before 2005 and after 2099 not supported.");
            }

            for (int month = 1; month <= 12; month++)
            {
                DateTime date = new DateTime(year, month, 1);
                for (int day = 1; day <= Calendar.GetDaysInMonth(year, month); day++)
                {
                    if (IsTollFree(date))
                    {
                        yield return date;
                    }

                    date += TimeSpan.FromDays(1);
                }
            }
        }

        private static DateTime CalculateEasterDay(int year)
        {
            const int M = 24; // For years 1900 - 2199
            const int N = 5; // For years 1900 - 2099

            int a = year % 19;
            int b = year % 4;
            int c = year % 7;
            int d = (19 * a + M) % 30;
            int e = (2 * b + 4 * c + 6 * d + N) % 7;

            if (d + e <= 9)
            {
                return new DateTime(year, 3, 22 + d + e);
            }

            int dayInApril = d + e - 9;

            if (dayInApril == 26)
            {
                return new DateTime(year, 4, 19);
            }

            if (dayInApril == 25 && d == 28 && e == 6 && (11 * M + 11) % 30 < 19)
            {
                return new DateTime(year, 4, 18);
            }

            return new DateTime(year, 4, dayInApril);
        }

    }
}
