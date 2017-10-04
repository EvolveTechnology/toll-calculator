using System;
using System.Collections.Generic;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.Contracts.Rules
{
    /// <summary>
    /// Rules for calculating toll fees.
    /// </summary>
    /// <remarks>
    /// When using toll rules, always check <see cref="IsTollFreeDate"/>
    /// first to see if toll fee is applicable at all.
    /// </remarks>
    public interface ITollFeeRules
    {
        /// <summary>
        /// Gets whether toll fee is applicable or not.
        /// </summary>
        bool IsTollFreeDate { get; }

        /// <summary>
        /// Gets enumeration of ISO 3166 two letter country codes
        /// that are considered domestic vehicles.
        /// </summary>
        IEnumerable<string> DomesticIso3166CountryCodes { get; }

        /// <summary>
        /// Gets whether non-domestic vehicles are toll free or not.
        /// </summary>
        bool AreNonDomesticVehiclesTollFree { get; }

        /// <summary>
        /// Gets list of vehicle types that are toll free.
        /// </summary>
        IEnumerable<VehicleType> TollFreeVehicleTypes { get; }

        /// <summary>
        /// Gets ordered enumeration of time spans with toll fees.
        /// </summary>
        IEnumerable<KeyValuePair<TimeSpan, decimal>> TollFeeOrderedByStartTime { get; }

        /// <summary>
        /// Gets optional daily maximum toll fee.
        /// </summary>
        decimal? DailyMaximumTollFee { get; }

        /// <summary>
        /// Gets optional number of minutes for single charge rule.
        /// <b>Null</b> means there is no single charge rule.
        /// </summary>
        /// <remarks>
        /// Single charge rule means that only the most expensive charge
        /// for the period of minutes mentioned will be considered.
        /// </remarks>
        int? NumberOfMinutesForSingleChargeRule { get; }
    }
}
