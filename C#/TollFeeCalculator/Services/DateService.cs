using Nager.Date;
using System;

namespace TollFeeCalculator
{
    public class DateService : IDateService
    {
        public bool IsTollFreeDate(DateTime date)
        {
            // This should be configurable somehow..
            if (date.DayOfWeek == DayOfWeek.Saturday
            || date.DayOfWeek == DayOfWeek.Sunday
            || DateSystem.IsPublicHoliday(date, CountryCode.SE)
            || date.Month == 7) // july is free
            {
                return true;
            }

            return false;
        }
    }
}
