using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using TollFeeCalculator;

namespace TollFeeCalculatorTests
{
    [TestFixture]
    public class TestsForDefaultConfig
    {
        [Test]
        public void TestCannotInstantiateCalculatorWithNullService()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TollCalculator(null));
            Assert.AreEqual("Value cannot be null.\r\nParameter name: tollFeeService", ex.Message);
        }

        [Test]
        public void TestCannoCallCalculatorWithNullVehicle()
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var ex = Assert.Throws<ArgumentNullException>(() => calc.GetTollFee(null, new List<DateTime>()));
            Assert.AreEqual("Value cannot be null.\r\nParameter name: vehicle", ex.Message);
        }

        [Test]
        public void TestCannoCallCalculatorWithNullDates()
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var ex = Assert.Throws<ArgumentNullException>(() => calc.GetTollFee(new Car(), null));
            Assert.AreEqual("Value cannot be null.\r\nParameter name: dates", ex.Message);
        }

        [Test]
        public void TestThreeDisjointPassages()
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "06:00", "07:00", "08:00"));
            Assert.AreEqual(8 + 18 + 13, fee);
        }

        [Test]
        public void TestThreePassagesWhereTwoAreClose()
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "06:00", "06:59", "08:00"));
            Assert.AreEqual(Math.Max(8, 13) + 13, fee);
        }

        [Test]
        public void TestThreePassagesWhereAllAreClose()
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "15:15", "15:30", "15:45"));
            Assert.AreEqual(18, fee);
        }

        [Test]
        public void TestManyPassages()
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2021-01-04", "05:55", "06:30", "11:35", "12:35", "16:30", "17:10"));
            Assert.AreEqual(13 + 8 + 8 + 18, fee);
        }

        [TestCase(typeof(Motorbike), 0)]
        [TestCase(typeof(Car), 13)]  // make sure not everything counts to zero for the selected time passage
        public void TestDifferentVehicleTypes(Type type, decimal expected)
        {
            var vehicle = (IVehicle) Activator.CreateInstance(type);
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(vehicle, timeSeries("2021-01-04", "08:10"));
            Assert.AreEqual(expected, fee);
        }

        [TestCase("2021-01-01", 0)]
        [TestCase("2021-01-02", 0)]
        [TestCase("2021-01-03", 0)]
        [TestCase("2021-01-04", 13)]  // make sure not everything counts to zero for the selected time passage
        [TestCase("2021-01-05", 13)]
        [TestCase("2021-01-06", 0)]
        [TestCase("2021-01-07", 13)]  
        public void TestRedDatesAndWeekEnds(string datePart, decimal expected)
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries(datePart, "08:00"));
            Assert.AreEqual(expected, fee);
        }

        [TestCase("00:00", 0)]
        [TestCase("00:00,00:00", 0)]
        [TestCase("05:59", 0)]
        [TestCase("05:59,05:59", 0)]
        [TestCase("05:59,05:59,06:00,06:00", 8)]
        [TestCase("23:59", 0)]
        [TestCase("23:59,23:59", 0)]
        [TestCase("00:00,00:00,23:59,23:59", 0)]
        [TestCase("06:40,07:39", 18)]
        [TestCase("06:40,07:40", 18+13)]
        public void TestCornerCases(string times, int expected)
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), timeSeries("2020-12-31", times.Split(",".ToCharArray())));
            Assert.AreEqual(expected, fee);
        }

        [Test]
        public void TestThatThereIsAMaximumFee()
        {
            var calc = new TollCalculator(new defaultTollFeeService());
            var fee = calc.GetTollFee(new Car(), Enumerable.Range(0, 48).Select(_ => new DateTime(2021, 3, 1).AddMinutes(30 * _)).ToList());
            Assert.AreEqual(60, fee);
        }

        //[Test, Explicit]
        //public void RipOutOldTimeTable()
        //{
        //    var oc = new OldTollCalculator();
        //    var d = new DateTime(2021, 1, 4);
        //    var lastFee = -1;
        //    for (var i = 0; i < 48; i++)
        //    {
        //        var fee = oc.GetTollFee(d, new Car());
        //        if (fee != lastFee)
        //        {
        //            Console.WriteLine($"new TollFeeByTime {{ Hour = {d:HH}, Minute = {d:mm}, Fee = {fee} }},");
        //            lastFee = fee;
        //        }
        //        d = d.AddMinutes(30);
        //    }
        //}

        private static List<DateTime> timeSeries(string datepart, params string[] times)
        {
            var date = DateTime.ParseExact(datepart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return times
                .Select(_ => _.Split(":".ToCharArray()))
                .Select(_ => date.AddHours(int.Parse(_[0])).AddMinutes(int.Parse(_[1])))
                .ToList();
        }

        private class defaultTollFeeService : BasicTollFeeService
        {
            public defaultTollFeeService()
            {
                FeesByVehicleTypeAndTime.Add(VehicleType.Car, new List<TollFeeByTime>
            {
                new TollFeeByTime {Hour = 06, Minute = 00, Fee = 8},
                new TollFeeByTime {Hour = 06, Minute = 30, Fee = 13},
                new TollFeeByTime {Hour = 07, Minute = 00, Fee = 18},
                new TollFeeByTime {Hour = 08, Minute = 00, Fee = 13},
                new TollFeeByTime {Hour = 08, Minute = 30, Fee = 8},
                new TollFeeByTime {Hour = 09, Minute = 00, Fee = 0},
                new TollFeeByTime {Hour = 09, Minute = 30, Fee = 8},
                // i assume this half hour pending is just a bug in the original code...
                //new TollFeeByTime {Hour = 10, Minute = 00, Fee = 0},
                //new TollFeeByTime {Hour = 10, Minute = 30, Fee = 8},
                //new TollFeeByTime {Hour = 11, Minute = 00, Fee = 0},
                //new TollFeeByTime {Hour = 11, Minute = 30, Fee = 8},
                //new TollFeeByTime {Hour = 12, Minute = 00, Fee = 0},
                //new TollFeeByTime {Hour = 12, Minute = 30, Fee = 8},
                //new TollFeeByTime {Hour = 13, Minute = 00, Fee = 0},
                //new TollFeeByTime {Hour = 13, Minute = 30, Fee = 8},
                //new TollFeeByTime {Hour = 14, Minute = 00, Fee = 0},
                //new TollFeeByTime {Hour = 14, Minute = 30, Fee = 8},
                new TollFeeByTime {Hour = 15, Minute = 00, Fee = 13},
                new TollFeeByTime {Hour = 15, Minute = 30, Fee = 18},
                new TollFeeByTime {Hour = 17, Minute = 00, Fee = 13},
                new TollFeeByTime {Hour = 18, Minute = 00, Fee = 8},
                new TollFeeByTime {Hour = 18, Minute = 30, Fee = 0}
            });
                RedDates.AddRange(new[]
                    {
                    new DateTime(2021, 1, 1),
                    new DateTime(2021, 1, 6)
                }
                );

                FreeTimeSlotPassageInMinutes = 60;
                MaximumFeeForOneDay = 60;
            }
        }

    }

}
