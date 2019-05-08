using System;
using System.Collections.Generic;
using NUnit.Framework;
using TollFeeCalculator.TollFeeAmount;
using TollFeeCalculator.TollFeeTime;
using TollFeeCalculator.Vehicles;

namespace TollCalculatorTests
{
    public class GetTollFeeAmountTests
    {
        private TollFeeAmountService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new TollFeeAmountService(new TollFeeTimeService());
        }
        
        [Test]
        public void It_shall_return_fee_if_vehicle_is_null_or_uknown()
        {
            var result = _sut.GetTollFeeAmount(GetValidTestDate(), null);
            Assert.That(result, Is.Not.EqualTo(0));
        }

        [Test]
        public void It_shall_return_fee_if_vehicle_is_car()
        {
            var result = _sut.GetTollFeeAmount(GetValidTestDate(), new Car());
            Assert.That(result, Is.Not.EqualTo(0));
        }

        [TestCaseSource(nameof(FeeFreeVehicles))]
        public void It_shall_return_0_amount_if_toll_free_vehicle(IVehicle vehicle)
        {
            var result = _sut.GetTollFeeAmount(GetValidTestDate(), vehicle);
            Assert.That(result, Is.EqualTo(0));
        }
        private static IEnumerable<TestCaseData> FeeFreeVehicles
        {
            get
            {
                yield return new TestCaseData(new Motorbike());
                yield return new TestCaseData(new Tractor());
                yield return new TestCaseData(new Emergency());
                yield return new TestCaseData(new Diplomat());
                yield return new TestCaseData(new Foreign());
                yield return new TestCaseData(new Military());
            }
        }

        private static DateTime GetValidTestDate()
        {
            return new DateTime(2013, 1, 2, 8, 0, 0);
        }
    }
}
