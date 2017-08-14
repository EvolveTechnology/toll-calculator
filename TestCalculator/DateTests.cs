using System;
using System.CodeDom;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateLibrary;

namespace CalculatorTests
{
    [TestClass]
    public class DateTests
    {
        [TestMethod]
        public void TestDates()
        {
            var dates = new Dictionary<bool, DateTime>
            {
                {true, new DateTime(2017, 12, 25)},
                {false, new DateTime(2017, 12, 22)}
            };

            foreach (var date in dates)
            {
                TestHolidays(date.Key,date.Value);
            }
        }

        [TestMethod]
        public void TestHolidays(bool value, DateTime date)
        {
            if (value)
            {
                Assert.IsTrue(DateLibrary.HolidayProvider.IsHoliday(date));
            }
            else
            {
                Assert.IsFalse(DateLibrary.HolidayProvider.IsHoliday(date));
            }
        }
    }
}
