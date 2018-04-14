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
    }
}
