using NUnit.Framework;
using System;
using TollFeeCalculator;

namespace TollCalculatorTest
{
    public class TollTests
    {
        [Test]
        public void Test_regular_workday_car()
        {
            var vehicle = new Car();
            var dates = new[]
            {
                new DateTime(2019, 10, 21, 7, 30, 0),
                new DateTime(2019, 10, 21, 15, 31, 0)
            };
            var expectedFee = 36;
            var calculator = new TollCalculator();
            Assert.AreEqual(calculator.GetTotalTollFee(vehicle, dates), expectedFee);
        }

        [Test]
        public void Test_crazy_morning_car()
        {
            var vehicle = new Car();
            var dates = new[]
            {
                new DateTime(2019, 10, 21, 6, 0, 0),
                new DateTime(2019, 10, 21, 6, 15, 0),
                new DateTime(2019, 10, 21, 6, 22, 13),
                new DateTime(2019, 10, 21, 6, 31, 10),
                new DateTime(2019, 10, 21, 6, 40, 0),
                new DateTime(2019, 10, 21, 6, 59, 59)
            };
            var expectedFee = 13;
            var calculator = new TollCalculator();
            Assert.AreEqual(calculator.GetTotalTollFee(vehicle, dates), expectedFee);
        }

        [Test]
        public void Test_max_fee_car()
        {
            var vehicle = new Car();
            var dates = new[]
            {
                new DateTime(2019, 10, 21, 5, 0, 0),
                new DateTime(2019, 10, 21, 6, 0, 0),
                new DateTime(2019, 10, 21, 7, 0, 0),
                new DateTime(2019, 10, 21, 8, 0, 0),
                new DateTime(2019, 10, 21, 9, 0, 0),
                new DateTime(2019, 10, 21, 10, 0, 0),
                new DateTime(2019, 10, 21, 11, 0, 0),
                new DateTime(2019, 10, 21, 12, 0, 0),
                new DateTime(2019, 10, 21, 13, 0, 0),
                new DateTime(2019, 10, 21, 14, 0, 0),
                new DateTime(2019, 10, 21, 15, 0, 0),
                new DateTime(2019, 10, 21, 16, 0, 0),
                new DateTime(2019, 10, 21, 17, 0, 0),
                new DateTime(2019, 10, 21, 18, 0, 0),
                new DateTime(2019, 10, 21, 19, 0, 0),
                new DateTime(2019, 10, 21, 20, 0, 0)

            };
            var expectedFee = 60;
            var calculator = new TollCalculator();
            Assert.AreEqual(calculator.GetTotalTollFee(vehicle, dates), expectedFee);
        }

        [Test]
        public void Test_free_motorbike()
        {
            var vehicle = new Motorbike();
            var dates = new[]
            {
                new DateTime(2019, 10, 21, 6, 0, 0),
                new DateTime(2019, 10, 21, 6, 15, 0),
                new DateTime(2019, 10, 21, 6, 22, 13),
                new DateTime(2019, 10, 21, 6, 31, 10),
                new DateTime(2019, 10, 21, 6, 40, 0),
                new DateTime(2019, 10, 21, 6, 59, 59)
            };
            var expectedFee = 0;
            var calculator = new TollCalculator();
            Assert.AreEqual(calculator.GetTotalTollFee(vehicle, dates), expectedFee);
        }

        [Test]
        public void Test_christmas_car()
        {
            var vehicle = new Car();
            var dates = new[]
            {
                new DateTime(2013, 12, 24, 6, 0, 0),
                new DateTime(2013, 12, 24, 10, 0, 0),
                new DateTime(2013, 12, 24, 15, 0, 0),
                new DateTime(2013, 12, 24, 20, 0, 0)

            };
            var expectedFee = 0;
            var calculator = new TollCalculator();
            Assert.AreEqual(calculator.GetTotalTollFee(vehicle, dates), expectedFee);
        }

        [Test]
        public void Test_passings_different_days()
        {
            var vehicle = new Car();
            var dates = new[]
            {
                new DateTime(2013, 11, 11, 15, 0, 0),
                new DateTime(2013, 11, 12, 20, 0, 0)

            };
            var calculator = new TollCalculator();
            Assert.Throws(typeof(ArgumentException), () => calculator.GetTotalTollFee(vehicle, dates));
        }
    }
}