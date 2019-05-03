using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TollFeeCalculator.TollFeeAmount;
using TollFeeCalculator.Vehicles;

namespace TollCalculatorTests
{
    public class GetTollFeeAmountTests
    {
        private TollFeeAmountService _sut;

        [SetUp]
        public void Setup()
        {
            
            _sut = new TollFeeAmountService();

        }
        private static readonly List<DateTime> FeeFreeDays = new List<DateTime>
        {
            new DateTime(2013,1,1,8,0,0),
            new DateTime(2013,3,28,8,0,0),new DateTime(2013,3,29,8,0,0),
            new DateTime(2013,4,1,8,0,0),new DateTime(2013,4,30,8,0,0),
            new DateTime(2013,5,1,8,0,0),new DateTime(2013,5,8,8,0,0),new DateTime(2013,5,9,8,0,0),
            new DateTime(2013,6,5,8,0,0),new DateTime(2013,6,6,8,0,0),new DateTime(2013,6,21,8,0,0),
            new DateTime(2013,7,1,8,0,0),new DateTime(2013,7,30,8,0,0),
            new DateTime(2013,11,1,8,0,0),
            new DateTime(2013,12,24,8,0,0),new DateTime(2013,12,25,8,0,0),new DateTime(2013,12,26,8,0,0),new DateTime(2013,12,31,8,0,0),

        };
        private static readonly List<IVehicle> FeeFreeVehicles = new List<IVehicle>
        {
            new Motorbike(),
            new Motorbike(),
            new Tractor(),
            new Emergency(),
            new Diplomat(),
            new Foreign(),
            new Military()

        };

        [Test]
        public void It_shall_return_0_if_vehicle_is_null()
        {
            var result = _sut.GetTollFeeAmount(new DateTime(), null);
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCaseSource(nameof(FeeFreeDays))]
        public void It_shall_return_0_amount_if_fee_free_day(DateTime date)
        {
            var result = _sut.GetTollFeeAmount(date, null);
            Assert.That(result, Is.EqualTo(0));
        }
        [TestCase(DayOfWeek.Saturday)]
        [TestCase(DayOfWeek.Sunday)]
        public void It_shall_return_0_amount_if_weekend(DayOfWeek day)
        {
            var date = Enumerable.Range(1, 7).Select(i => new DateTime(2013, 1, i))
                .First(d => d.DayOfWeek == day);
            var result = _sut.GetTollFeeAmount(date, null);
            Assert.That(result, Is.EqualTo(0));
        }

        [TestCaseSource(nameof(FeeFreeVehicles))]
        public void It_shall_return_0_amount_if_toll_free_vehicle(IVehicle vehicle)
        {
            var result = _sut.GetTollFeeAmount(new DateTime(2013, 1, 2, 8, 0, 0), vehicle);
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void It_shall_return_fee_if_vehicle_is_car()
        {
            var result = _sut.GetTollFeeAmount(new DateTime(2013, 1, 2, 8, 0, 0), new Car());
            Assert.That(result, Is.Not.EqualTo(0));
        }

        [TestCase(6,30)]
        [TestCase(6,59)]
        [TestCase(8, 0)]
        [TestCase(8, 29)]
        [TestCase(15, 0)]
        [TestCase(15, 29)]
        [TestCase(17, 00)]
        [TestCase(17, 59)]
        public void It_shall_return_amount_13_fee_time(int hour, int minute)
        {
            var result = _sut.GetTollFeeAmount(new DateTime(2013, 1, 2, hour, minute, 0), new Car());
            Assert.That(result, Is.EqualTo(13));
        }

        [TestCase(6, 0)]
        [TestCase(6, 29)]
        [TestCase(8, 30)]
        [TestCase(14, 59)]
        [TestCase(18, 0)]
        [TestCase(18, 29)]
        public void It_shall_return_amount_8_fee_time(int hour, int minute)
        {
            var result = _sut.GetTollFeeAmount(new DateTime(2013, 1, 2, hour, minute, 0), new Car());
            Assert.That(result, Is.EqualTo(8));
        }

        [TestCase(7, 0)]
        [TestCase(7, 59)]
        [TestCase(15, 30)]
        [TestCase(16, 59)]
        public void It_shall_return_amount_18_fee_time(int hour, int minute)
        {
            var result = _sut.GetTollFeeAmount(new DateTime(2013, 1, 2, hour, minute, 0), new Car());
            Assert.That(result, Is.EqualTo(18));
        }

        [TestCase(15, 30)]
        [TestCase(16, 59)]
        public void It_shall_return_amount_0_fee_if_off_fee_hours(int hour, int minute)
        {
            var result = _sut.GetTollFeeAmount(new DateTime(2013, 1, 2, hour, minute, 0), new Car());
            Assert.That(result, Is.EqualTo(18));
        }
    }
}
