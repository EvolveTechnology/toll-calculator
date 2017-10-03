using System;
using System.Collections.Generic;
using TollCalculator.Contracts.Rules;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.Gothenburg
{
    /// <summary>
    /// Represents toll fee rules for Gothenburg.
    /// </summary>
    public class GothenburgTollFeeRules : ITollFeeRules
    {
        private static VehicleType[] TollFreeVehicles => new[]
        {
            VehicleType.Emergency,
            VehicleType.HeavyBus,
            VehicleType.Diplomat,
            VehicleType.Motorbike,
            VehicleType.Moped,
            VehicleType.Military,
            VehicleType.ECMobileCrane,
            VehicleType.Tractor
        };

        private static string[] CountryCode => new[] { "SE" };

        /// <summary>
        /// Gets Gothenburg toll fee rules applicable from 2015 onwards.
        /// </summary>
        public static GothenburgTollFeeRules Rules2015 => new GothenburgTollFeeRules()
        {
            AreNonDomesticVehiclesTollFree = false,
            TollFreeVehicleTypes = TollFreeVehicles,
            DailyMaximumTollFee = 60,
            NumberOfMinutesForSingleChargeRule = 60,
            TollFeeOrderedByStartTime = new Dictionary<TimeSpan, decimal>()
                    {
                        { new TimeSpan(06, 00, 0), 9 },
                        { new TimeSpan(06, 30, 0), 16 },
                        { new TimeSpan(07, 00, 0), 22 },
                        { new TimeSpan(08, 00, 0), 16 },
                        { new TimeSpan(08, 30, 0), 9 },
                        { new TimeSpan(15, 00, 0), 16 },
                        { new TimeSpan(15, 30, 0), 22 },
                        { new TimeSpan(17, 00, 0), 16 },
                        { new TimeSpan(18, 00, 0), 9 },
                        { new TimeSpan(18, 30, 0), 0 }
                    }
        };

        /// <summary>
        /// Gets Gothenburg toll fee rules applicable for 2014.
        /// </summary>
        public static GothenburgTollFeeRules Rules2014 => new GothenburgTollFeeRules()
        {
            AreNonDomesticVehiclesTollFree = false,
            TollFreeVehicleTypes = TollFreeVehicles,
            DailyMaximumTollFee = 60,
            NumberOfMinutesForSingleChargeRule = 60,
            TollFeeOrderedByStartTime = new Dictionary<TimeSpan, decimal>()
                    {
                        { new TimeSpan(06, 00, 0), 8 },
                        { new TimeSpan(06, 30, 0), 13 },
                        { new TimeSpan(07, 00, 0), 18 },
                        { new TimeSpan(08, 00, 0), 13 },
                        { new TimeSpan(08, 30, 0), 8 },
                        { new TimeSpan(15, 00, 0), 13 },
                        { new TimeSpan(15, 30, 0), 18 },
                        { new TimeSpan(17, 00, 0), 13 },
                        { new TimeSpan(18, 00, 0), 8 },
                        { new TimeSpan(18, 30, 0), 0 }
                    }
        };

        /// <summary>
        /// Gets Gothenburg toll fee rules applicable for 2013.
        /// </summary>
        public static GothenburgTollFeeRules Rules2013 => new GothenburgTollFeeRules()
        {
            AreNonDomesticVehiclesTollFree = true,
            TollFreeVehicleTypes = TollFreeVehicles,
            DailyMaximumTollFee = 60,
            NumberOfMinutesForSingleChargeRule = 60,
            TollFeeOrderedByStartTime = new Dictionary<TimeSpan, decimal>()
                    {
                        { new TimeSpan(06, 00, 0), 8 },
                        { new TimeSpan(06, 30, 0), 13 },
                        { new TimeSpan(07, 00, 0), 18 },
                        { new TimeSpan(08, 00, 0), 13 },
                        { new TimeSpan(08, 30, 0), 8 },
                        { new TimeSpan(15, 00, 0), 13 },
                        { new TimeSpan(15, 30, 0), 18 },
                        { new TimeSpan(17, 00, 0), 13 },
                        { new TimeSpan(18, 00, 0), 8 },
                        { new TimeSpan(18, 30, 0), 0 }
                    }
        };

        public bool IsTollFreeDate => false;

        public IEnumerable<string> DomesticIso3166CountryCodes => CountryCode;

        public bool AreNonDomesticVehiclesTollFree { get; set; }

        public IEnumerable<VehicleType> TollFreeVehicleTypes { get; set; }

        public IEnumerable<KeyValuePair<TimeSpan, decimal>> TollFeeOrderedByStartTime { get; set; }

        public decimal? DailyMaximumTollFee { get; set; }

        public int? NumberOfMinutesForSingleChargeRule { get; set; }
    }
}
