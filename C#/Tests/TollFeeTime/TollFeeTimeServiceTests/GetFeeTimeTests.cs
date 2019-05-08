using NUnit.Framework;
using System;
using TollFeeCalculator.TollFeeTime;

namespace TollCalculatorTests
{
    public class GetFeeTimesAmountTests
    {
        private TollFeeTimeService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TollFeeTimeService();
        }

        [TestCase(6, 30)]
        [TestCase(6, 59)]
        [TestCase(8, 0)]
        [TestCase(8, 29)]
        [TestCase(15, 0)]
        [TestCase(15, 29)]
        [TestCase(17, 00)]
        [TestCase(17, 59)]
        public void It_shall_return_amount_13_fee_time(int hour, int minute)
        {
            var result = _sut.GetFeeTime(new TimeSpan(hour, minute, 0));
            Assert.That(result.Amount, Is.EqualTo(13));
        }

        [TestCase(6, 0)]
        [TestCase(6, 29)]
        [TestCase(8, 30)]
        [TestCase(14, 59)]
        [TestCase(18, 0)]
        [TestCase(18, 29)]
        public void It_shall_return_amount_8_fee_time(int hour, int minute)
        {
            var result = _sut.GetFeeTime(new TimeSpan(hour, minute, 0));
            Assert.That(result.Amount, Is.EqualTo(8));
        }

        [TestCase(7, 0)]
        [TestCase(7, 59)]
        [TestCase(15, 30)]
        [TestCase(16, 59)]
        public void It_shall_return_amount_18_fee_time(int hour, int minute)
        {
            var result = _sut.GetFeeTime(new TimeSpan(hour, minute, 0));
            Assert.That(result.Amount, Is.EqualTo(18));
        }

        [TestCase(0, 0)]
        [TestCase(5, 59)]
        [TestCase(18, 30)]
        public void It_shall_return_amount_0_fee_if_off_fee_hours(int hour, int minute)
        {
            var result = _sut.GetFeeTime(new TimeSpan(hour, minute, 0));
            Assert.That(result.Amount, Is.EqualTo(0));
        }
    }
}
