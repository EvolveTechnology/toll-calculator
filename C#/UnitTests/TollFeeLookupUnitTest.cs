using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;
using System;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class TollFeeLookupUnitTest
    {
        [TestMethod]
        public void Fee_RushHour_MaximumFee()
        {
            Assert.AreEqual(TollFeeLookup.Fee(new TimeSpan(7, 50, 0)), Settings.FEE_HIGHEST);
        }

        [TestMethod]
        public void Fee_RightBeforeRushHour_HighFee()
        {
            Assert.AreEqual(TollFeeLookup.Fee(new TimeSpan(6, 50, 0)), Settings.FEE_HIGH);
        }

        [TestMethod]
        public void Fee_MidDay_MediumFee()
        {
            Assert.AreEqual(TollFeeLookup.Fee(new TimeSpan(12, 50, 0)), Settings.FEE_MEDIUM);
        }

        [TestMethod]
        public void Fee_EarlyMorning_LowFee()
        {
            Assert.AreEqual(TollFeeLookup.Fee(new TimeSpan(4, 50, 0)), Settings.FEE_LOW);
        }

        // Etc. Could try to reach 100% test coverage, but this only serves to show the process.

    }
}
