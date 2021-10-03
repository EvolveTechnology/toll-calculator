using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TollCalculatorApp;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class DateTimeExtensionTests
    {
        #region IsBetweenTimes
        [TestMethod]
        public void IsBetweenTimes_Simple_DateTime_Comparison()
        {
            var testDate = new DateTime(2020, 01, 01, 10, 0, 0);
            var isBetween = testDate.IsBetweenTimes(TimeSpan.Parse("09:00"), TimeSpan.Parse("10:30"));

            Assert.IsTrue(isBetween);
        }
        [TestMethod]
        public void IsBetweenTimes_Start_Same_As_Date_Being_Compared()
        {
            var testDate = new DateTime(2020, 01, 01, 9, 0, 0);
            var isBetween = testDate.IsBetweenTimes(TimeSpan.Parse("09:00"), TimeSpan.Parse("10:30"));

            Assert.IsTrue(isBetween);
        }
        [TestMethod]
        public void IsBetweenTimes_End_Same_As_Date_Being_Compared()
        {
            var testDate = new DateTime(2020, 01, 01, 10, 0, 0);
            var isBetween = testDate.IsBetweenTimes(TimeSpan.Parse("09:00"), TimeSpan.Parse("10:00"));

            Assert.IsFalse(isBetween);
        }


        #endregion

        #region IsWeekend
        [TestMethod]
        public void IsWeekend_Saturday()
        {
            var testDate = new DateTime(2021, 01, 02, 10, 0, 0);
            Assert.IsTrue(testDate.IsWeekend());
        }
        [TestMethod]
        public void IsWeekend_Sunday()
        {
            var testDate = new DateTime(2021, 01, 03, 10, 0, 0);
            Assert.IsTrue(testDate.IsWeekend());
        }
        [TestMethod]
        public void IsWeekend_Weekday()
        {
            var testDate = new DateTime(2021, 01, 04, 10, 0, 0);
            Assert.IsFalse(testDate.IsWeekend());
        }
        #endregion
    }
}
