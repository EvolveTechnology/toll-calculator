using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using toll_calculator;

namespace IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        private TollCalculator _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new TollCalculator(new List<string>() {"year_2013.json" });
        }


        [DataTestMethod]
        [DataRow(VehicleType.Car, 36)]
        [DataRow(VehicleType.Diplomat, 0)]
        [DataRow(VehicleType.Emergency, 0)]
        [DataRow(VehicleType.Foreign, 0)]
        [DataRow(VehicleType.Military, 0)]
        [DataRow(VehicleType.Motorbike, 0)]
        [DataRow(VehicleType.Tractor, 0)]
        [DataRow(VehicleType.Unknown, 36)]
        public void ReturnsCorrectValueForTollFeeDay(VehicleType type, int expectedResult)
        {
            var passes = DateFeeder.GetPassesWithTollTimes();
            var result = _sut.GetTollFee(type, DateFeeder.GetPassesWithTollTimes().ToArray());
            result.Should().Be(expectedResult);
        }


        [DataTestMethod]
        [DataRow(VehicleType.Car, 0)]
        [DataRow(VehicleType.Diplomat, 0)]
        [DataRow(VehicleType.Emergency, 0)]
        [DataRow(VehicleType.Foreign, 0)]
        [DataRow(VehicleType.Military, 0)]
        [DataRow(VehicleType.Motorbike, 0)]
        [DataRow(VehicleType.Tractor, 0)]
        [DataRow(VehicleType.Unknown, 0)]
        public void WhenWhenTollFreeDay_returnsAllZero(VehicleType type, int expectedResult)
        {
            var passes = DateFeeder.GetPassesWithTollTimes();
            var result = _sut.GetTollFee(type, DateFeeder.GetPassesWithTollTimesOnTollFreeDay().ToArray());
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void TooLongDateRange_ThrowsInvalidDateRangeException()
        {
            var passes = DateFeeder.GetPassesWithTollTimes();
            var result = _sut.GetTollFee(VehicleType.Foreign, DateFeeder.GetPassesWithTollTimesOnDifferebtDays().ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void NoDateRange_ThrowsInvalidDateRangeException()
        {
            var passes = DateFeeder.GetPassesWithTollTimes();
            var result = _sut.GetTollFee(VehicleType.Foreign, new DateTime[0]); ;
        }
    }
}
