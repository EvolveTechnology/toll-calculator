using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;
using System.Collections.Generic;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class TollFeeCalculatorUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "The list of times provided in the argument is empty")]
        public void TollFeeCalculator_emptyTimesListInput_ArgumentException()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Car;
            input.Date = new DateTime(2017 - 6 - 19);
            input.Times = new List<TimeSpan>();
            TollFeeCalc t = new TollFeeCalc(input);
        }

        [TestMethod]
        public void TollFeeCalculator_OnePassDuringRushHour_returnHighestFee()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Car;
            input.Date = new DateTime(2013, 1, 2);
            input.Times = new List<TimeSpan>() { new TimeSpan(7, 30, 0) };
            TollFeeCalc t = new TollFeeCalc(input);
            Assert.AreEqual(t.Calc(), Settings.FEE_HIGHEST);
        }

        [TestMethod]
        public void TollFeeCalculator_TollFreeVehicle_return0()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Diplomat;
            input.Date = new DateTime(2013, 1, 2);
            input.Times = new List<TimeSpan>() { new TimeSpan(7, 30, 0) };
            TollFeeCalc t = new TollFeeCalc(input);
            Assert.AreEqual(t.Calc(), 0);
        }

        [TestMethod]
        public void TollFeeCalculator_TwoPassesDuringRushHour_returnTwoTimesHighestFee()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Car;
            input.Date = new DateTime(2013, 1, 2);
            input.Times = new List<TimeSpan>() { new TimeSpan(7, 30, 0) , new TimeSpan(16, 30, 0)};
            TollFeeCalc t = new TollFeeCalc(input);
            Assert.AreEqual(t.Calc(), 2*Settings.FEE_HIGHEST);
        }

        [TestMethod]
        public void TollFeeCalculator_TwoPassesDuringRushHourButNotSortedTimestamps_returnTwoTimesHighestFee()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Car;
            input.Date = new DateTime(2013, 1, 2);
            input.Times = new List<TimeSpan>() { new TimeSpan(16, 30, 0), new TimeSpan(7, 30, 0) };
            TollFeeCalc t = new TollFeeCalc(input);
            Assert.AreEqual(t.Calc(), 2 * Settings.FEE_HIGHEST);
        }

        [TestMethod]
        public void TollFeeCalculator_TwoPassesWithinOneHour_returnOnlyOneHighestFee()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Car;
            input.Date = new DateTime(2013, 1, 2);
            input.Times = new List<TimeSpan>() { new TimeSpan(7, 30, 0), new TimeSpan(7, 40, 0) };
            TollFeeCalc t = new TollFeeCalc(input);
            Assert.AreEqual(t.Calc(), Settings.FEE_HIGHEST);
        }

        [TestMethod]
        public void TollFeeCalculator_OnePassDuringRushHourAndOneDuringMediumTraffic_returnHighestFeePlusHighFee()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Car;
            input.Date = new DateTime(2013, 1, 2);
            input.Times = new List<TimeSpan>() { new TimeSpan(6, 40, 0), new TimeSpan(16, 40, 0) };
            TollFeeCalc t = new TollFeeCalc(input);
            Assert.AreEqual(t.Calc(), Settings.FEE_HIGH + Settings.FEE_HIGHEST);
        }

        [TestMethod]
        public void TollFeeCalculator_manyPassesDuringOneDay_returnMaximumFee()
        {
            TollGatePassageData input;
            input.Vehicle = VehicleType.Car;
            input.Date = new DateTime(2013, 1, 2);
            input.Times = new List<TimeSpan>() { new TimeSpan(6, 40, 0), new TimeSpan(16, 40, 0), new TimeSpan(7,50,0), new TimeSpan(12, 50, 0), new TimeSpan(15, 10, 0), new TimeSpan(10, 50, 0), new TimeSpan(13, 50, 0), new TimeSpan(8, 50, 0) };
            TollFeeCalc t = new TollFeeCalc(input);
            Assert.AreEqual(t.Calc(), Settings.MAXIMUM_FEE);
        }

        // Etc. Could try to reach 100% test coverage, but this only serves to show the process.

    }
}
