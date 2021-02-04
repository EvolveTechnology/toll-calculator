using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TollCalculatorBlazorApp.Models;
using TollCalculatorBlazorApp.Services;

namespace TollCalculatorBlazorUnitTest
{
    [TestClass]
    public class TollCalculatorUnitTest
    {
        [TestMethod]
        public void TollDailyInvoice_DatesfromDifferentDays_onlyTollFeeForDatesFromFirstDay()
        {
            //Arrange
            var tollCalculation = new TollCalculatorService(new Car());
            DateTime[] dates = { new DateTime(2021, 5, 7, 08, 15, 20), new DateTime(2021, 5, 7, 08, 20, 20), new DateTime(2021, 5, 7, 08, 25, 20), new DateTime(2021, 5, 7, 10, 40, 20), new DateTime(2021, 5, 7, 11, 20, 20), new DateTime(2021, 5, 7, 08, 0, 20), new DateTime(2022, 12, 8) };

            //Act
            var result = tollCalculation.CalculateTollDailyInvoice(dates);
            //Assert
            Assert.AreEqual(result, 21);
        }

        [TestMethod]
        public void TollDailyInvoice_emptyDates_minusOne()
        {
            //Arrange
            var tollCalculation = new TollCalculatorService(new Car());
            DateTime[] dates = { };


            //Act
            var result = tollCalculation.CalculateTollDailyInvoice(dates);
            //Assert
            Assert.AreEqual(result, -1);
        }

        [TestMethod]
        public void TollDailyInvoice_TollFreeVehcile_Zero()
        {
            //Arrange
            var tollCalculation = new TollCalculatorService(new Motorbike());
            DateTime[] dates = { new DateTime(2021, 5, 7, 08, 15, 20), new DateTime(2021, 5, 7, 08, 20, 20), new DateTime(2021, 5, 7, 08, 25, 20), new DateTime(2021, 5, 7, 10, 40, 20), new DateTime(2021, 5, 7, 11, 20, 20), new DateTime(2021, 5, 7, 08, 0, 20), new DateTime(2022, 12, 8) };

            //Act
            var result = tollCalculation.CalculateTollDailyInvoice(dates);
            //Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void TollDailyInvoice_TollFreeDay_Zero()
        {
            //Arrange
            var tollCalculation = new TollCalculatorService(new Car());
            DateTime[] dates = { new DateTime(2021, 1, 1, 08, 15, 20), new DateTime(2021, 1, 1, 08, 20, 20), new DateTime(2021, 1, 1, 08, 25, 20), new DateTime(2022, 12, 8) };

            //Act
            var result = tollCalculation.CalculateTollDailyInvoice(dates);
            //Assert
            Assert.AreEqual(result, 0);
        }
    }
}
