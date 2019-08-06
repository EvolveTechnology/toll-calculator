using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests
{
	[TestClass()]
	public class TollCalculatorTests
	{
		/**
		 * Tests for GetTollFee(Vehicles vehicle, DateTime[] dates)
		 */
		[TestMethod()]
		public void TestGetTollFeeTollFreeVehicleShouldReturnZero()
		{
			List<DateTime> dates = new List<DateTime>();
			dates.AddRange(new List<DateTime>
			{
				new DateTime(2019, 04, 01, 14, 30, 0),
				new DateTime(2019, 04, 01, 17, 0, 0)
			 });

			Assert.AreEqual(0, TollCalculator.GetTollFee(TollCalculator.Vehicles.Motorbike, dates));
		}

		[TestMethod()]
		public void TestGetTollFeeShouldReturnMaxFeeSixty()
		{
			List<DateTime> dates = new List<DateTime>();
			dates.AddRange(new List<DateTime>
			{
				new DateTime(2019, 04, 01, 6, 0, 0), //Fee is 8
				new DateTime(2019, 04, 01, 7, 0, 0), //Fee is 18
				new DateTime(2019, 04, 01, 8, 0, 0), //Fee is 13
				new DateTime(2019, 04, 01, 15, 0, 0), //Fee is 13
				new DateTime(2019, 04, 01, 16, 0, 0), //Fee is 18
				new DateTime(2019, 04, 01, 18, 0, 0) //Fee is 8
			 });

			//Check that maximum fee is 60 (total fee is 78)
			Assert.AreEqual(60, TollCalculator.GetTollFee(TollCalculator.Vehicles.Car, dates));
		}

		[TestMethod()]
		public void TestGetTollFeeShouldReturnTotalFee()
		{
			List<DateTime> dates = new List<DateTime>();
			dates.AddRange(new List<DateTime>
			{
				new DateTime(2019, 04, 01, 6, 0, 0), //Fee is 8
				new DateTime(2019, 04, 01, 8, 0, 0), //Fee is 13
				new DateTime(2019, 04, 01, 15, 30, 0) //Fee is 18
			 });

			//Check that total fee is 39 (8+13+18)
			Assert.AreEqual(39, TollCalculator.GetTollFee(TollCalculator.Vehicles.Car, dates));
		}

		//Test to check that the highest fee within the same hour period is returned
		[TestMethod()]
		public void TestGetTollFeeShouldReturnHighestFeeForOneHour()
		{
			List<DateTime> dates = new List<DateTime>();
			dates.AddRange(new List<DateTime>
			{
				new DateTime(2019, 04, 01, 6, 0, 0), //Fee is 8
				new DateTime(2019, 04, 01, 6, 30, 0), // Fee is 13
				new DateTime(2019, 04, 01, 6, 10, 0) // Fee is 8
			 });

			//Check that returned fee is 13 (highest fee in the same hour period)
			Assert.AreEqual(13, TollCalculator.GetTollFee(TollCalculator.Vehicles.Car, dates));
		}

		//Test to check that the highest fee is returned even if the dates are'nt ordered
		[TestMethod()]
		public void TestGetTollFeeShouldReturnHighestFeeForOneHourWhenDatesAreUnordered()
		{
			List<DateTime> dates = new List<DateTime>();
			dates.AddRange(new List<DateTime>
			{
				new DateTime(2019, 04, 01, 6, 30, 0), // Fee is 13
				new DateTime(2019, 04, 01, 6, 0, 0), //Fee is 8
				new DateTime(2019, 04, 01, 6, 10, 0) // Fee is 8
			 });

			//Check that returned fee is 13 (highest fee in the same hour period)
			Assert.AreEqual(13, TollCalculator.GetTollFee(TollCalculator.Vehicles.Car, dates));
		}

		/**
		 * Tests for GetTollFee(date)
		 */
		[TestMethod()]
		public void TestGetTollFeeTollFreeTimeShouldReturnZero()
		{
			//Check that toll free time returns zero fee
			Assert.AreEqual(0, TollCalculator.GetTollFee(new DateTime(2019, 04, 01, 19, 0, 0)));
		}

		/**
		 * Tests for IsTollFeeEight()
		 */
		[TestMethod()]
		public void TestIsTollFeeEightShouldReturnTrue()
		{
			Assert.IsTrue(TollCalculator.IsTollFeeEight(new DateTime(2019, 04, 01, 6, 0, 0)));
		}

		[TestMethod()]
		public void TestIsTollFeeEightShouldReturnFalse()
		{
			Assert.IsFalse(TollCalculator.IsTollFeeEight(new DateTime(2019, 04, 01, 6, 30, 0)));
		}

		/**
		 * Tests for IsTollFeeThirteen()
		 */
		[TestMethod()]
		public void TestIsTollFeeThirteenShouldReturnTrue()
		{
			Assert.IsTrue(TollCalculator.IsTollFeeThirteen(new DateTime(2019, 04, 01, 6, 30, 0)));
		}

		[TestMethod()]
		public void TestIsTollFeeThirteenShouldReturnFalse()
		{
			Assert.IsFalse(TollCalculator.IsTollFeeThirteen(new DateTime(2019, 04, 01, 6, 0, 0)));
		}

		/**
		 * Tests for IsTollFeeEighteen()
		 */
		[TestMethod()]
		public void TestIsTollFeeEighteenShouldReturnTrue()
		{
			Assert.IsTrue(TollCalculator.IsTollFeeEighteen(new DateTime(2019, 04, 01, 7, 0, 0)));
		}

		[TestMethod()]
		public void TestIsTollFeeEighteenShouldReturnFalse()
		{
			Assert.IsFalse(TollCalculator.IsTollFeeEighteen(new DateTime(2019, 04, 01, 6, 30, 0)));
		}

		/**
		 * Tests for IsTollFreeVehicle
		 */
		[TestMethod()]
		public void TestIsTollFreeVehicleShouldReturnTrue()
		{
			Assert.IsTrue(TollCalculator.IsTollFreeVehicle(TollCalculator.Vehicles.Motorbike));
		}

		[TestMethod()]
		public void TestIsTollFreeVehicleShouldReturnFalse()
		{
			Assert.IsFalse(TollCalculator.IsTollFreeVehicle(TollCalculator.Vehicles.Car));
		}

		/**
		 * Tests for TimeIsBetween
		 */
		[TestMethod()]
		public void TestTimeIsBetweenShouldReturnTrue()
		{
			Assert.IsTrue(TollCalculator.TimeIsBetween(new DateTime(2019, 06, 15, 6, 36, 0), new TimeSpan(6, 30, 0), new TimeSpan(6, 50, 0)));
		}

		[TestMethod()]
		public void TestTimeIsBetweenShouldReturnFalse()
		{
			Assert.IsFalse(TollCalculator.TimeIsBetween(new DateTime(2019, 06, 15, 6, 29, 0), new TimeSpan(6, 30, 0), new TimeSpan(6, 50, 0)));
		}

		/**
		 * Tests for IsTollFreeDate
		 */
		[TestMethod()]
		public void TestIsTollFreeDateWeekend()
		{
			//Saturday should be toll free
			Assert.IsTrue(TollCalculator.IsTollFreeDate(new DateTime(2019, 06, 15)));
		}

		[TestMethod()]
		public void TestIsTollFreeDateHoliday()
		{
			//Nationdal day should be toll free
			Assert.IsTrue(TollCalculator.IsTollFreeDate(new DateTime(2019, 06, 06)));
		}
	}
}