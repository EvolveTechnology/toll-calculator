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
        /// This method returns wether a datetime is a weekend day or not
        /// </summary>
        public static bool IsWeekend(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) 
                return true;
            
            return false;
        }
    }
}

