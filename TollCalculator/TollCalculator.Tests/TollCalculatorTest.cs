using System;
using System.Collections.Generic;
using TollCalculator.Application.Models;
using Xunit;

namespace TollCalculator.Tests
{
    public class TollCalculatorTest
    {
        [Fact]
        public void GetTollFee_NoParam()
        {
            var calculator = new TollCalculator();
            Assert.Throws<ArgumentNullException>(
                () => calculator.GetTollFeePerDay(null, new List<DateTime> {new DateTime(2020,08,25,7,0,0)}));
            Assert.Throws<ArgumentNullException>(() =>
                calculator.GetTollFeePerDay(new Vehicle {VehicleType = VehicleType.Car}, null));
        }

        [Fact]
        public void GetTollFee_TollFreeVehicle()
        {
            var calculator = new TollCalculator();
            var toll = calculator.GetTollFeePerDay(new Vehicle{VehicleType = VehicleType.Motorbike}, new List<DateTime> { new DateTime(2020, 08, 25, 7, 0, 0) });
            Assert.Equal(0, toll);
        }

        [Fact]
        public void GetTollFee_TollFreeDay()
        {
            var calculator = new TollCalculator();
            var toll = calculator.GetTollFeePerDay(new Vehicle { VehicleType = VehicleType.Car }, new List<DateTime> { new DateTime(2020, 12, 31, 7, 0, 0) });
            Assert.Equal(0, toll);
        }

        [Fact]
        public void GetTollFee_TollMaxPerHour()
        {
            var calculator = new TollCalculator();
            var toll = calculator.GetTollFeePerDay(new Vehicle { VehicleType = VehicleType.Car }, new List<DateTime> { new DateTime(2020, 08, 25, 6, 10, 0), new DateTime(2020, 08, 25, 6, 40, 0) });
            Assert.Equal(13, toll);
        }

        [Fact]
        public void GetTollFee_TollSum()
        {
            var calculator = new TollCalculator();
            var toll = calculator.GetTollFeePerDay(new Vehicle { VehicleType = VehicleType.Car }, new List<DateTime> { new DateTime(2020, 08, 25, 6, 10, 0), new DateTime(2020, 08, 25, 7, 40, 0) });
            Assert.Equal(26, toll);
        }

        [Fact]
        public void GetTollFee_TollMaxSum()
        {
            var maximumCrossing = new List<DateTime>
            {
                new DateTime(2020, 08, 25, 6, 10, 0),
                new DateTime(2020, 08, 25, 7, 40, 0),
                new DateTime(2020, 08, 25, 8, 40, 0),
                new DateTime(2020, 08, 25, 9, 40, 0),
                new DateTime(2020, 08, 25, 10, 40, 0),
                new DateTime(2020, 08, 25, 11, 40, 0),
                new DateTime(2020, 08, 25, 12, 40, 0),
                new DateTime(2020, 08, 25, 13, 40, 0),
                new DateTime(2020, 08, 25, 14, 40, 0),
                new DateTime(2020, 08, 25, 15, 40, 0),
                new DateTime(2020, 08, 25, 16, 40, 0),
                new DateTime(2020, 08, 25, 17, 40, 0),

            };
            var calculator = new TollCalculator();
            var toll = calculator.GetTollFeePerDay(new Vehicle { VehicleType = VehicleType.Car },maximumCrossing );
            Assert.Equal(60, toll);
        }

        [Fact]
        public void GetTollFee_TollFreeAfterHour()
        {
            var calculator = new TollCalculator();
            var toll = calculator.GetTollFeePerDay(new Vehicle { VehicleType = VehicleType.Car }, new List<DateTime> { new DateTime(2020, 12, 31, 19, 0, 0) });
            Assert.Equal(0, toll);
        }

        [Fact]
        public void GetTollFee_ThrowException()
        {
            var calculator = new TollCalculator();
            Assert.Throws<ArgumentException>(
                () => calculator.GetTollFeePerDay(new Vehicle { VehicleType = VehicleType.Car }, new List<DateTime> { new DateTime(2020, 2, 1, 11, 0, 0), new DateTime(2020, 4, 1, 11, 0, 0) }));
        }
    }
}