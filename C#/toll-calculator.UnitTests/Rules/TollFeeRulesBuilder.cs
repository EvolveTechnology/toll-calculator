using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculator.Contracts.Rules;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.UnitTests.Rules
{
    internal class TollFeeRulesBuilder
    {
        private TestTollFeeRules Rules { get; }

        private List<(int hour, int minute, decimal fee)> Fees { get; } 

        private TollFeeRulesBuilder(TestTollFeeRules rules)
        {
            Rules = rules;
            Fees = new List<(int hour, int minute, decimal fee)>();
        }

        public TollFeeRulesBuilder IsTollFree()
        {
            Rules.IsTollFreeDate = true;
            return this;
        }

        public TollFeeRulesBuilder WithDailyMaximumTollFee(decimal? maximumFee)
        {
            Rules.DailyMaximumTollFee = maximumFee;
            return this;
        }

        public TollFeeRulesBuilder WithSingleChargeMinutes(int? minutesForSingleCharge)
        {
            Rules.NumberOfMinutesForSingleChargeRule = minutesForSingleCharge;
            return this;
        }

        public TollFeeRulesBuilder WithDomesticCountryCodes(params string[] countryCodes)
        {
            Rules.DomesticIso3166CountryCodes = countryCodes;
            return this;
        }

        public TollFeeRulesBuilder WithNonDomesticVehiclesTollFree(bool nonDomesticTollFree)
        {
            Rules.AreNonDomesticVehiclesTollFree = nonDomesticTollFree;
            return this;
        }

        public TollFeeRulesBuilder WithTollFreeVehicleTypes(params VehicleType[] vehicleTypes)
        {
            Rules.TollFreeVehicleTypes = vehicleTypes;
            return this;
        }

        public TollFeeRulesBuilder WithTollFee(int hour, int minute, decimal fee)
        {
            Fees.Add((hour, minute, fee));
            return this;
        }

        public ITollFeeRules Build()
        {
            if (Fees.Any())
            {
                Rules.TollFeeOrderedByStartTime = Fees
                    .ToDictionary(item => new TimeSpan(item.hour, item.minute, 0), item => item.fee)
                    .OrderBy(kvp => kvp.Key);
            }

            return Rules;
        }

        public static TollFeeRulesBuilder SimpleTollRulesCharge1NoMax()
        {
            return new TollFeeRulesBuilder(new TestTollFeeRules()
            {
                IsTollFreeDate = false,
                AreNonDomesticVehiclesTollFree = true,
                DomesticIso3166CountryCodes = new[] { "SE" },
                DailyMaximumTollFee = null,
                NumberOfMinutesForSingleChargeRule = null,
                TollFreeVehicleTypes = new VehicleType[0],
                TollFeeOrderedByStartTime = new KeyValuePair<TimeSpan, decimal>[]
                {
                    new KeyValuePair<TimeSpan, decimal>(new TimeSpan(0, 0, 0), 1)
                }
            });
        }
    }
}
