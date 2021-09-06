using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;

namespace UnitTests
{
    [TestClass]
    public class TollCalculatorTest
    {
        TollCalculator tc;

        [TestInitialize]
        public void Before()
        {
            tc = new TollCalculator();
        }

        [TestMethod]
        public void TestMethodMaxIntervalFee()
        {
            Assert.AreEqual(18, tc.GetTollFee(new Car(), new DateTime[] {
                new DateTime(2013, 2, 1, 6, 30, 0),
                new DateTime(2013, 2, 1, 6, 45, 0),
                new DateTime(2013, 2, 1, 7, 0, 0),
                new DateTime(2013, 2, 1, 7, 15, 0),
                new DateTime(2013, 2, 1, 7, 30, 0) }));
        }

        [TestMethod]
        public void TestMethodMaxDayFee()
        {
            Assert.AreEqual(60, tc.GetTollFee(new Car(), new DateTime[] {
                new DateTime(2013, 2, 1, 6, 0, 0),
                new DateTime(2013, 2, 1, 7, 0, 0),
                new DateTime(2013, 2, 1, 8, 0, 0),
                new DateTime(2013, 2, 1, 9, 0, 0),
                new DateTime(2013, 2, 1, 11, 0, 0),
                new DateTime(2013, 2, 1, 13, 0, 0),
                new DateTime(2013, 2, 1, 15, 0, 0),
                new DateTime(2013, 2, 1, 16, 0, 0),
                new DateTime(2013, 2, 1, 17, 0, 0),
                new DateTime(2013, 2, 1, 18, 0, 0) }));
        }

        [TestMethod]
        public void TestMethodTimeOfDay()
        {
            Assert.AreEqual(0, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 5, 0, 0) }));
            Assert.AreEqual(8, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 6, 0, 0) }));
            Assert.AreEqual(13, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 6, 30, 0) }));
            Assert.AreEqual(18, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 7, 30, 0) }));
            Assert.AreEqual(13, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 8, 0, 0) }));
            Assert.AreEqual(8, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 8, 30, 0) }));
            Assert.AreEqual(8, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 9, 30, 0) }));
            Assert.AreEqual(8, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 12, 30, 0) }));
            Assert.AreEqual(13, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 15, 0, 0) }));
            Assert.AreEqual(18, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 15, 30, 0) }));
            Assert.AreEqual(18, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 16, 30, 0) }));
            Assert.AreEqual(13, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 17, 30, 0) }));
            Assert.AreEqual(8, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 18, 0, 0) }));
            Assert.AreEqual(0, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 2, 1, 18, 30, 0) }));
        }

        [TestMethod]
        public void TestMethodTollFreeDays()
        {
            Assert.AreEqual(0, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 1, 1, 12, 0, 0) }));
            Assert.AreEqual(0, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 5, 1, 12, 0, 0) }));
            Assert.AreEqual(0, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 7, 1, 12, 0, 0) }));
            Assert.AreEqual(0, tc.GetTollFee(new Car(), new DateTime[] { new DateTime(2013, 12, 24, 12, 0, 0) }));
        }

        [TestMethod]
        public void TestMethodTollFreeVehicle()
        {
            Assert.AreEqual(0, tc.GetTollFee(new Motorbike(), new DateTime[] { new DateTime(2013, 2, 1, 12, 0, 0) }));
        }
    }
}
