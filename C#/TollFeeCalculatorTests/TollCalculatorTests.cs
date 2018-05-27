using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class TollCalculatorTests
    {    
        [TestMethod]
        public void GetTollFee_NoFreeDate_NoFreeVehicle()
        {
            var tollCalculator = new TollCalculator();
            var date = new DateTime(2018, 01, 02, 7, 35, 0);
            var vehicle = new Car();
            int expected = 18;

            int tollFee = tollCalculator.GetTollFee(date, vehicle);

            Assert.AreEqual(expected, tollFee);
        }

        [TestMethod]
        public void GetTollFee_FreeDate_NoFreeVehicle()
        {
            var tollCalculator = new TollCalculator();
            var date = new DateTime(2018, 1, 6, 7, 35, 0); // weekend
            var vehicle = new Car();
            int expected = 0;

            int tollFee = tollCalculator.GetTollFee(date, vehicle);

            Assert.AreEqual(expected, tollFee);
        }

        [TestMethod]
        public void GetTollFee_NoFreeDate_FreeVehicle()
        {
            var tollCalculator = new TollCalculator();
            var date = new DateTime(2018, 01, 02, 7, 35, 0);
            var vehicle = new Motorbike();
            int expected = 0;

            int tollFee = tollCalculator.GetTollFee(date, vehicle);

            Assert.AreEqual(expected, tollFee);
        }

        [TestMethod]
        public void GetTollFee_Holiday()
        {
            var tollCalculator = new TollCalculator();
            var date = new DateTime(2018, 5, 1, 7, 35, 0); // first of may is a holiday
            var vehicle = new Car();
            int expected = 0;

            int tollFee = tollCalculator.GetTollFee(date, vehicle);

            Assert.AreEqual(expected, tollFee);
        }

        [TestMethod]
        public void GetTollFee_July()
        {
            var tollCalculator = new TollCalculator();
            var date = new DateTime(2018, 7, 4, 7, 35, 0); // july is toll free
            var vehicle = new Car();
            int expected = 0;

            int tollFee = tollCalculator.GetTollFee(date, vehicle);

            Assert.AreEqual(expected, tollFee);
        }

        [TestMethod]
        public void GetTollFees_ZeroDates()
        {
            var tollCalculator = new TollCalculator();
            var dates = new DateTime[]{ };
            var vehicle = new Car();
            int expected = 0;

            int actual = tollCalculator.GetTollFee(vehicle, dates);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTollFees_OneDate()
        {
            var tollCalculator = new TollCalculator();
            var dates = new DateTime[] 
            {
                new DateTime(2018, 1, 2, 8, 15, 0) // 13 kr
            };
            var vehicle = new Car();
            int expected = 13;

            int actual = tollCalculator.GetTollFee(vehicle, dates);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTollFees_MultipleDates()
        {
            var tollCalculator = new TollCalculator();
            var dates = new DateTime[]
            {
                new DateTime(2018, 1, 2, 8, 15, 0), // 13 kr
                new DateTime(2018, 1, 2, 10, 20, 0), // 8 kr
                new DateTime(2018, 1, 2, 15, 50, 0) // 18 kr

            };
            var vehicle = new Car();
            int expected = 39;

            int actual = tollCalculator.GetTollFee(vehicle, dates);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTollFees_MaxFee()
        {
            var tollCalculator = new TollCalculator();
            var dates = new DateTime[]
            {
                new DateTime(2018, 1, 2, 8, 15, 0), // 13 kr
                new DateTime(2018, 1, 2, 10, 20, 0), // 8 kr
                new DateTime(2018, 1, 2, 15, 50, 0), // 18 kr
                new DateTime(2018, 1, 2, 17, 12, 0), // 13 kr
                new DateTime(2018, 1, 2, 17, 47, 0), // 13 kr
                new DateTime(2018, 1, 2, 18, 22, 0), // 13 kr

            };
            var vehicle = new Car();
            int expected = 60;

            int actual = tollCalculator.GetTollFee(vehicle, dates);

            Assert.AreEqual(expected, actual);
        }
    }
}
