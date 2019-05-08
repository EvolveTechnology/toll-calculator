using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator.TollFeeTime;

namespace TollCalculatorTests
{
    public class IsTollFreeDateTests
    {
        private TollFeeTimeService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TollFeeTimeService();
        }

        [TestCase(DayOfWeek.Saturday)]
        [TestCase(DayOfWeek.Sunday)]
        public void It_shall_return_true_if_weekend(DayOfWeek day)
        {
            var date = Enumerable.Range(1, 7).Select(i => new DateTime(2013, 1, i))
                .First(d => d.DayOfWeek == day);
            var result = _sut.IsTollFreeDate(date);
            Assert.That(result, Is.True);
        }

        [Test]
        public void It_shall_return_false_if_not_year_2013()
        {
            var result = _sut.IsTollFreeDate(new DateTime(2019, 5, 8));
            Assert.That(result, Is.False);
        }

        [TestCaseSource(nameof(FeeFreeDays))]
        public void It_shall_return_true_if_fee_free_day(DateTime date)
        {
            var result = _sut.IsTollFreeDate(date);
            Assert.That(result, Is.True);
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
    }
}
