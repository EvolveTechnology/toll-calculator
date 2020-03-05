using System;
using System.Collections.Generic;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Models;
using Xunit;

namespace TollFeeCalculator.Test
{
    public class TollCalculatorTests
    {
        [Fact]
        public void WhenATollFreeVehicleTypeIsGivenCalculateTollFeeAsZero()
        {
            int expectedFee = 0;
            ITollCalculator dailyTollCalculator = new TollCalculator();
            var testDate = new DateTime[] {new DateTime(2010, 1, 1)};
            decimal fee = dailyTollCalculator.GetDailyTollFee(new Vehicle(VehicleType.Emergency), testDate);

            Assert.Equal(expectedFee, fee);
        }

        [Fact]
        public void WhenVehicleAndDatesGivenCalculateFeeAccordingToFeeStructure()
        {
            int expectedFee = 8;
            ITollCalculator dailyTollCalculator = new TollCalculator();
            var testDate = new DateTime[] {new DateTime(2010, 1, 1)};
            decimal fee = dailyTollCalculator.GetDailyTollFee(new Vehicle(VehicleType.Emergency), testDate);

            Assert.Equal(expectedFee, fee);

        }
    }
}
