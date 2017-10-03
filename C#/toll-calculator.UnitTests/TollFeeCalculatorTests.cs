using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TollCalculator.Contracts.Rules;
using TollCalculator.Contracts.Vehicles;
using TollCalculator.UnitTests.Rules;

namespace TollCalculator.UnitTests
{
    [TestClass]
    public class TollFeeCalculatorTests
    {
        private static CultureInfo SwedishCulture => CultureInfo.CreateSpecificCulture("sv-SE");
        private Mock<ITollFeeRulesRepository> RulesRepositoryMock { get; set; }
        private ITollFeeRules CurrentRules { get; set; }
        private Vehicle Vehicle { get; set; }
        private TollFeeCalculator Sut { get; set; }

        [TestInitialize]
        public void Setup()
        {
            RulesRepositoryMock = new Mock<ITollFeeRulesRepository>();
            RulesRepositoryMock
                .Setup(x => x.GetTollFeeRulesForDate(It.IsAny<DateTime>()))
                .Returns(() => CurrentRules);

            Vehicle = new Vehicle("ABC012", "SE", VehicleType.Car);

            Sut = new TollFeeCalculator(RulesRepositoryMock.Object, null);
        }

        /// <summary>
        /// Test single day report with same charge all day.
        /// </summary>
        /// <param name="expected">Expected toll fee and taxable fee.</param>
        /// <param name="p1">First passage.</param>
        /// <param name="p2">Second passage.</param>
        [DataRow(1, "2017-10-01 07:30", null, DisplayName = "SinglePassage")]
        [DataRow(2, "2017-10-01 07:30", "2017-10-01 15:00", DisplayName = "Two Passages")]
        [DataRow(2, "2017-10-01 15:00", "2017-10-01 07:30", DisplayName = "Two Passages (Reverse order)")]
        [DataRow(1, "2017-10-01 07:30", "2017-10-02 15:00", DisplayName = "Single Passage + Passage Next Day")]
        [DataRow(2, "2017-10-01 07:30", "2017-10-01 07:30", DisplayName = "Two Simultaneous Passages")]
        [DataTestMethod]
        public void CalculateDailyTollFee_SameChargeAllDay(int expected, string p1, string p2)
        {
            // Arrange
            IEnumerable<DateTime> formattedPassages = new[] { p1, p2 }
                .Where(p => p != null)
                .Select(p => DateTime.Parse(p, SwedishCulture));

            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .Build();

            DateTime expectedDate = formattedPassages.First().Date;
            int expectedNumberOfPassages = formattedPassages.Count(p => p.Date == expectedDate);

            // Act & Assert
            ValidateToll(
                passages: formattedPassages,
                expected: new DailyTollFee(expectedDate, expectedNumberOfPassages, expected, expected));
        }

        /// <summary>
        /// Test single day report with variable charge during the day.
        /// </summary>
        /// <param name="expected">Expected toll fee and taxable fee.</param>
        /// <param name="p1">First passage.</param>
        /// <param name="p2">Second passage.</param>
        /// <param name="p3">Third passage.</param>
        [DataRow(1, "2017-10-01 07:00", null, null, DisplayName = "Single Low-Charge Passage")]
        [DataRow(2, "2017-10-01 08:00", null, null, DisplayName = "Single High-Charge Passage")]
        [DataRow(1, "2017-10-01 19:00", null, null, DisplayName = "Single Low-Charge Passage 2")]
        [DataRow(4, "2017-10-01 07:00", "2017-10-01 08:00", "2017-10-01 09:00", DisplayName = "Low-High-Low Charge Passages")]
        [DataRow(4, "2017-10-01 08:00", "2017-10-01 09:00", "2017-10-01 07:00", DisplayName = "Low-High-Low Charge Passages (Wrong order)")]

        [DataTestMethod]
        public void CalculateDailyTollFee_VariableChargesDuringDay(int expected, string p1, string p2, string p3)
        {
            // Arrange
            IEnumerable<DateTime> formattedPassages = new[] { p1, p2, p3 }
                .Where(p => p != null)
                .Select(p => DateTime.Parse(p, SwedishCulture));

            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .WithTollFee(00, 00, 1)
                .WithTollFee(07, 30, 2)
                .WithTollFee(08, 30, 1)
                .Build();

            DateTime expectedDate = formattedPassages.First().Date;
            int expectedNumberOfPassages = formattedPassages.Count(p => p.Date == expectedDate);

            // Act & Assert
            ValidateToll(
                passages: formattedPassages,
                expected: new DailyTollFee(expectedDate, expectedNumberOfPassages, expected, expected));
        }

        /// <summary>
        /// Test single day report with max charge for the day.
        /// </summary>
        /// <param name="expectedTotal">Expected total fee.</param>
        /// <param name="expectedTaxable">Expected taxable fee.</param>
        /// <param name="p1">First passage.</param>
        /// <param name="p2">Second passage.</param>
        [DataRow(1, 1, "2017-10-01 07:00", null, DisplayName = "Single Low-Charge Passage")]
        [DataRow(2, 2, "2017-10-01 08:00", null, DisplayName = "Single High-Charge Passage")]
        [DataRow(2, 2, "2017-10-01 07:00", "2017-10-01 09:00", DisplayName = "Double Low-Charge Passages")]
        [DataRow(3, 2, "2017-10-01 07:00", "2017-10-01 08:00", DisplayName = "Max Charge Passages (Low-High)")]
        [DataRow(4, 2, "2017-10-01 07:30", "2017-10-01 08:00", DisplayName = "Max Charge Passages (High-High)")]
        [DataRow(3, 2, "2017-10-01 08:00", "2017-10-01 17:00", DisplayName = "Max Charge Passages (High-Low)")]
        [DataTestMethod]
        public void CalculateDailyTollFee_MaxChargesDuringDay(int expectedTotal, int expectedTaxable, string p1, string p2)
        {
            // Arrange
            IEnumerable<DateTime> formattedPassages = new[] { p1, p2 }
                .Where(p => p != null)
                .Select(p => DateTime.Parse(p, SwedishCulture));

            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .WithDailyMaximumTollFee(2)
                .WithTollFee(00, 00, 1)
                .WithTollFee(07, 30, 2)
                .WithTollFee(08, 30, 1)
                .Build();

            DateTime expectedDate = formattedPassages.First().Date;
            int expectedNumberOfPassages = formattedPassages.Count(p => p.Date == expectedDate);

            // Act & Assert
            ValidateToll(
                passages: formattedPassages,
                expected: new DailyTollFee(expectedDate, expectedNumberOfPassages, expectedTotal, expectedTaxable));
        }

        /// <summary>
        /// Test single day report with max daily charge and single charge each 60 mins.
        /// </summary>
        /// <param name="expectedTotal">Expected total fee.</param>
        /// <param name="expectedTaxable">Expected taxable fee.</param>
        /// <param name="p1">First passage.</param>
        /// <param name="p2">Second passage.</param>
        /// <param name="p3">Third passage.</param>
        [DataRow(1, 1, "2017-10-01 07:00", null, null, DisplayName = "Single Low-Charge Passage")]
        [DataRow(2, 2, "2017-10-01 08:00", null, null, DisplayName = "Single High-Charge Passage")]
        [DataRow(2, 1, "2017-10-01 07:00", "2017-10-01 07:15", null, DisplayName = "Single-Charge: Two Low-Charge Passages")]
        [DataRow(3, 2, "2017-10-01 07:15", "2017-10-01 08:00", null, DisplayName = "Single-Charge: Two Passages (Low-High)")]
        [DataRow(5, 3, "2017-10-01 07:15", "2017-10-01 08:00", "2017-10-01 08:25", DisplayName = "Single-Charge: Max Charge Passages (Low-High-High)")]
        [DataRow(4, 2, "2017-10-01 07:30", "2017-10-01 08:00", null, DisplayName = "Single-Charge: Max Charge Passages (High-High)")]
        [DataRow(6, 2, "2017-10-01 07:45", "2017-10-01 08:00", "2017-10-01 08:10", DisplayName = "Single-Charge: Max Charge Passages (High-High-High)")]
        [DataRow(5, 3, "2017-10-01 07:30", "2017-10-01 08:00", "2017-10-01 08:30", DisplayName = "Single-Charge: Max Charge Passages (High-High-Low)")]
        [DataRow(4, 3, "2017-10-01 08:00", "2017-10-01 17:00", "2017-10-01 17:00", DisplayName = "Single-Charge: Max Charge Passages (High-Low-Low)")]
        [DataTestMethod]
        public void CalculateDailyTollFee_MaxChargeAndSingleChargePerHourDuringDay(int expectedTotal, int expectedTaxable, string p1, string p2, string p3)
        {
            // Arrange
            IEnumerable<DateTime> formattedPassages = new[] { p1, p2, p3 }
                .Where(p => p != null)
                .Select(p => DateTime.Parse(p, SwedishCulture));

            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .WithDailyMaximumTollFee(3)
                .WithSingleChargeMinutes(60)
                .WithTollFee(00, 00, 1)
                .WithTollFee(07, 30, 2)
                .WithTollFee(08, 30, 1)
                .Build();

            DateTime expectedDate = formattedPassages.First().Date;
            int expectedNumberOfPassages = formattedPassages.Count(p => p.Date == expectedDate);

            // Act & Assert
            ValidateToll(
                passages: formattedPassages,
                expected: new DailyTollFee(expectedDate, expectedNumberOfPassages, expectedTotal, expectedTaxable));
        }

        [TestMethod]
        public void CalculateDailyTollFee_NoPassages_NoToll()
        {
            // Arrange
            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .Build();

            DateTime expectedDate = DateTime.Now.Date;

            // Act & Assert
            ValidateToll(
                passages: new DateTime[0],
                expected: new DailyTollFee(expectedDate, 0, 0, 0));
        }

        [TestMethod]
        public void CalculateDailyTollFee_TollFreeDate_NoToll()
        {
            // Arrange
            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .IsTollFree()
                .Build();

            DateTime passage = DateTime.Now;

            // Act & Assert
            ValidateToll(
                passages: new [] { passage },
                expected: new DailyTollFee(passage.Date, 1, 0, 0));
        }

        [TestMethod]
        public void CalculateDailyTollFee_TollFreeVehicle_NoToll()
        {
            // Arrange
            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .WithTollFreeVehicleTypes(VehicleType.Car)
                .Build();

            DateTime passage = DateTime.Now;

            // Act & Assert
            ValidateToll(
                passages: new[] { passage },
                expected: new DailyTollFee(passage.Date, 1, 0, 0));
        }

        [TestMethod]
        public void CalculateDailyTollFee_NonDomesticIsTollFree_NoToll()
        {
            // Arrange
            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .WithNonDomesticVehiclesTollFree(true)
                .WithDomesticCountryCodes("NO")
                .Build();

            DateTime passage = DateTime.Now;

            // Act & Assert
            ValidateToll(
                passages: new[] { passage },
                expected: new DailyTollFee(passage.Date, 1, 0, 0));
        }

        private void ValidateToll(IEnumerable<DateTime> passages, DailyTollFee expected)
        {
            // Arrange
            int expectedNumberOfPassages = passages.Count();

            // Act
            DailyTollFee actual = Sut.CalculateDailyTollFee(expected.Date, Vehicle, passages);

            // Assert
            Assert.IsTrue(Equals(expected, actual),
                $"DailyTollFee not as expected:" +
                $"\r\n\r\nExpected = {ToDisplay(expected)}" +
                $"\r\n\r\nActual   = {ToDisplay(actual)}");

            // Local functions for comparison and output

            bool Equals(DailyTollFee x, DailyTollFee y)
            {
                if (x == null || y == null) return x == y;

                return x.Date == y.Date &&
                    x.NumberOfPassages == y.NumberOfPassages &&
                    x.TotalAmount == y.TotalAmount &&
                    x.TaxableAmount == y.TaxableAmount;
            }

            string ToDisplay(DailyTollFee dailyTollFee) => dailyTollFee == null
                ? "<null>"
                : $"[Date = {dailyTollFee.Date:d}, NumberOfPassages = {dailyTollFee.NumberOfPassages}, " +
                  $"TotalAmount = {dailyTollFee.TotalAmount}, TaxableAmount = {dailyTollFee.TaxableAmount}]";
        }

        [TestMethod]
        public void CalculateTollFee_NoPassages_NoDailyResults()
        {
            // Act & Assert
            ValidateTollFee(
                passages: null,
                expectedDailyFees: 0,
                expectedTaxableAmount: 0);
        }

        /// <summary>
        /// Test toll calculation for multiple passages.
        /// </summary>
        /// <param name="expectedDays">Expected number of days with report.</param>
        /// <param name="expectedAmount">Expected total taxable amount.</param>
        /// <param name="p1">First passage.</param>
        /// <param name="p2">Second passage.</param>
        /// <param name="p3">Third passage.</param>
        [DataRow(1, 1, "2017-10-01 07:00", null, null, DisplayName = "One Day Passage")]
        [DataRow(2, 2, "2017-10-01 07:00", "2017-10-02 07:00", null, DisplayName = "Two Day Passages")]
        [DataRow(1, 2, "2017-10-01 07:00", "2017-10-01 19:00", null, DisplayName = "Two Single-Day Passages")]
        [DataRow(3, 3, "2017-10-03 07:00", "2017-10-02 07:00", "2017-10-01 07:00", DisplayName = "Three Day Passages")]
        [DataRow(1, 3, "2017-10-01 07:00", "2017-10-01 09:00", "2017-10-01 21:00", DisplayName = "Three Single-Day Passages")]
        [DataTestMethod]
        public void CalculateTollFee_MultiplePassages(int expectedDays, int expectedAmount, string p1, string p2, string p3)
        {
            // Arrange
            IEnumerable<DateTime> formattedPassages = new[] { p1, p2, p3 }
                .Where(p => p != null)
                .Select(p => DateTime.Parse(p, SwedishCulture));

            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .Build();

            // Act & Assert
            ValidateTollFee(
                passages: formattedPassages,
                expectedDailyFees: expectedDays,
                expectedTaxableAmount: expectedAmount);
        }

        /// <summary>
        /// Test the case when single charge continues over night to next date
        /// </summary>
        /// <param name="expectedDays">Expected number of days with report.</param>
        /// <param name="expectedAmount">Expected total taxable amount.</param>
        /// <param name="p1">First passage.</param>
        /// <param name="p2">Second passage.</param>
        [DataRow(2, 2, "2017-10-01 07:00", "2017-10-02 07:00", DisplayName = "Two Day Passages")]
        [DataRow(2, 1, "2017-10-01 23:30", "2017-10-02 00:15", DisplayName = "Single-Charge Passages")]
        [DataTestMethod]
        public void CalculateTollFee_SingleChargeOverMultipleDays(int expectedDays, int expectedAmount, string p1, string p2)
        {
            // Arrange
            IEnumerable<DateTime> formattedPassages = new[] { p1, p2 }
                .Where(p => p != null)
                .Select(p => DateTime.Parse(p, SwedishCulture));

            CurrentRules = TollFeeRulesBuilder
                .SimpleTollRulesCharge1NoMax()
                .WithSingleChargeMinutes(60)
                .Build();

            // Act & Assert
            ValidateTollFee(
                passages: formattedPassages,
                expectedDailyFees: expectedDays,
                expectedTaxableAmount: expectedAmount);
        }

        private void ValidateTollFee(IEnumerable<DateTime> passages, int expectedDailyFees, decimal expectedTaxableAmount)
        {
            // Arrange
            var expected = (Vehicle, expectedDailyFees, expectedTaxableAmount);

            // Act
            TollFeeResult actual = Sut.CalculateTollFee(Vehicle, passages);

            // Assert
            Assert.IsTrue(Equals(expected, actual),
                $"TollFeeResult not as expected:" +
                $"\r\n\r\nExpected = {ExpectedDisplay(expected)}" +
                $"\r\n\r\nActual   = {ActualDisplay(actual)}");

            // Local functions for comparison and output

            bool Equals((Vehicle vehicle, int dailyFees, decimal taxableAmount) x, TollFeeResult y)
            {
                if (y == null) return false;

                return x.vehicle.Equals(y.Vehicle) &&
                    x.dailyFees == y.DailyTollFees.Count() &&
                    x.taxableAmount == y.TotalTaxableAmount;
            }

            string ExpectedDisplay((Vehicle vehicle, int dailyFees, decimal taxableAmount) x) =>
                $"[Vehicle = {x.vehicle.RegistrationIdentifier}, #DailyTollFees = {x.dailyFees}, " +
                $"TotalTaxableAmount = {x.taxableAmount}]";

            string ActualDisplay(TollFeeResult tollFeeResult) => tollFeeResult == null
                ? "<null>"
                : $"[Vehicle = {tollFeeResult.Vehicle.RegistrationIdentifier}, #DailyTollFees = {tollFeeResult.DailyTollFees.Count()}, " +
                  $"TotalTaxableAmount = {tollFeeResult.TotalTaxableAmount}]";
        }
    }
}
