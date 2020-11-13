using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class TollFeePeriodTests
    {
        private ITimeTable fakeTimeTable;
        private TollFeePeriod _sut;

        [TestInitialize]
        public void Initialize()
        {
            fakeTimeTable = new Fake<ITimeTable>().FakedObject;
            _sut = new TollFeePeriod(fakeTimeTable);
        }


        [TestMethod]
        public void WhenGivenAPeriodReturnsHighestToll()
        {
            A.CallTo(() => fakeTimeTable.GetFeeAtTimeStamp(A<DateTime>.Ignored)).Returns(5).Once();          
            A.CallTo(() => fakeTimeTable.GetFeeAtTimeStamp(A<DateTime>.Ignored)).Returns(8).Twice();
            var dateList = new[] { new DateTime(2020, 10, 22, 6, 6, 50), new DateTime(2020, 10, 22, 9, 6, 50) };
            var result = _sut.GetHighestFeeInPeriod(dateList);
            result.Should().Be(8);
            A.CallTo(() => fakeTimeTable.GetFeeAtTimeStamp(A<DateTime>.Ignored)).MustHaveHappenedTwiceExactly();
        }
    }
}
