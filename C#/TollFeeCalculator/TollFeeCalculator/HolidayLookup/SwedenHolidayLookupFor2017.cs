using System;

namespace TollFeeCalculator.HolidayLookup
{
    /// <summary>
    /// Implements a public holiday lookup for Sweden for holidays with variable dates and year 2017.
    /// </summary>
    public class SwedenHolidayLookupFor2017 : SwedenHolidayLookup
    {
        public SwedenHolidayLookupFor2017()
        {
            // set public holidays with variable dates for 2017
            holidayCalendar[(int)Month.April, 14] = true; // good friday
            holidayCalendar[(int)Month.April, 17] = true; // easter monday
            holidayCalendar[(int)Month.May, 25] = true; // ascension day
            holidayCalendar[(int)Month.June, 23] = true; // midsummer eve
            holidayCalendar[(int)Month.June, 24] = true; // midsummer day
        }

        public override bool IsPublicHoliday(DateTime date)
        {
            if(date.Year != 2017) throw new ArgumentException("Current holiday lookup supports year 2017.");
            return base.IsPublicHoliday(date);
        }
    }
}
