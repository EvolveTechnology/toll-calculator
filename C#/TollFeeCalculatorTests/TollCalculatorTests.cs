using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
   [TestClass]
   public class TollCalculatorTests
   {
      private const int DefaultFee = 0;
      private readonly Mock<ITollFreeVehicles> _tollFreeVehicles = new Mock<ITollFreeVehicles>();
      private readonly Mock<ITollFreeDates> _tollFreeDates = new Mock<ITollFreeDates>();
      private readonly Mock<IDailyTollCalculator> _dailyToll = new Mock<IDailyTollCalculator>();
      private readonly Mock<IVehicle> _vehicle = new Mock<IVehicle>();
      private TollCalculator _tollCalculator;

      [TestInitialize]
      public void Setup()
      {
         _tollCalculator = new TollCalculator(_tollFreeVehicles.Object, _tollFreeDates.Object, _dailyToll.Object);
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void GetTollFeeNullVehicleThrows()
      {
         _tollCalculator.GetTollFee(null, new List<DateTime>());
      }

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void GetTollFeeNullDateTimeListThrows()
      {
         _tollCalculator.GetTollFee(_vehicle.Object, null);
      }

      [TestMethod]
      public void GetTollFeeReturnsDefaultFeeIfTollFreeVehicle()
      {
         var arbitrayDate = new List<DateTime> { new DateTime(1, 1, 1) };

         SetupTollFreeVehicles(true);
         SetupTollFreeDates(false);

         var fee = _tollCalculator.GetTollFee(_vehicle.Object, arbitrayDate);

         Assert.AreEqual(DefaultFee, fee);
      }

      [TestMethod]
      public void GetTollFeeReturnsDefaultFeeIfNoDates()
      {
         SetupTollFreeVehicles(false);
         SetupTollFreeDates(false);

         var fee = _tollCalculator.GetTollFee(_vehicle.Object, new List<DateTime>());
         Assert.AreEqual(DefaultFee, fee);
      }

      [TestMethod]
      public void GetTollFeeReturnsDefaultFeeIfTollFreeDate()
      {
         SetupTollFreeVehicles(false);
         SetupTollFreeDates(true);

         var fee = _tollCalculator.GetTollFee(_vehicle.Object, new List<DateTime>());
         Assert.AreEqual(DefaultFee, fee);
      }

      [TestMethod]
      public void GetTollFeeReturnsValueFromDailyTollOtherwise()
      {
         var arbitrayDate = new List<DateTime> { new DateTime(1, 1, 1) };
         SetupTollFreeVehicles(false);
         SetupTollFreeDates(false);

         const int ArbitraryValue = 39824786;
         SetupTollDailyToll(ArbitraryValue);

         var fee = _tollCalculator.GetTollFee(_vehicle.Object, arbitrayDate);
         Assert.AreEqual(ArbitraryValue, fee);
      }

      private void SetupTollFreeVehicles(bool value)
      {
         _tollFreeVehicles.Setup(o => o.IsTollFree(It.IsAny<IVehicle>())).Returns(value);
      }

      private void SetupTollFreeDates(bool value)
      {
         _tollFreeDates.Setup(o => o.IsTollFree(It.IsAny<DateTime>())).Returns(value);
      }

      private void SetupTollDailyToll(int value)
      {
         _dailyToll.Setup(o => o.GetDailyTotal(It.IsAny<List<DateTime>>())).Returns(new Money(value));
      }
   }
}