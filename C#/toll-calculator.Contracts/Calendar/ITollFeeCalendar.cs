using System;
using System.Collections.Generic;
using System.Globalization;

namespace TollCalculator.Contracts.Calendar
{
    /// <summary>
    /// Toll fee calendar representing days that are toll free.
    /// </summary>
    public interface ITollFeeCalendar
    {
        /// <summary>
        /// Gets the culture for the calendar.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the calendar to use for dates.
        /// </summary>
        System.Globalization.Calendar Calendar { get; }

        /// <summary>
        /// Checks if date is toll free or not.
        /// </summary>
        /// <param name="date">Date to check.</param>
        /// <returns><b>True</b> if date is toll free, <b>false</b> otherwise.</returns>
        bool IsTollFree(DateTime date);

        /// <summary>
        /// Returns all toll free days for specific year (including weekends if applicable).
        /// </summary>
        /// <param name="year">Year to get toll free days for.</param>
        /// <returns>Ordered enumeration of toll free days.</returns>
        IEnumerable<DateTime> TollFreeDaysForYear(int year);
    }
}
