using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TollCalculatorApp
{
    public static class DateTimeExtension
    {

        /// <summary>
        /// Determines if date is between two Timespans representing time of day
        /// </summary>        
        public static bool IsBetweenTimes(this DateTime date, TimeSpan start, TimeSpan end)
        {
            if(start <= end)
                return date.TimeOfDay >= start && date.TimeOfDay < end;
            else
                throw new ArgumentException("End must be later than start");
        }

        /// <summary>
        /// This method calculates the day that easterday occur on using Gauss algorithm
        /// Code for algorithm used comes from this stack-exchange article https://codereview.stackexchange.com/questions/193847/find-easter-on-any-given-year
        /// </summary>
        /// <param name="year">The year you want to get the date of easter day for</param>
        /// <returns>DateTime that Easterday occurs on </returns>
        
        public static bool IsWeekend(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) 
                return true;
            
            return false;
        }
    }
}

