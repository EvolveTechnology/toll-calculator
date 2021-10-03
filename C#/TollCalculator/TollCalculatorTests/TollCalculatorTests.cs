using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TollCalculatorApp.Services;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class TollCalculatorTests
    {
        private TollCalculator _tollCalculator;

        [TestInitialize]
        public void TestInitialize()
        {
            var holidayService = new SwedishHolidayService();
            _tollCalculator = new TollCalculator(holidayService);
        }

        [TestMethod]
        public void Maximum_Fee_Per_Day()
        {
            //Arrange
            var vehicle = new Car();
            var dates = new DateTime[] {
                new DateTime(2021, 10, 6, 7, 0, 0), 
                new DateTime(2021, 10, 6, 9, 0, 0), 
                new DateTime(2021, 10, 6, 11, 0, 0), 
                new DateTime(2021, 10, 6, 13, 0, 0), 
                new DateTime(2021, 10, 6, 15, 0, 0),
                new DateTime(2021, 10, 6, 17, 0, 0)
            };

            //Act
            var tollFee = _tollCalculator.GetTollFee(vehicle, dates);

            //Assert
            Assert.AreEqual(60, tollFee, "Expected max fee for a day to be 60");
        }

        [TestMethod]
        public void Fee_Free_Vehicle_Returns_No_Fee()
        {
            //Arrange
            var vehicle = new Motorbike();
            var dates = new DateTime[] { new DateTime(2021, 01, 01, 10, 0, 0) };

            //Act
            var tollFee = _tollCalculator.GetTollFee(vehicle, dates);

            //Assert
            Assert.AreEqual(0, tollFee, "Expected motorbike to be tollfree vehicle");
        }

        [TestMethod]
        public void Fee_Free_Holiday_Returns_No_Fee()
        {
            //Arrange
            var vehicle = new Car();
            var dates = new DateTime[] { new DateTime(2021, 01, 01, 10, 0, 0) };

            //Act
            var tollFee = _tollCalculator.GetTollFee(vehicle, dates);

            //Assert
            Assert.AreEqual(0, tollFee, "Expected no tax on holiday");
        }

        [TestMethod]
        public void Highest_Fee_Is_Applied_For_Fees_Within_60_Minutes()
        {
            //Arrange
            var vehicle = new Car();
            var dates = new DateTime[] { 
                new DateTime(2021, 02, 01, 15, 0, 0), //Medium fee - 13
                new DateTime(2021, 02, 01, 15, 35, 0) //Medium fee - 18
            };

            //Act
            var tollFee = _tollCalculator.GetTollFee(vehicle, dates);

            //Assert
            Assert.AreEqual(18, tollFee, "Expected highest fee to be returned");
        }

        [TestMethod]
        public void One_TollStation_Pass_Returns_Correct_Fee()
        {
            //Arrange
            var vehicle = new Car();
            var dates = new DateTime[] {
                new DateTime(2021, 02, 01, 15, 0, 0) //Medium fee - 13
            };

            //Act
            var tollFee = _tollCalculator.GetTollFee(vehicle, dates);

            //Assert
            Assert.AreEqual(13, tollFee);
        }
        [TestMethod]
        public void Large_Amount_of_TollStation_Passes_Returns_Correct_Fee()
        {
            //Arrange
            var vehicle = new Car();
            var dates = new DateTime[] {
                new DateTime(2021, 02, 01, 6, 0, 0), //Medium fee - 8
                new DateTime(2021, 02, 01, 10, 0, 0), //Medium fee - 8
                new DateTime(2021, 02, 01, 15, 0, 0), //Medium fee - 13
                new DateTime(2021, 02, 01, 18, 0, 0), //Small fee - 8
            };

            //Act
            var tollFee = _tollCalculator.GetTollFee(vehicle, dates);

            //Assert
            Assert.AreEqual(37, tollFee);
        }
    }
}
