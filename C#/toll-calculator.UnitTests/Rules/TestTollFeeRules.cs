using System;
using System.Collections.Generic;
using TollCalculator.Contracts.Rules;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.UnitTests.Rules
{
    internal class TestTollFeeRules : ITollFeeRules
    {
        public bool IsTollFreeDate { get; set; }

        public IEnumerable<string> DomesticIso3166CountryCodes { get; set; }

        public bool AreNonDomesticVehiclesTollFree { get; set; }

        public IEnumerable<VehicleType> TollFreeVehicleTypes { get; set; }

        public IEnumerable<KeyValuePair<TimeSpan, decimal>> TollFeeOrderedByStartTime { get; set; }

        public decimal? DailyMaximumTollFee { get; set; }

        public int? NumberOfMinutesForSingleChargeRule { get; set; }
    }
}
