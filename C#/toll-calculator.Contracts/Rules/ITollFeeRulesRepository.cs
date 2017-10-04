using System;

namespace TollCalculator.Contracts.Rules
{
    /// <summary>
    /// Repository for toll fee rules to use in toll fee calculations.
    /// </summary>
    public interface ITollFeeRulesRepository
    {
        /// <summary>
        /// Gets the ISO 4217 currency symbol (e.g. EUR, USD, SEK) used for fees.
        /// </summary>
        string Iso4217CurrencySymbol { get; }

        /// <summary>
        /// Gets toll fee rules for specific date.
        /// </summary>
        /// <param name="date">Date to get toll fee rules for.</param>
        /// <returns>Toll fee rules for specific date.</returns>
        ITollFeeRules GetTollFeeRulesForDate(DateTime date);
    }
}
