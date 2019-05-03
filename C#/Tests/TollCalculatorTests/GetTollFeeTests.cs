using System;
using System.Collections.Generic;
using NUnit.Framework;
using TollFeeCalculator.TollFeeAmount;
using TollFeeCalculator.Vehicles;

namespace TollCalculatorTests
{
    public class GetTollFeeTests
    {
        private TollFeeCalculator.TollCalculator _sut;
        private const int MAX_FEE = 60;

        [SetUp]
        public void Setup()
        {
            var tollFeeService = new TollFeeAmountService();
            _sut = new TollFeeCalculator.TollCalculator(tollFeeService);
        }
        [Test]
        public void It_shall_return_0_if_no_toll_date()
        {

            var result = _sut.GetTollFee(new Car(), new List<DateTime>());
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void It_shall_only_fee_once_per_hour_and_return_highest_fee()
        {
            var result = _sut.GetTollFee(new Car(), new List<DateTime> {
                new DateTime(2013, 1, 2, 7, 30,0),//18
                new DateTime(2013, 1, 2, 8, 1,0)//13
            });

            Assert.That(result, Is.EqualTo(18));
        }
        [Test]
        public void It_shall_return_max_60_fee_total()
        {
            var dates = new List<DateTime> {
                new DateTime(2013, 1, 2, 7, 0,0),//18
                new DateTime(2013, 1, 2, 8, 1,0),//13
                new DateTime(2013, 1, 2, 10, 2,0),//13
                new DateTime(2013, 1, 2, 11, 3,0),//8
                new DateTime(2013, 1, 2, 12, 4,0),//8
                new DateTime(2013, 1, 2, 13, 5,0),//8
            };
            //Total 63
            var result = _sut.GetTollFee(new Car(), dates);
            Assert.That(result, Is.EqualTo(MAX_FEE));
        }


    }
}
