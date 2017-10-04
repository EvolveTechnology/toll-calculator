using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TollCalculator.Gothenburg.UnitTests
{
    [TestClass]
    public class SwedishTollFeeCalendarTests
    {
        private SwedishTollFeeCalendar Sut { get; set; }

        [TestInitialize]
        public void Setup()
        {
            Sut = new SwedishTollFeeCalendar();
        }

        [TestMethod]
        public void CultureInfo_IsSwedish()
        {
            // Arrange
            const string expected = "sv-SE";

            // Act
            var actual = Sut.Culture.Name;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [DataRow(01, 05, DisplayName = "Epiphany 1")]
        [DataRow(01, 06, DisplayName = "Epiphany 2")]
        [DataRow(01, 07, DisplayName = "Saturday")]
        [DataRow(01, 08, DisplayName = "Sunday")]
        [DataRow(04, 13, DisplayName = "Easter 1")]
        [DataRow(04, 14, DisplayName = "Easter 2")]
        [DataRow(04, 17, DisplayName = "Easter 3")]
        [DataRow(05, 01, DisplayName = "1st of May")]
        [DataRow(05, 24, DisplayName = "Ascension 1")]
        [DataRow(05, 25, DisplayName = "Ascension 2")]
        [DataRow(06, 05, DisplayName = "National Day of Sweden 1")]
        [DataRow(06, 06, DisplayName = "National Day of Sweden 2")]
        [DataRow(06, 23, DisplayName = "Midsummer")]
        [DataRow(11, 03, DisplayName = "All Saints Day")]
        [DataRow(12, 25, DisplayName = "Christmas 1")]
        [DataRow(12, 26, DisplayName = "Christmas 2")]
        [DataTestMethod]
        public void IsTollFree_2017(int month, int day)
        {
            // Arrange
            DateTime date = new DateTime(2017, month, day);

            // Act
            bool actual = Sut.IsTollFree(date);

            // Assert
            Assert.IsTrue(actual);
        }

        [DataRow(2005, 03, 27, DisplayName = "2005")]
        [DataRow(2006, 04, 16, DisplayName = "2006")]
        [DataRow(2009, 04, 12, DisplayName = "2009")]
        [DataRow(2011, 04, 24, DisplayName = "2011")]
        [DataRow(2014, 04, 20, DisplayName = "2014")]
        [DataRow(2016, 03, 27, DisplayName = "2016")]
        [DataRow(2017, 04, 16, DisplayName = "2017")]
        [DataRow(2018, 04, 01, DisplayName = "2018")]
        [DataRow(2049, 04, 18, DisplayName = "2049")]
        [DataRow(2076, 04, 19, DisplayName = "2076")]
        [DataTestMethod]
        public void IsTollFree_Easter(int year, int month, int day)
        {
            // Arrange
            DateTime date = new DateTime(year, month, day);
            var datesToCheck = new []
            {
                date.AddDays(-3),
                date.AddDays(-2),
                date.AddDays(-1),
                date,
                date.AddDays(1),
            };

            // Act
            IEnumerable<bool> actual = datesToCheck.Select(Sut.IsTollFree);

            // Assert
            Assert.IsTrue(actual.All(x => x == true),
                $"{date:d} is not correctly calculated as Easter Day.");
        }

        [DataRow(1861, DisplayName = "1861")]
        [DataRow(2004, DisplayName = "2004")]
        [DataRow(2100, DisplayName = "2100")]
        [DataRow(3000, DisplayName = "3000")]
        [DataTestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsTollFree_Before2005AndAfter2099_ThrowsException(int year)
        {
            // Arrange
            DateTime date = new DateTime(year, 12, 31);

            // Act
            Sut.IsTollFree(date);
        }

        [TestMethod]
        public void TollFreeDaysForYear_2017()
        {
            // Arrange
            const int year = 2017;
            var fixedDates = new List<(int month, int day)>()
            {
                (01, 05), (01, 06), (04, 13), (04, 14), (04, 17),
                (05, 01), (05, 24), (05, 25), (06, 05), (06, 06),
                (06, 23), (11, 03), (12, 25), (12, 26)
            }.Select(x => new DateTime(year, x.month, x.day));

            Calendar calendar = CultureInfo.CreateSpecificCulture("sv-SE").Calendar;

            // Enumerate all weekend, July and toll free dates of 2017
            var expected = Enumerable.Range(1, 12)
                .SelectMany(month => Enumerable.Range(1, calendar.GetDaysInMonth(year, month))
                    .Select(day => new DateTime(year, month, day)))
                .Where(d => calendar.GetDayOfWeek(d) == DayOfWeek.Saturday ||
                            calendar.GetDayOfWeek(d) == DayOfWeek.Sunday ||
                            d.Month == 7 ||
                            fixedDates.Contains(d));

            // Act
            var actual = Sut.TollFreeDaysForYear(year);

            // Assert
            var daysNotExpected = actual.Except(expected);
            var daysMissing = expected.Except(actual);

            Assert.IsTrue(!daysNotExpected.Any() && !daysMissing.Any(),
                $"Toll free days for {year} does not match expected:" +
                (daysNotExpected.Any() ? $"\r\n\r\nNot expected: {string.Join(", ", daysNotExpected.Select(d => d.ToString("d")))}" : string.Empty) +
                (daysMissing.Any() ? $"\r\n\r\nMissing: {string.Join(", ", daysMissing.Select(d => d.ToString("d")))}" : string.Empty));
        }

        [DataRow(1861, DisplayName = "1861")]
        [DataRow(2004, DisplayName = "2004")]
        [DataRow(2100, DisplayName = "2100")]
        [DataRow(3000, DisplayName = "3000")]
        [DataTestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TollFreeDaysForYear_Before2005AndAfter2099_ThrowsException(int year)
        {
            // Act
            Sut.TollFreeDaysForYear(year).Count();
        }
    }
}
