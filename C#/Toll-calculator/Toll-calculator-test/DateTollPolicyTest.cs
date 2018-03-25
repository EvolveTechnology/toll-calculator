using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toll_calculator;
using Toll_calculator.Holidays;

namespace Toll_calculator_test {

    [TestClass]
    public class DateTollPolicyTest {

        private StandardDateTollPolicy datePolicy;

        [TestInitialize]
        public void testInit() {
            IHolidayChecker holidayChecker = new Sweden2018HolidayChecker();
            datePolicy = new StandardDateTollPolicy(holidayChecker);
        }

        [TestCleanup]
        public void testClean() {
            datePolicy = null;
        }

        [TestMethod]
        public void TestStandardPolicy() {
            Assert.IsFalse(datePolicy.IsTollable(new DateTime(2018, 3, 25)));
            Assert.IsTrue(datePolicy.IsTollable(new DateTime(2018, 3, 26)));
            Assert.IsFalse(datePolicy.IsTollable(new DateTime(2018, 7, 21)));
            Assert.IsTrue(datePolicy.IsTollable(new DateTime(2016, 10, 13)));
        }
    }
}
