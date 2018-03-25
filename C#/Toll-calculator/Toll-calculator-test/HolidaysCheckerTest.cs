using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toll_calculator.Holidays;

namespace Toll_calculator_test {

    [TestClass]
    public class HolidaysCheckerTest {

        private IHolidayChecker holidayChecker;

        [TestInitialize]
        public void testInit() {
            holidayChecker = new Sweden2018HolidayChecker();
        }

        [TestCleanup]
        public void testClean() {
            holidayChecker = null;
        }

        [TestMethod]
        public void TestFixedHolidays() {
            Assert.IsTrue(holidayChecker.IsHoliday(new DateTime(1916, 1, 1)));
            Assert.IsTrue(holidayChecker.IsHoliday(new DateTime(2018, 1, 1)));
            Assert.IsTrue(holidayChecker.IsHoliday(new DateTime(2016, 6, 6)));
        }

        [TestMethod]
        public void Test2018SpecificHolidays() {
            Assert.IsTrue(holidayChecker.IsHoliday(new DateTime(2018, 3, 30)));
            Assert.IsFalse(holidayChecker.IsHoliday(new DateTime(2017, 3, 30)));
            Assert.IsTrue(holidayChecker.IsHoliday(new DateTime(2018, 6, 23)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidDate() {
            holidayChecker.IsHoliday(new DateTime(2018, 2, 29));
        }

    }
}
