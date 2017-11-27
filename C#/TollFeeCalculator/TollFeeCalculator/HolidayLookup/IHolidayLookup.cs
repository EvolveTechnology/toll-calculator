using System;

namespace TollFeeCalculator.HolidayLookup
{
    /// <summary>
    /// Provides look ups for different public holidays.
    /// This is a very general API that allows for implementations to be configured 
    /// for a specific country and a specific year (some holidays are variable from year to year) 
    /// without affecting the consuming client.
    /// </summary>
    public interface IHolidayLookup
    {
        /// <summary>
        /// Returns true if a certain date is a public holiday.
        /// </summary>
        bool IsPublicHoliday(DateTime date);
    }
}
