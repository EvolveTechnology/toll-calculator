using System;
using FluentAssertions;
using Toll.Calculator.UnitTests.Common;
using Xunit;

namespace Toll.Calculator.Domain.UnitTests
{
    public class TimeStampTests : UnitTestBase<TimeStamp>
    {
        #region Smaller Or Equal To

        [Fact]
        public void ForLesserOrEqual_WhenTimeStampsAreIdentical_ThenReturnTrue()
        {
            var t1 = new TimeStamp(new DateTime(1, 1, 1, 10, 50, 0));
            var t2 = new TimeStamp(new DateTime(2, 1, 1, 10, 50, 0));

            var result = t1 <= t2;

            result.Should().BeTrue();
        }

        [Fact]
        public void ForLesserOrEqual_WhenT1Is1MinuteBigger_ThenReturnFalse()
        {
            var t1 = new TimeStamp(new DateTime(1, 1, 1, 10, 51, 0));
            var t2 = new TimeStamp(new DateTime(2, 1, 1, 10, 50, 0));

            var result = t1 <= t2;

            result.Should().BeFalse();
        }

        [Fact]
        public void ForLesserOrEqual_WhenT1Is1MinuteSmaller_ThenReturnTrue()
        {
            var t1 = new TimeStamp(new DateTime(1, 1, 1, 10, 49, 0));
            var t2 = new TimeStamp(new DateTime(2, 1, 1, 10, 50, 0));

            var result = t1 <= t2;

            result.Should().BeTrue();
        }

        #endregion

        #region Greater Or Equal To

        [Fact]
        public void ForGreaterOrEqual_WhenT1Is1MinuteSmaller_ThenReturnFalse()
        {
            var t1 = new TimeStamp(new DateTime(1, 1, 1, 10, 49, 0));
            var t2 = new TimeStamp(new DateTime(2, 1, 1, 10, 50, 0));

            var result = t1 >= t2;

            result.Should().BeFalse();
        }

        [Fact]
        public void ForGreaterOrEqual_WhenT1Is1MinuteBigger_ThenReturnTrue()
        {
            var t1 = new TimeStamp(new DateTime(1, 1, 1, 10, 51, 0));
            var t2 = new TimeStamp(new DateTime(2, 1, 1, 10, 50, 0));

            var result = t1 >= t2;

            result.Should().BeTrue();
        }

        [Fact]
        public void ForGreaterOrEqual_WhenTimeStampsAreIdentical_ThenReturnTrue()
        {
            var t1 = new TimeStamp(new DateTime(1, 1, 1, 10, 50, 0));
            var t2 = new TimeStamp(new DateTime(2, 1, 1, 10, 50, 0));

            var result = t1 >= t2;

            result.Should().BeTrue();
        }

        #endregion
    }
}
