using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TollCalculator.Contracts.Calendar;
using TollCalculator.Contracts.Rules;

namespace TollCalculator.Gothenburg.UnitTests
{
    [TestClass]
    public class GothenburgTollFeeRepositoryTests
    {
        bool IsTollFree { get; set; }
        private Mock<ITollFeeCalendar> TollFeeCalendarMock { get; set; }
        private GothenburgTollFeeRepository Sut { get; set; }

        [TestInitialize]
        public void Setup()
        {
            TollFeeCalendarMock = new Mock<ITollFeeCalendar>();
            TollFeeCalendarMock
                .Setup(x => x.IsTollFree(It.IsAny<DateTime>()))
                .Returns(() => IsTollFree);

            Sut = new GothenburgTollFeeRepository(TollFeeCalendarMock.Object, null);
        }

        [TestMethod]
        public void GetTollFeeRulesForDate_BeforeCongestionTax_NoToll()
        {
            // Arrange
            DateTime date = new DateTime(2012, 12, 31);

            // Act
            ITollFeeRules rules = Sut.GetTollFeeRulesForDate(date);

            // Assert
            Assert.IsTrue(rules.IsTollFreeDate,
                $"{date:d} should be toll free due to being before congestion toll.");
            TollFeeCalendarMock
                .Verify(x => x.IsTollFree(It.IsAny<DateTime>()),
                    Times.Never);
        }

        [TestMethod]
        public void GetTollFeeRulesForDate_IsTollFree_NoToll()
        {
            // Arrange
            DateTime date = new DateTime(2013, 01, 01);
            IsTollFree = true;

            // Act
            ITollFeeRules rules = Sut.GetTollFeeRulesForDate(date);

            // Assert
            Assert.IsTrue(rules.IsTollFreeDate,
                $"{date:d} should be toll free due to toll free day in calendar.");
            TollFeeCalendarMock
                .Verify(x => x.IsTollFree(It.IsAny<DateTime>()),
                    Times.Once);
        }

        [TestMethod]
        public void GetTollFeeRulesForDate_2013_Rules2013()
        {
            // Arrange
            DateTime date = new DateTime(2013, 01, 01);
            IsTollFree = false;

            // Act
            ITollFeeRules rules = Sut.GetTollFeeRulesForDate(date);

            // Assert
            Assert.IsTrue(rules.AreNonDomesticVehiclesTollFree,
                "For 2013 non-domestic vehicles should be toll free.");
        }

        [TestMethod]
        public void GetTollFeeRulesForDate_2014_Rules2014()
        {
            // Arrange
            decimal expectedHighestToll = 18;
            DateTime date = new DateTime(2014, 01, 01);
            IsTollFree = false;

            // Act
            ITollFeeRules rules = Sut.GetTollFeeRulesForDate(date);
            decimal actual = rules.TollFeeOrderedByStartTime.Max(kvp => kvp.Value);

            // Assert
            Assert.IsFalse(rules.AreNonDomesticVehiclesTollFree,
                "For 2014 non-domestic vehicles should not be toll free.");
            Assert.AreEqual(expectedHighestToll, actual,
                "Highest daily toll does not match for 2014:" +
                $"\r\nExpected = {expectedHighestToll}, Actual = {actual}");
        }

        [TestMethod]
        public void GetTollFeeRulesForDate_2015_Rules2015()
        {
            // Arrange
            decimal expectedHighestToll = 22;
            DateTime date = new DateTime(2015, 01, 01);
            IsTollFree = false;

            // Act
            ITollFeeRules rules = Sut.GetTollFeeRulesForDate(date);
            decimal actual = rules.TollFeeOrderedByStartTime.Max(kvp => kvp.Value);

            // Assert
            Assert.IsFalse(rules.AreNonDomesticVehiclesTollFree,
                "For 2015 non-domestic vehicles should not be toll free.");
            Assert.AreEqual(expectedHighestToll, actual,
                "Highest daily toll does not match for 2015:" +
                $"\r\nExpected = {expectedHighestToll}, Actual = {actual}");
        }
    }
}
