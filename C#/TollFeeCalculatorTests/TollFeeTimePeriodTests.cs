using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class TollFeeTimePeriodTests
    {
        [TestMethod]
        public void IsInTimePeriod_Match()
        {
            int hour = 8;
            int minute = 15;
            var feePeriod = new TollFeeTimePeriod(7, 0, 8, 29, 18);
            bool expected = true;

            bool actual = feePeriod.Contains(hour, minute);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsInTimePeriod_NoMatch()
        {
            int hour = 9;
            int minute = 30;
            var feePeriod = new TollFeeTimePeriod(6, 30, 6, 59, 10);
            bool expected = false;

            bool actual = feePeriod.Contains(hour, minute);

            Assert.AreEqual(expected, actual);
        }
    }
}
