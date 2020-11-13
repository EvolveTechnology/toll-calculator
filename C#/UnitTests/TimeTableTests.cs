using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class TimeTableTests
    {
        private IConfiguration fakeConf;
        private TimeTable _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            fakeConf = new Fake<IConfiguration>().FakedObject;
            _sut = new TimeTable(fakeConf);
        }

        [TestMethod]
        public void GetFeeAtTime_WhenDateTimeIsFoundReturnsFee()
        {
            var testDate = new DateTime(2020, 10, 23, 10, 10, 10);
            var result =_sut.GetFeeAtTimeStamp(testDate);
            A.CallTo(() => fakeConf.TollZones()).MustHaveHappened();
        }

        [TestMethod]
        public void GetFeeAtTime_WhenDateTimeIsNotincludedReturnZero()
        {
            var testDate = new DateTime(2020, 10, 23, 16, 10, 10);
            A.CallTo(() => fakeConf.TollZones()).Returns(new List<TollZone>());
            var result = _sut.GetFeeAtTimeStamp(testDate);
            A.CallTo(() => fakeConf.TollZones()).MustHaveHappened();
            result.Should().Be(0);
        }
    }
}
