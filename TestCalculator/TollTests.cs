using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toll_Calculator;
using Toll_Calculator.Helpers;
using Toll_Calculator.Models;
using Toll_Calculator.Models.Vehicles;

namespace TestCalculator
{
    [TestClass]
    public class TollTests
    {
        [TestMethod]
        [TestCategory("Toll test")]
        public void TestGetTollFee()
        {
            var fees = TollHelper.GetFeePeriods();
            var dates = new[]
            {
                new DateTime(2018,11,03, 13,45,00),
                new DateTime(2017,08,17, 15,20,00),
                new DateTime(2017,08,17, 15,45,00),
                new DateTime(2017,08,17, 17,15,00),
                new DateTime(2017,08,17, 14,40,00),
                new DateTime(2017,08,17, 17,05,00),
                new DateTime(2017,08,18, 13,05,00)
            };
            var tollcalculator = new TollCalculator();
            var fee = tollcalculator.GetTollFee(new Car(), dates);
            Assert.IsTrue(fee == 47);
        }

        [TestMethod]
        [TestCategory("Toll test")]
        public void TollFreeVehicle()
        {
            var fees = TollHelper.GetFeePeriods();
            var dates = new[]
            {
                new DateTime(2018,11,03, 13,45,00),
                new DateTime(2017,08,17, 15,20,00),
                new DateTime(2017,08,17, 15,45,00),
                new DateTime(2017,08,17, 17,15,00),
                new DateTime(2017,08,17, 14,40,00),
                new DateTime(2017,08,17, 17,05,00),
                new DateTime(2017,08,18, 13,05,00)
            };
            var tollcalculator = new TollCalculator();
            var fee = tollcalculator.GetTollFee(new Motorbike(), dates);
            Assert.IsTrue(fee == 0);
        }

        [TestMethod]
        [TestCategory("Toll test")]
        public void SumLimit()
        {
            var dailySum = new DailySum(100);
            Assert.IsTrue(dailySum.Sum == 60);
        }

        [TestMethod]
        [TestCategory("Toll test")]
        public void NullVehicle()
        {
            var tollCalculator = new TollCalculator();

            var dates = new[]
{
                new DateTime(2018,11,03, 13,45,00),
                new DateTime(2017,08,17, 15,20,00),
                new DateTime(2017,08,17, 15,45,00),
                new DateTime(2017,08,17, 17,15,00),
                new DateTime(2017,08,17, 14,40,00),
                new DateTime(2017,08,17, 17,05,00),
                new DateTime(2017,08,18, 13,05,00)
            };
            var fee = tollCalculator.GetTollFee(null, dates);
            Assert.IsTrue(fee == 0);
        }
    }
}
