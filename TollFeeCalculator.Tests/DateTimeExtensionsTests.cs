using NUnit.Framework;
using System;
using System.Collections.Generic;
using TollFeeCalculator.Utils;

namespace TollFeeCalculator.Tests
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        public static IEnumerable<TestCaseData> TimesInsideInterval
        {
            get
            {
                yield return new TestCaseData(new DateTime(2021, 1, 1, 10, 0, 0));
                yield return new TestCaseData(new DateTime(2021, 1, 1, 12, 30, 0));
                yield return new TestCaseData(new DateTime(2021, 1, 1, 15, 29, 59));
            }
        }

        public static IEnumerable<TestCaseData> TimesOutsideInterval
        {
            get
            {
                yield return new TestCaseData(new DateTime(2021, 1, 1, 9, 59, 59));
                yield return new TestCaseData(new DateTime(2021, 1, 1, 15, 30, 0));
                yield return new TestCaseData(new DateTime(2021, 1, 1, 0, 0, 0));
            }
        }

        [Test]
        [TestCaseSource(nameof(TimesInsideInterval))]
        public void IsBetweenTimes_Returns_True_Inside_Interval(DateTime date)
        {
            Assert.IsTrue(date.IsBetweenTimes("10:00", "15:30"));
        }

        [Test]
        [TestCaseSource(nameof(TimesOutsideInterval))]
        public void IsBetweenTimes_Returns_False_Outside_Interval(DateTime date)
        {
            Assert.IsFalse(date.IsBetweenTimes("10:00", "15:30"));
        }
    }
}
