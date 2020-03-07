using System;
using System.Collections.Generic;
using System.Text;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Models;
using TollFeeCalculator.Services;
using Xunit;

namespace TollFeeCalculator.Test
{
    public class TollFreeVehicleTests
    {
        [Theory]
        [InlineData(VehicleType.Tractor)]
        [InlineData(VehicleType.Motorbike)]
        [InlineData(VehicleType.Diplomat)]
        [InlineData(VehicleType.Emergency)]
        [InlineData(VehicleType.Foreign)]
        [InlineData(VehicleType.Military)]
        public void WhenTollFreeVehicleIsGiven_ReturnTrue(VehicleType vehicleType)
        {
            ITollFreeVehicles tollFreeVehicle = new TollFreeVehicles();

            IVehicle vehicle = new Vehicle(vehicleType);

            var result = tollFreeVehicle.IsTollFreeVehicle(vehicle);

            Assert.True(result);
        }

        [Theory]
        [InlineData(VehicleType.Car)]
        public void WhenNotTollFreeVehicleIsGiven_ReturnFalse(VehicleType vehicleType)
        {
            ITollFreeVehicles tollFreeVehicle = new TollFreeVehicles();

            IVehicle vehicle = new Vehicle(vehicleType);

            var result = tollFreeVehicle.IsTollFreeVehicle(vehicle);

            Assert.False(result);
        }
    }
}
