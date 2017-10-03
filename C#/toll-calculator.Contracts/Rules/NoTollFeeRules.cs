using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.Contracts.Rules
{
    /// <summary>
    /// Represents toll fee rules for days where no toll fee is applicable.
    /// </summary>
    public class NoTollFeeRules : ITollFeeRules
    {
        public bool IsTollFreeDate => true;

        public IEnumerable<string> DomesticIso3166CountryCodes => Enumerable.Empty<string>();

        public bool AreNonDomesticVehiclesTollFree => false;

        public IEnumerable<VehicleType> TollFreeVehicleTypes => Enumerable.Empty<VehicleType>();

        public IEnumerable<KeyValuePair<TimeSpan, decimal>> TollFeeOrderedByStartTime =>
            Enumerable.Empty<KeyValuePair<TimeSpan, decimal>>();

        public decimal? DailyMaximumTollFee => null;

        public int? NumberOfMinutesForSingleChargeRule => null;
    }
}
