using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator.Extensions;

namespace TollFeeCalculatorTests
{
   [TestClass]
   public class DateTimeExtensionsTests
   {
      private const int ArbitraryYear = 2000;
      private const int ArbitraryDay = 1;
      private const int JulyMonth = 7;

      [TestMethod]
      public void IsJulyReturnsTrueIfItIsJuly()
      {
         Assert.IsTrue(new DateTime(ArbitraryYear, JulyMonth, ArbitraryDay).IsJuly());
      }

      [TestMethod]
      public void IsJulyReturnsFalseIfItIsNotJuly()
      {
         for (var month = 1; month <= 12; ++month)
         {
            if (month == JulyMonth) continue;

            Assert.IsFalse(new DateTime(ArbitraryYear, month, ArbitraryDay).IsJuly());
         }
      }

      [TestMethod]
      public void IsWeekendReturnsTrueIfItIsSaturday()
      {
         var saturday = new DateTime(2017, 11, 25);
         Assert.AreEqual(DayOfWeek.Saturday, saturday.DayOfWeek);
         Assert.IsTrue(saturday.IsWeekend());
      }

      [TestMethod]
      public void IsWeekendReturnsTrueIfItIsSunday()
      {
         var sunday = new DateTime(2017, 11, 26);
         Assert.AreEqual(DayOfWeek.Sunday, sunday.DayOfWeek);
         Assert.IsTrue(sunday.IsWeekend());
      }

      [TestMethod]
      public void IsWeekendReturnsFalseIfItIsNotSaturdayNorSunday()
      {
         var monday = new DateTime(2017, 11, 27);
         for (var dayCount = 0; dayCount < 5; ++dayCount)
         {
            Assert.IsFalse(monday.AddDays(dayCount).IsWeekend());
         }
      }
   }
}