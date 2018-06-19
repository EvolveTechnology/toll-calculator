using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class CalendarUnitTest
    {
        [TestMethod]
        public void Calendar_IsTollFreeDate_NewYearsDay_true()
        {
            DateTime t = new DateTime(2013, 1, 1);
            Assert.AreEqual(Calendar.IsTollFreeDate(t), true);
        }

        [TestMethod]
        public void Calendar_IsTollFreeDate_JanuarySecond_false()
        {
            DateTime t = new DateTime(2013, 1, 2);
            Assert.AreEqual(Calendar.IsTollFreeDate(t), false);
        }

        [TestMethod]
        public void Calendar_IsTollFreeDate_MayFirst_true()
        {
            DateTime t = new DateTime(2013, 5, 1);
            Assert.AreEqual(Calendar.IsTollFreeDate(t), true);
        }

        // Etc. Could try to reach 100% test coverage, but this only serves to show the process.

    }
}
