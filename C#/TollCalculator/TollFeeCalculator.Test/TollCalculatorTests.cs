using System;
using System.Collections.Generic;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Models;
using Xunit;

namespace TollFeeCalculator.Test
{
    public class TollCalculatorTests
    {
        public static IEnumerable<object[]> NormalDatesNotFreeVehicleSinglePassData =>
            new List<object[]>
            {
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2020,02,20, 08, 35, 10)}, 8m},
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2020,01,10, 06, 33, 50)}, 13m},
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2019,12,18, 16, 10, 10)}, 18m},
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2019,12,18, 02, 17, 10)}, 0m}
            };

        public static IEnumerable<object[]> NormalDaatesMixedFreeVehicleSinglePassData =>
            new List<object[]>
            {
                new object[] { new Vehicle(VehicleType.Diplomat), new DateTime[] {new DateTime(2020,02,20, 08, 35, 10)}, 0m},
                new object[] { new Vehicle(VehicleType.Emergency), new DateTime[] {new DateTime(2020,01,10, 06, 33, 50)}, 0m},
                new object[] { new Vehicle(VehicleType.Military), new DateTime[] {new DateTime(2019,12,18, 16, 10, 10)}, 0m},
                new object[] { new Vehicle(VehicleType.Foreign), new DateTime[] {new DateTime(2019,12,18, 02, 17, 10)}, 0m}
            };

        public static IEnumerable<object[]> NormalDayMultiplePassesInOneHourWithDifferentFeeStructures =>
            new List<object[]>
            {
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2020,02,20, 06, 45, 50) //13
                                                                                  ,new DateTime(2020,02,20, 07, 23, 50) //18
                                                                                  ,new DateTime(2020,02,20, 08, 12, 10) //13
                                                                                  ,new DateTime(2020,02,20, 08, 35, 10)}, //08
                                31m} // 18 (second pass) + 13 (Third pass) = 31
            };

        public static IEnumerable<object[]> HolidayMixVehiclesSinglePassData =>
            new List<object[]>
            {
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2019,07,19, 08, 35, 10)}, 0m},
                new object[] { new Vehicle(VehicleType.Car), new DateTime[]
                                                            {
                                                                new DateTime(2020,07,19, 09, 33, 50),
                                                                new DateTime(2020,07,19, 10, 15, 50)
                                                            }, 0m},
                new object[] { new Vehicle(VehicleType.Foreign), new DateTime[] {new DateTime(2019,07,19, 10, 10, 10)}, 0m},
                
            };

        [Theory]
        [MemberData(nameof(NormalDatesNotFreeVehicleSinglePassData))]
        public void WhenASingleNormalDateIsGiven_WithNonFreeVehicle_CalculatesCorrectFee(Vehicle vehicle, DateTime[] passes,
            decimal expectedFee)
        {
            ITollCalculator dailyTollCalculator = new TollCalculator();

            var result = dailyTollCalculator.GetDailyTollFee(vehicle, passes);

            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [MemberData(nameof(NormalDaatesMixedFreeVehicleSinglePassData))]
        public void WhenASingleNormalDateIsGiven_WithFreeVehicle_CalculatesFeeAsZero(Vehicle vehicle, DateTime[] passes,
            decimal expectedFee)
        {
            ITollCalculator dailyTollCalculator = new TollCalculator();

            var result = dailyTollCalculator.GetDailyTollFee(vehicle, passes);

            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [MemberData(nameof(NormalDayMultiplePassesInOneHourWithDifferentFeeStructures))]
        public void WhenANormalDateWithDifferentFeesInOneHourIsGiven_CalculatesFeeTakingTheHighestFeeForHour(Vehicle vehicle, DateTime[] passes,
            decimal expectedFee)
        {
            ITollCalculator dailyTollCalculator = new TollCalculator();

            var result = dailyTollCalculator.GetDailyTollFee(vehicle, passes);

            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [MemberData(nameof(NormalDayMultiplePassesInOneHourWithDifferentFeeStructures))]
        public void WhenAHolidayWithDifferentTypeVehiclesIsGiven_CalculatesFeeAsZero(Vehicle vehicle, DateTime[] passes,
            decimal expectedFee)
        {
            ITollCalculator dailyTollCalculator = new TollCalculator();

            var result = dailyTollCalculator.GetDailyTollFee(vehicle, passes);

            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [InlineData(VehicleType.Tractor, 0)]
        [InlineData(VehicleType.Motorbike, 0)]
        [InlineData(VehicleType.Diplomat, 0)]
        [InlineData(VehicleType.Emergency, 0)]
        [InlineData(VehicleType.Foreign, 0)]
        [InlineData(VehicleType.Military, 0)]
        public void WhenNormalDayFreeVehicleTypeIsGiven_CalculatesFeeAsZero(VehicleType vehicleType, decimal expectedFee)
        {
            ITollCalculator dailyTollCalculator = new TollCalculator();
            IVehicle vehicle = new Vehicle(vehicleType);
            DateTime[] passes = new DateTime[]{
                new DateTime(2020, 02, 13, 12, 10, 20)
            };

            var result = dailyTollCalculator.GetDailyTollFee(vehicle, passes);

            Assert.Equal(expectedFee, result);
        }

        [Fact] 
        public void WhenNullVehicleTypeIsGiven_ThrowExceptiono()
        {
            ITollCalculator dailyTollCalculator = new TollCalculator();
           
            DateTime[] passes = new DateTime[]{
                new DateTime(2020, 02, 13, 12, 10, 20)
            };

           Action act = () => dailyTollCalculator.GetDailyTollFee(null, passes);

            Assert.Throws<ArgumentNullException>(act);
        }


    }
}
