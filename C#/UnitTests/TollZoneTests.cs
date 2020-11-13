using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class TollZoneTests
    {
        
        [TestMethod]
        public void WhenGivenTimeStampIsBetweenStartAndEnd_returnsTrue()
        {
            var _sut = new TollZone(new TimeSpan(6, 0,0), new TimeSpan(9, 30,0),7);
            var result = _sut.IsValidZone(new System.DateTime(2020, 12, 12, 7, 38,39));
            result.Should().BeTrue();
        }


        [TestMethod]
        public void WhenGivenTimeStampIsNotBetweenStartAndEnd_returnsfalse()
        {
            var _sut = new TollZone(new TimeSpan(6, 0, 0), new TimeSpan(10, 30, 0), 7);
            var result = _sut.IsValidZone(new System.DateTime(2020, 12, 12, 11, 22, 39));
            result.Should().BeFalse();
        }
    }
}
