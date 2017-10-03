using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TollCalculator.Contracts.Calendar;
using TollCalculator.Contracts.Rules;

namespace TollCalculator.Gothenburg
{
    /// <summary>
    /// Repository for Gothenburg toll fee rules.
    /// </summary>
    public class GothenburgTollFeeRepository : ITollFeeRulesRepository
    {
        private static ITollFeeRules NoToll => new NoTollFeeRules();

        private static Calendar SwedishCalendar => CultureInfo.CreateSpecificCulture("sv-SE").Calendar;

        // Toll fee rules sorted in descending order by start date
        private static SortedDictionary<DateTime, ITollFeeRules> TollFeeRules =
            new SortedDictionary<DateTime, ITollFeeRules>(new DescendingDateTimeComparer())
        {
            { new DateTime(2013, 1, 1), GothenburgTollFeeRules.Rules2013 },
            { new DateTime(2014, 1, 1), GothenburgTollFeeRules.Rules2014 },
            { new DateTime(2015, 1, 1), GothenburgTollFeeRules.Rules2015 }
        };

        private static DateTime CongestionTaxStartDate => TollFeeRules.Keys.Min();

        private ITollFeeCalendar TollFeeCalendar { get; } 

        private ILogger Logger { get; }

        /// <summary>
        /// Creates repository for Gothenburg toll fee rules.
        /// </summary>
        /// <param name="tollFeeCalendar">Calendar to use.</param>
        /// <param name="logger">Optional logger to use.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="tollFeeCalendar"/> is <b>null</b>.
        /// </exception>
        public GothenburgTollFeeRepository(ITollFeeCalendar tollFeeCalendar, ILogger logger)
        {
            TollFeeCalendar = tollFeeCalendar ??
                throw new ArgumentNullException(nameof(tollFeeCalendar));
            Logger = logger ?? NullLogger.Instance;
        }

        /// <summary>
        /// Gets ISO 4217 currency symbol for Swedish currency.
        /// </summary>
        public string Iso4217CurrencySymbol => new RegionInfo("sv-SE").ISOCurrencySymbol;

        /// <summary>
        /// Gets toll fee rules for specific date.
        /// </summary>
        /// <param name="date">Date to get rules for.</param>
        /// <returns>Toll fee rules to use for date.</returns>
        public ITollFeeRules GetTollFeeRulesForDate(DateTime date)
        {
            if (date.Date < CongestionTaxStartDate)
            {
                Logger.LogInformation($"{date.Date:d} is before start of congestion tax ({CongestionTaxStartDate:d}).");
                return NoToll;
            }

            if (TollFeeCalendar.IsTollFree(date))
            {
                return NoToll;
            }

            return TollFeeRules.First(rule => rule.Key <= date.Date).Value;
        }
    }
}
