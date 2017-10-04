using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.UnitTests
{
    [TestClass]
    public class TollFeeResultTests
    {
        private Vehicle Vehicle { get; set; }

        private TollFeeResult Sut { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Vehicle = new Vehicle(string.Empty, "SE", VehicleType.Car);
        }

        [TestMethod]
        public void TollFeeResult_WithMultipleDailyResults_TaxableAmountsAreCombined()
        {
            // Arrange
            const decimal expected = 3;
            Sut = new TollFeeResult(Vehicle, new[]
            {
                new DailyTollFee(new DateTime(2017, 9, 16), 0, 0, 1),
                new DailyTollFee(new DateTime(2017, 9, 17), 0, 0, 2)
            });

            // Act
            var actual = Sut.TotalTaxableAmount;

            // Assert
            Assert.AreEqual(expected, actual,
                "TotalTaxableAmountInSek was not as expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
            "Expected ArgumentNullException not thrown for vehicle == NULL.")]
        public void TollFeeResult_NullVehicle_ThrowsArgumentNullException()
        {
            // Arrange & Act
            Sut = new TollFeeResult(null, new List<DailyTollFee>());
        }

        [TestMethod]
        public void TollFeeResult_NullDailyTollFees_DoesNotThrowException()
        {
            // Arrange & Act
            Sut = new TollFeeResult(Vehicle, null);

            // Assert
            // Exception is not thrown.
        }
    }
}
