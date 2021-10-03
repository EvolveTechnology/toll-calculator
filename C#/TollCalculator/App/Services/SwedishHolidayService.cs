using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculatorApp.Services.Interfaces;

namespace TollCalculatorApp.Services
{
   
    public class SwedishHolidayService : IHolidayService
    {

        /// <summary>
        /// Determines if date is the same day as any of the swedish bank-holidays
        /// </summary>
        public bool IsHoliday(DateTime date)
        {
            var holidays = GetSwedishHolidays(date.Year);

            if (holidays.Any(holiday => holiday.Month == date.Month && holiday.Day == date.Day))
                return true;

            return false;
        }

        /// <summary>
        /// This method returns the swedish bank holidays
        /// </summary>
        /// <param name="year">The year you want days to be generated for</param>
        /// <returns>List of DateTimes where swedish holidays occur</returns>
        ///
        /// *I assumed that the "holidays" in the description in this repository meant bank-holidays
        /// Used the following list of holidays https://www.riksbank.se/en-gb/press-and-published/calendar/bank-holidays-2021/
        private List<DateTime> GetSwedishHolidays(int year)
        {
            //Holidays that are  in terms of date of month - they occur at the same date of a month each year.
            var Holidays = new List<DateTime>() {
                new DateTime(year, 1, 1), //New years day
                new DateTime(year, 1, 6), //Epiphany
                new DateTime(year, 5,1), //First of May
                new DateTime(year, 6,6), //Swedish national day 
                new DateTime(year, 12,24), //Christmas eve
                new DateTime(year, 12,25), //Chirstmas day 
                new DateTime(year, 12,26), //Boxing day
                new DateTime(year, 12,31) //New years eve
            };

            //Holidays that are not the same date of month each year
            var calculatedHolidays = new List<DateTime>();
            calculatedHolidays.Add(GetEasterDay(year).AddDays(-2)); //Good Friday
            calculatedHolidays.Add(GetEasterDay(year).AddDays(-1)); //Easter Eve
            calculatedHolidays.Add(GetEasterDay(year)); // Easter Sunday
            calculatedHolidays.Add(GetEasterDay(year).AddDays(1)); // Easter Monday
            calculatedHolidays.Add(GetMidsummerDay(year).AddDays(-1)); //Midsummers eve
            calculatedHolidays.Add(GetMidsummerDay(year));
            calculatedHolidays.Add(GetAllSaintsDay(year));
            calculatedHolidays.Add(GetAscensionDay(year));
            calculatedHolidays.Add(GetPentecostDay(year));
            calculatedHolidays.Add(GetPentecostDay(year).AddDays(-1));  //Whitsun


            var swedishHolidays = new List<DateTime>();
            swedishHolidays.AddRange(Holidays);
            swedishHolidays.AddRange(calculatedHolidays);

            return swedishHolidays;
        }



        //This method uses Gauss algorithm to calculate easter day
        //The algorithm used here was copied from following stack-exchange article: https://codereview.stackexchange.com/q/193847
        /// <summary>
        /// This method calculates the day that Easter day occur on
        /// </summary>
        /// <param name="year">The year you want to get the date of Easter day for</param>
        /// <returns>DateTime that Easter day occur on</returns>
        private DateTime GetEasterDay(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = (b - (b / 4) - ((8 * b + 13) / 25) + (19 * a) + 15) % 30;
            int d = c - (c / 28) * (1 - (c / 28) * (29 / (c + 1)) * ((21 - a) / 11));
            int e = d - ((year + (year / 4) + d + 2 - b + (b / 4)) % 7);
            int month = 3 + ((e + 40) / 44);
            int day = e + 28 - (31 * (month / 4));
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// This method calculates the day that All saints day occur on
        /// </summary>
        /// <param name="year">The year you want to get the date of all saints day for</param>
        /// <returns>DateTime that all saints day occur on</returns>
        private DateTime GetAllSaintsDay(int year)
        {
            var date = new DateTime(year, 10, 31);
            while (date.DayOfWeek != DayOfWeek.Saturday)
            {
                date = date.AddDays(1);
            }

            return date;
        }

        /// <summary>
        /// This method calculates the day that All saints day occur on
        /// </summary>
        /// <param name="year">The year you want to get the date of All saints day for</param>
        /// <returns>DateTime that ascension day occur on</returns>
        private DateTime GetAscensionDay(int year)
        {
            return GetEasterDay(year).AddDays(39);
        }

        /// <summary>
        /// This method calculates the day that Pentecost occur on
        /// </summary>
        /// <param name="year">The year you want to get the date of Pentecost for</param>
        /// <returns>DateTime that Pentecost occur on</returns>
        private DateTime GetPentecostDay(int year)
        {
            return GetEasterDay(year).AddDays(49);
        }

        /// <summary>
        /// This method calculates the day that Midsummer day occur on
        /// </summary>
        /// <param name="year">The year you want to get the date of Midsummer day for</param>
        /// <returns>DateTime that Midsummer day occur on</returns>
        private DateTime GetMidsummerDay(int year)
        {
            var date = new DateTime(year, 6, 20);
            while (date.DayOfWeek != DayOfWeek.Saturday)
            {
                date = date.AddDays(1);
            }

            return date;
        }
    }
}
