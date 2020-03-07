using System.Collections.Generic;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Models;
using TollFeeCalculator.Services;
using Xunit;

namespace TollFeeCalculator.Test
{
    public class TollFreeVehicleTests
    {
        public static IEnumerable<object[]> FreeVehicleTypes =>
            new List<object[]>
            {
                new object[] { VehicleType.Tractor},
                new object[] { VehicleType.Motorbike},
                new object[] { VehicleType.Diplomat},
                new object[] { VehicleType.Emergency},
                new object[] { VehicleType.Foreign},
                new object[] { VehicleType.Military}
            };

        public static IEnumerable<object[]> NotFreeVehicleTypes =>
            new List<object[]>
            {
                new object[] { VehicleType.Car}
            };

        [Theory]
        [MemberData(nameof(FreeVehicleTypes))]
        public void WhenTollFreeVehicleIsGiven_ReturnTrue(VehicleType vehicleType)
        {
            ITollFreeVehicles tollFreeVehicle = new TollFreeVehicles();

            IVehicle vehicle = new Vehicle(vehicleType);

            var result = tollFreeVehicle.IsTollFreeVehicle(vehicle);

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(NotFreeVehicleTypes))]
        public void WhenNotTollFreeVehicleIsGiven_ReturnFalse(VehicleType vehicleType)
        {
            ITollFreeVehicles tollFreeVehicle = new TollFreeVehicles();

            IVehicle vehicle = new Vehicle(vehicleType);

            var result = tollFreeVehicle.IsTollFreeVehicle(vehicle);

            Assert.False(result);
        }
    }
}
