using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TollFeeCalculator.Sweden.Tests
{
	[TestClass]
	public class CalendarTests
	{
		private Calendar _calendar;

		[TestInitialize]
		public void TestInitialize()
		{
			ConfigurationManager.AppSettings["DateCheckAPIBaseAddress"] = "https://api.dryg.net";
			ConfigurationManager.AppSettings["DateCheckAPIPathAndQuery"] = "/dagar/v1/?datum=";
			_calendar = new Calendar();
		}

		[TestMethod]
		public void NewYearEveIsTollFree()
		{
			var newYearEve = new DateTime(2015, 12, 31);
			Assert.IsTrue(_calendar.IsDateTollFree(newYearEve));
		}

		[TestMethod]
		public void FifthOfJuneIsTollFree()
		{
			var fifthOfJune = new DateTime(2017, 6, 5);
			Assert.IsTrue(_calendar.IsDateTollFree(fifthOfJune));
		}

		[TestMethod]
		public void SaturdayAndSundayAreTollFree()
		{
			var saturday = new DateTime(2017, 10, 21);
			Assert.IsTrue(_calendar.IsDateTollFree(saturday));

			var sunday = new DateTime(2017, 10, 22);
			Assert.IsTrue(_calendar.IsDateTollFree(sunday));
		}

		[TestMethod]
		public void JulyIsTollFree()
		{
			var twentyFirstOfJuly = new DateTime(2017, 7, 21);
			Assert.IsTrue(_calendar.IsDateTollFree(twentyFirstOfJuly));
		}

		[TestMethod]
		public void FirstOfSeptemberIsNotTollFree()
		{
			var firstOfSeptember = new DateTime(2017, 9, 1);
			Assert.IsFalse(_calendar.IsDateTollFree(firstOfSeptember));
		}
	}
}
