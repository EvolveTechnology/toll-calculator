using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TollCalculatorTests
    {
        [TestMethod]
        public void TestTollFreeVehicles()
        {
            var tc = new TollCalculator.TollCalculator();
            Assert.IsTrue(tc.IsTollFreeVehicle(TollCalculator.Vehicle.Diplomat));
            Assert.IsFalse(tc.IsTollFreeVehicle(TollCalculator.Vehicle.Car));
        }

        [TestMethod]
        public void TestFeePerHour()
        {
            var tc = new TollCalculator.TollCalculator();
            Assert.AreEqual(0, tc.GetTollFee(new DateTime(2020,9,4,5,32,0)));
        }

        [TestMethod]
        public void TestTollFee_OnlyOnePerHour1()
        {
            var tc = new TollCalculator.TollCalculator();
            Assert.AreEqual(18, tc.GetTollFee(TollCalculator.Vehicle.Car, 
                new DateTime[]
                {
                    new DateTime(2020, 9, 4, 7, 25, 0),
                    new DateTime(2020, 9, 4, 7, 32, 0)
                }));
        }

        [TestMethod]
        public void TestTollFee_OnlyOnePerHour2()
        {
            var tc = new TollCalculator.TollCalculator();
            Assert.AreEqual(18+18, tc.GetTollFee(TollCalculator.Vehicle.Car,
                new DateTime[]
                {
                    new DateTime(2020, 9, 3, 7, 25, 0),
                    new DateTime(2020, 9, 4, 7, 32, 0)
                }));
        }

        [TestMethod]
        public void TestTollFee_Max60PerDay()
        {
            var tc = new TollCalculator.TollCalculator();
            Assert.AreEqual(120, tc.GetTollFee(TollCalculator.Vehicle.Car,
                new DateTime[]
                {
                    new DateTime(2020, 9, 3, 7, 25, 0),
                    new DateTime(2020, 9, 3, 8, 25, 0),
                    new DateTime(2020, 9, 3, 9, 25, 0),
                    new DateTime(2020, 9, 3, 10, 25, 0),
                    new DateTime(2020, 9, 3, 11, 25, 0),
                    new DateTime(2020, 9, 3, 12, 25, 0),
                    new DateTime(2020, 9, 3, 13, 25, 0),
                    new DateTime(2020, 9, 4, 7, 32, 0),
                    new DateTime(2020, 9, 4, 8, 32, 0),
                    new DateTime(2020, 9, 4, 9, 32, 0),
                    new DateTime(2020, 9, 4, 10, 32, 0),
                    new DateTime(2020, 9, 4, 11, 32, 0),
                    new DateTime(2020, 9, 4, 12, 32, 0),
                    new DateTime(2020, 9, 4, 13, 32, 0)
                }));
        }
    }
}
