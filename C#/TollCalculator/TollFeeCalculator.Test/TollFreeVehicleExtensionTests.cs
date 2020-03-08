using System;
using System.Collections.Generic;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Models;
using Xunit;

namespace TollFeeCalculator.Test
{
    public class TollFreeVehicleExtensionTests
    {
        public static IEnumerable<object[]> FreeVehicleTypes =>
            new List<object[]>
            {
                new object[] { VehicleType.Tractor, 0},
                new object[] { VehicleType.Motorbike, 0},
                new object[] { VehicleType.Diplomat, 0},
                new object[] { VehicleType.Emergency, 0},
                new object[] { VehicleType.Foreign, 0},
                new object[] { VehicleType.Military, 0},
                new object[] { CustomVehicleType.Bus, 0},
            };

        public static IEnumerable<object[]> NormalDatesNotFreeVehicleSinglePassData =>
            new List<object[]>
            {
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2020,02,20, 08, 35, 10)}, 8m},
                new object[] { new Vehicle(CustomVehicleType.Van), new DateTime[] {new DateTime(2020,01,10, 06, 33, 50)}, 13m},
                new object[] { new Vehicle(VehicleType.Car), new DateTime[] {new DateTime(2019,12,18, 16, 10, 10)}, 18m},
                new object[] { new Vehicle(CustomVehicleType.Van), new DateTime[] {new DateTime(2019,12,18, 02, 17, 10)}, 0m}
            };

        [Theory]
        [MemberData(nameof(FreeVehicleTypes))]
        public void WhenNormalDayFreeVehicleTypeIsGiven_CalculatesFeeAsZero(VehicleType vehicleType, decimal expectedFee)
        {
            // using the extended tollfreevehicles service with Bus as a free vehicle
            ITollCalculator dailyTollCalculator = new TollCalculator(new CustomTollFreeVehicles(),null,null,null );
           
            IVehicle vehicle = new Vehicle(vehicleType);
            DateTime[] passes = new[]{
                new DateTime(2020, 02, 13, 12, 10, 20)
            };

            var result = dailyTollCalculator.GetDailyTollFee(vehicle, passes);

            Assert.Equal(expectedFee, result);
        }

        [Theory]
        [MemberData(nameof(NormalDatesNotFreeVehicleSinglePassData))]
        public void WhenASingleNormalDateIsGiven_WithNonFreeVehicle_CalculatesCorrectFee(Vehicle vehicle, DateTime[] passes,
            decimal expectedFee)
        {
            ITollCalculator dailyTollCalculator = new TollCalculator(new CustomTollFreeVehicles(), null, null, null);

            var result = dailyTollCalculator.GetDailyTollFee(vehicle, passes);

            Assert.Equal(expectedFee, result);
        }
    }


    public class CustomVehicleType: VehicleType
    {
        public static readonly VehicleType Van = new CustomVehicleType("Van");
        public static readonly VehicleType Bus = new CustomVehicleType("Bus");

        public CustomVehicleType(string name) : base(name)
        {
        }
    }

    public class CustomTollFreeVehicles : ITollFreeVehicles
    {
        private readonly List<VehicleType> _tollFreeVehicles = new List<VehicleType>
        {
            VehicleType.Motorbike,
            VehicleType.Tractor,
            VehicleType.Emergency,
            VehicleType.Diplomat,
            VehicleType.Foreign,
            VehicleType.Military,
            CustomVehicleType.Bus
        };

        public bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null) return false;

            return _tollFreeVehicles.Contains(vehicle.GetVehicleType());
        }
    }
}
