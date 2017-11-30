using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
   [TestClass]
   public class TimeToTollFeeTests
   {
      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void ThrowsIfConstructedWithEmptyList()
      {
         // Intentionally unused return value
         new TimeToTollFee(new List<TollRate>());
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentException))]
      public void ThrowsIfConstructedWithListNotStartingAtMidnight()
      {
         var arbitraryTimeSpanGreaterThanZero = new TimeSpan(1, 0, 0);
         var aribtraryMoney = new Money(0);

         // Intentionally unused return value
         new TimeToTollFee(new List<TollRate> { new TollRate(arbitraryTimeSpanGreaterThanZero, aribtraryMoney) });
      }

      [TestMethod]
      public void GetTollRateFindsCorrectRate()
      {
         var timeToTollFee = new TimeToTollFee(new List<TollRate> { MakeTollRate(0, 0), MakeTollRate(1, 1), MakeTollRate(2, 0), MakeTollRate(3, 3) });

         Assert.AreEqual(0, timeToTollFee.GetTollRate(MakeDateTime(0, 0)).Amount);
         Assert.AreEqual(0, timeToTollFee.GetTollRate(MakeDateTime(0, 30)).Amount);
         Assert.AreEqual(1, timeToTollFee.GetTollRate(MakeDateTime(1, 0)).Amount);
         Assert.AreEqual(1, timeToTollFee.GetTollRate(MakeDateTime(1, 30)).Amount);
         Assert.AreEqual(0, timeToTollFee.GetTollRate(MakeDateTime(2, 0)).Amount);
         Assert.AreEqual(0, timeToTollFee.GetTollRate(MakeDateTime(2, 30)).Amount);
         Assert.AreEqual(3, timeToTollFee.GetTollRate(MakeDateTime(3, 0)).Amount);
         Assert.AreEqual(3, timeToTollFee.GetTollRate(MakeDateTime(3, 30)).Amount);
         Assert.AreEqual(3, timeToTollFee.GetTollRate(MakeDateTime(23, 59)).Amount);
      }

      private static TollRate MakeTollRate(int hour, int fee)
      {
         return new TollRate(new TimeSpan(hour, 0, 0), new Money(fee));
      }

      private static DateTime MakeDateTime(int hours, int minutes)
      {
         const int ArbitraryYear = 1;
         const int ArbitraryMonth = 1;
         const int ArbitraryDay = 1;
         return new DateTime(ArbitraryYear, ArbitraryMonth, ArbitraryDay, hours, minutes, 0);
      }
   }
}