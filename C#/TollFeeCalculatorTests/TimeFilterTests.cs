using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
   [TestClass]
   public class TimeFilterTests
   {
      private readonly TimeFilter _filter = new TimeFilter();

      [TestMethod]
      public void OncePerHourKeepsDatesFurtherApartThanOneHour()
      {
         var times = new List<DateTime> { MakeDateTime(1, 0), MakeDateTime(3, 0), MakeDateTime(5, 0) };
         var result = _filter.OncePerHour(times);
         Assert.AreEqual(3, result.Count());
      }

      [TestMethod]
      public void OncePerHourDiscardsDatesWithinOneHour()
      {
         var times = new List<DateTime> { MakeDateTime(1, 0), MakeDateTime(1, 30), MakeDateTime(1, 59) };
         var result = _filter.OncePerHour(times);
         Assert.AreEqual(1, result.Count());
      }

      [TestMethod]
      public void OncePerHourKeepsDatesExactlyOneHourLater()
      {
         var times = new List<DateTime> { MakeDateTime(1, 0), MakeDateTime(2, 0), MakeDateTime(3, 0) };
         var result = _filter.OncePerHour(times);
         Assert.AreEqual(3, result.Count());
      }

      private static DateTime MakeDateTime(int hour, int minute, int second = 0)
      {
         const int ArbitraryYear = 1;
         const int ArbitraryMonth = 1;
         const int ArbitraryDay = 1;
         return new DateTime(ArbitraryYear, ArbitraryMonth, ArbitraryDay, hour, minute, second);
      }
   }
}