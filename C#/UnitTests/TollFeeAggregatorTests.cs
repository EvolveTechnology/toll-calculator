using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class TollFeeAggregatorTests
    {
        private ITollFeePeriod fakeFeePeriod;
        private TollFeeAggregator _sut;

        [TestInitialize]
        public void Initialize()
        {
            fakeFeePeriod = new Fake<ITollFeePeriod>().FakedObject;
            
            _sut = new TollFeeAggregator(fakeFeePeriod);
        }

        [TestMethod]
        public void WhenTimeStampListIsempty_returnZero()
        {
            var result = _sut.GetTotalToll(new List<DateTime>());
            result.Should().Be(0);
        }

        [TestMethod]
        public void GivenSerieOfDateTimes_splitsListAndSumValues()
        {
            var dateList = CreateDateTimeList(13);
            A.CallTo(() => fakeFeePeriod.GetHighestFeeInPeriod(A<List<DateTime>>._)).Returns(4);
            var result = _sut.GetTotalToll(dateList);
            result.Should().Be(12);
            A.CallTo(() => fakeFeePeriod.GetHighestFeeInPeriod(A<List<DateTime>>._)).MustHaveHappened(6, Times.Exactly);
        }

        [TestMethod]
        public void WhenTotalFeeExceedsMaxAmount()
        {
            var dateList = CreateDateTimeList(10);
            A.CallTo(() => fakeFeePeriod.GetHighestFeeInPeriod(A<List<DateTime>>.Ignored)).Returns(75);
            var result = _sut.GetTotalToll(dateList);
            result.Should().Be(60);
        }

        private List<DateTime> CreateDateTimeList(int amount)
        {
            var list = new List<DateTime>();
            for (int a = 0; a < amount; a++)
            {
                list.Add(new DateTime(2020, 12, 24, 7, 1, 1).AddMinutes(a * 15));
            }
            return list;
        }
    }


}
