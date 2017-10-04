using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator
{
    /// <summary>
    /// Result of toll fee calculation.
    /// </summary>
    public class TollFeeResult
    {
        /// <summary>
        /// Creates new toll fee result.
        /// </summary>
        /// <param name="vehicle">Vehicle to create toll fee result for.</param>
        /// <param name="dailyTollFees">List of daily toll fee results.</param>
        /// <remarks>
        /// If several daily toll fee results are provided for same day, they are
        /// simply added; no filtering of results is done.
        /// </remarks>
        public TollFeeResult(Vehicle vehicle, IEnumerable<DailyTollFee> dailyTollFees)
        {
            // If no vehicle specified, throw exception
            Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));

            DailyTollFees = new List<DailyTollFee>(
                dailyTollFees ?? Enumerable.Empty<DailyTollFee>());
            
            TotalTaxableAmount = DailyTollFees.Sum(dailyFee => dailyFee.TaxableAmount);
        }

        /// <summary>
        /// Gets vehicle that toll fee applies for.
        /// </summary>
        public Vehicle Vehicle { get; }

        /// <summary>
        /// Gets all daily toll fees and taxable amounts included.
        /// </summary>
        public IEnumerable<DailyTollFee> DailyTollFees { get; }

        /// <summary>
        /// Gets total taxable amount in local currency.
        /// </summary>
        public decimal TotalTaxableAmount { get; }
    }
}
