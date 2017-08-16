using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateLibrary;
using Toll_Calculator;
using Toll_Calculator.Helpers;
using Toll_Calculator.Models.Vehicles;

namespace CalculatorTests
{
    [TestClass]
    public class DateTests
    {
        [TestMethod]
        [TestCategory("Date test")]
        public void ValidDates()
        {
            var dates = new Dictionary<bool, DateTime>
            {
                {true, new DateTime(2017, 12, 25)},
                {false, new DateTime(2017, 12, 22)}
            };

            foreach (var date in dates)
            {
                TestHolidays(date.Key, date.Value);
            }
        }

        private void TestHolidays(bool value, DateTime date)
        {
            if (value)
            {
                Assert.IsTrue(HolidayProvider.IsHoliday(date));
            }
            else
            {
                Assert.IsFalse(HolidayProvider.IsHoliday(date));
            }
        }

        [TestMethod]
        [TestCategory("Date test")]
        public void ValidPeriods()
        {
            var fees = TollHelper.GetFeePeriods();

            var dates = new[]
            {
                new DateTime(2018,11,03, 13,45,00),
                new DateTime(2017,08,17, 13,45,00),
                new DateTime(2017,08,17, 15,45,00),
                new DateTime(2017,08,17, 17,15,00),
                new DateTime(2017,08,17, 14,40,00),
                new DateTime(2017,08,17, 21,05,00),
                new DateTime(2017,08,19, 13,05,00)
            };
            var eligibleDates = TollHelper.GetEligibleDates(dates, fees).ToList();
            Assert.IsTrue(eligibleDates.Count == 4);
        }  
    }
}
