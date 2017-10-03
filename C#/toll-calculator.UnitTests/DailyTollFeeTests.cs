using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TollCalculator.UnitTests
{
    [TestClass]
    public class DailyTollFeeTests
    {
        [TestMethod]
        public void DailyTollFee_DateWithTimeSpecified_StoresOnlyDatePart()
        {
            // Arrange
            var dateWithTime = new DateTime(2017, 10, 1, 11, 00, 00);
            DateTime expected = dateWithTime.Date;

            // Act
            var sut = new DailyTollFee(dateWithTime, 1, 1, 1);
            DateTime actual = sut.Date;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
