using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollCalculatorApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculatorApp.Models;

namespace TollCalculatorApp.Tests
{
    [TestClass()]
    public class TollCalculatorTests
    {
        private TollCalculator _tc = new TollCalculator();

        [TestMethod()]
        public void CalculateTollFee_TollFreeDay_ReturnZero()
        {
            // Arrange
            var dates = new DateTime(2021, 1, 1, 0, 0, 0);
            var expected = 0;

            // Act
            var toll = _tc.CalculateTollFee(dates);

            // Assert
            int actual = toll;
            Assert.AreEqual(expected, actual, "Date should be Toll Free");
        }

        [TestMethod()]
        public void CalculateTollFee_TollNotFreeDay_ReturnLowestFee()
        {
            // Arrange
            var dates = new DateTime(2021, 1, 5, 6, 0, 0);
            var expected = 8;

            // Act
            var toll = _tc.CalculateTollFee(dates);

            // Assert
            int actual = toll;
            Assert.AreEqual(expected, actual, "Returning wrong fee amount");
        }

        [TestMethod()]
        public void CalculateTollFee_TollNotFreeDay_ReturnMiddleFee()
        {
            // Arrange
            var dates = new DateTime(2021, 1, 5, 6, 30, 0);
            var expected = 13;

            // Act
            var toll = _tc.CalculateTollFee(dates);

            // Assert
            int actual = toll;
            Assert.AreEqual(expected, actual, "Returning wrong fee amount");
        }

        [TestMethod()]
        public void CalculateTollFee_TollNotFreeDay_ReturnHighestFee()
        {
            // Arrange
            var dates = new DateTime(2021, 1, 5, 15, 59, 0);
            var expected = 18;

            // Act
            var toll = _tc.CalculateTollFee(dates);

            // Assert
            int actual = toll;
            Assert.AreEqual(expected, actual, "Returning wrong fee amount");
        }

        [TestMethod()]
        public void GetTollFee_TollApplicableVechile_ArrayWithDates_ReturnMaxToll()
        {
            // Arrange
            var dates = new DateTime[8]
           {
                new DateTime(2021, 1, 20, 6,0,0),
                new DateTime(2021, 1, 20, 7,1,0),
                new DateTime(2021, 1, 20, 8,25,0),
                new DateTime(2021, 1, 20, 9,30,0),
                new DateTime(2021, 1, 20, 14,0,0),
                new DateTime(2021, 1, 20, 16,30,0),
                new DateTime(2021, 1, 20, 17,0,0),
                new DateTime(2021, 1, 20, 17,25,0),
           };
            var expected = 60;

            // Act
            var toll = _tc.GetTollFee(new Vehicle(VehiclesType.Car), dates);

            // Assert
            int actual = toll;
            Assert.AreEqual(expected, actual, "Returning wrong fee amount");
        }

        [TestMethod()]
        public void GetTollFee_TollApplicableVechile_ArrayWithDates_ReturnHeigheststTollForOneHour()
        {
            // Arrange
            var dates = new DateTime[3]
           {
                new DateTime(2021, 1, 20, 6,59,0),
                new DateTime(2021, 1, 20, 7,30,0),
                new DateTime(2021, 1, 20, 8,10,0),
           };
            var expected = 31;

            // Act
            var toll = _tc.GetTollFee(new Vehicle(VehiclesType.Car), dates);

            // Assert
            int actual = toll;
            Assert.AreEqual(expected, actual, "Returning wrong fee amount");
        }

        [TestMethod()]
        public void GetTollFee_TollApplicableVechile_ArrayWithDates_ReturnSumOfHeigheststTollForTwoSeperateHours()
        {
            // Arrange
            var dates = new DateTime[6]
           {
                new DateTime(2021, 1, 20, 6,59,0),
                new DateTime(2021, 1, 20, 7,30,0),
                new DateTime(2021, 1, 20, 8,10,0),
                new DateTime(2021, 1, 20, 15,20,0),
                new DateTime(2021, 1, 20, 15,35,0),
                new DateTime(2021, 1, 20, 8,45,0),
           };
            var expected = 49;

            // Act
            var toll = _tc.GetTollFee(new Vehicle(VehiclesType.Car), dates);

            // Assert
            int actual = toll;
            Assert.AreEqual(expected, actual, "Returning wrong fee amount");
        }
    }
}