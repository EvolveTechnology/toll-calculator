using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
    [TestFixture]
    public class TestsForNonDefaultConfig
    {
        [Test]
        public void TestThreeDisjointPassages()
        {
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "06:00", "07:00", "08:00"));
            Assert.AreEqual(10 + 10 + 10, fee);
        }

        [Test]
        public void TestThreePassagesWhereTwoAreNotCloseInThisConfig()
        {
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "06:00", "06:59", "08:00"));
            Assert.AreEqual(10 + 10 + 10, fee);
        }

        [Test]
        public void TestThreePassagesWhereTwoAreClose()
        {
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "15:15", "15:30", "15:45"));
            Assert.AreEqual(10 + 10, fee);
        }

        [Test]
        public void TestManyPassages()
        {
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "05:55", "06:30", "11:35", "12:35", "16:30", "17:10"));
            Assert.AreEqual(Math.Min(45, 10 * 6), fee);
        }

        [TestCase(typeof(Motorbike), 5)]
        [TestCase(typeof(Car), 10)]
        public void TestDifferentVehicleTypes(Type type, decimal expected)
        {
            var vehicle = (IVehicle) Activator.CreateInstance(type);
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(vehicle, timeSeries("2021-01-04", "08:10"));
            Assert.AreEqual(expected, fee);
        }

        [TestCase("2021-01-01", 10)]
        [TestCase("2021-01-02", 0)]
        [TestCase("2021-01-03", 0)]
        [TestCase("2021-01-04", 10)]  // make sure not everything counts to zero for the selected time passage
        [TestCase("2021-01-05", 10)]
        [TestCase("2021-01-06", 10)]
        [TestCase("2021-04-01", 10)]
        [TestCase("2021-04-02", 0)]
        [TestCase("2021-04-03", 0)]
        [TestCase("2021-04-04", 0)]
        [TestCase("2021-04-05", 0)]
        [TestCase("2021-04-06", 10)]
        public void TestRedDatesAndWeekEnds(string datePart, decimal expected)
        {
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries(datePart, "08:00"));
            Assert.AreEqual(expected, fee);
        }

        [TestCase("00:00", 10)]
        [TestCase("00:00,00:00", 10)]
        [TestCase("05:59", 10)]
        [TestCase("05:59,05:59", 10)]
        [TestCase("05:59,05:59,06:00,06:00", 10)]
        [TestCase("23:59", 10)]
        [TestCase("23:59,23:59", 10)]
        [TestCase("00:00,00:00,23:59,23:59", 20)]
        public void TestCornerCases(string times, int expected)
        {
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2020-12-31", times.Split(",".ToCharArray())));
            Assert.AreEqual(expected, fee);
        }

        [Test]
        public void TestThatThereIsAMaximumFee()
        {
            var calc = new TollCalculator(new nonDefaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), Enumerable.Range(0, 48).Select(_ => new DateTime(2021, 3, 1).AddMinutes(30 * _)).ToList());
            Assert.AreEqual(45, fee);
        }


        private static List<DateTime> timeSeries(string datepart, params string[] times)
        {
            var date = DateTime.ParseExact(datepart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return times
                .Select(_ => _.Split(":".ToCharArray()))
                .Select(_ => date.AddHours(int.Parse(_[0])).AddMinutes(int.Parse(_[1])))
                .ToList();
        }

        private class nonDefaultTollFeeService : ITollFeeService
        {
            public readonly Dictionary<VehicleType, List<TollFeeByTime>> FeesByVehicleTypeAndTime = new Dictionary<VehicleType, List<TollFeeByTime>>
            {
                {VehicleType.Car, new List<TollFeeByTime> {new TollFeeByTime {Hour = 0, Minute = 00, Fee = 10}}},
                {VehicleType.Motorbike, new List<TollFeeByTime> {new TollFeeByTime {Hour = 0, Minute = 00, Fee = 5}}}
            };

            public readonly List<DateTime> RedDates = new List<DateTime>
            {
                new DateTime(2021, 4, 2),
                new DateTime(2021, 4, 5),
            };

            public List<TollFeeByTime> GetFeeTimeIntervals(VehicleType vehicleType, DateTime date)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || RedDates.Contains(date.Date))
                    return new List<TollFeeByTime>();
                return FeesByVehicleTypeAndTime.TryGetValue(vehicleType, out var list)
                    ? list
                    : new List<TollFeeByTime>();
            }

            public int FreeTimeSlotPassageInMinutes => 30;
            public decimal MaximumFeeForOneDay => 45;
        }

    }

}
