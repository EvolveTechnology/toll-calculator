using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TollCalculatorApp.Services;
using TollCalculatorApp.Services.Interfaces;

namespace TollFeeCalculatorTests
{
    [TestClass]
    public class SwedishHolidayServiceTest
    {
        private IHolidayService _holidayService;

        [TestInitialize]
        public void TestInitialize()
        {
            _holidayService = new SwedishHolidayService();
        }

        #region IsSwedishHoliday Tests
        [TestMethod]
        public void NewYearsDayTest()
        {
            var date = new DateTime(2021, 1, 1);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected New Years day to be in list of holidays");
        }
        [TestMethod]
        public void EpiphanyTest()
        {
            var date = new DateTime(2021, 1, 6);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Epiphany to be holiday");
        }
        [TestMethod]
        public void GoodFridayTest()
        {
            var date = new DateTime(2021, 4, 2);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Good friday to be holiday");
        }
        [TestMethod]
        public void EasterEveTest()
        {
            var date = new DateTime(2021, 4, 3);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Easter Eve to be holiday");
        }
        [TestMethod]
        public void EasterSundayTest()
        {
            var date = new DateTime(2021, 4, 4);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Easter Sunday to be holiday");
        }
        [TestMethod]
        public void EasterMondayTest()
        {
            var date = new DateTime(2021, 4, 5);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Easter Monday to be holiday");
        }
        [TestMethod]
        public void LabourDayTest()
        {
            var date = new DateTime(2021, 5, 1);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Labour Day to be holiday");
        }
        [TestMethod]
        public void AcensionDayTest()
        {
            var date = new DateTime(2021, 5, 13);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Acension Day to be holiday");
        }
        [TestMethod]
        public void WhitsunTest()
        {
            var date = new DateTime(2021, 5, 22);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Whitsun to be holiday");
        }
        [TestMethod]
        public void PentecostTest()
        {
            var date = new DateTime(2021, 5, 23);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Pentecost to be holiday");
        }
        [TestMethod]
        public void NationalDayTest()
        {
            var date = new DateTime(2021, 6, 6);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected National Day to be holiday");
        }
        [TestMethod]
        public void MidsummerEveTest()
        {
            var date = new DateTime(2021, 6, 25);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Midsummer Eve to be holiday");
        }
        [TestMethod]
        public void MidsummerDayTest()
        {
            var date = new DateTime(2021, 6, 26);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Midsummer Day to be holiday");
        }
        [TestMethod]
        public void AllSaintsDayTest()
        {
            var date = new DateTime(2021, 11, 6);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected All saints Day to be holiday");
        }
        [TestMethod]
        public void ChristmasEveTest()
        {
            var date = new DateTime(2021, 12, 24);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Christmas Eve to be holiday");
        }
        [TestMethod]
        public void ChristmasDayTest()
        {
            var date = new DateTime(2021, 12, 25);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Christmas Day to be holiday");
        }
        [TestMethod]
        public void BoxingDayTest()
        {
            var date = new DateTime(2021, 12, 26);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected Boxing to be holiday");
        }
        [TestMethod]
        public void NewYearsEveTest()
        {
            var date = new DateTime(2021, 12, 31);
            Assert.IsTrue(_holidayService.IsHoliday(date), "Expected New Year's Eve to be holiday");
        }
        #endregion
    }
}
