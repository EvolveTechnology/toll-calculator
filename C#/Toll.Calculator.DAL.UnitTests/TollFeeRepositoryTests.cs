using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Toll.Calculator.DAL.Repositories;
using Toll.Calculator.Infrastructure;
using Toll.Calculator.Infrastructure.Options;
using Toll.Calculator.UnitTests.Common;
using Xunit;

namespace Toll.Calculator.DAL.UnitTests
{
    public class TollFeeRepositoryTests : UnitTestBase<TollFeeRepository>
    {
        private readonly IOptions<FeeTimeZonesOptions> _timeZoneOptions;

        public TollFeeRepositoryTests()
        {
            _timeZoneOptions = Fixture.Freeze<IOptions<FeeTimeZonesOptions>>();

            var optionsObject = Fixture.Build<FeeTimeZonesOptions>()
                .With(o => o.FeeTimeZones, new List<string>
                {
                    "06:00-06:29;8",
                    "06:30-06:59;13",
                    "07:00-07:59;18",
                    "08:00-08:29;13",
                    "08:30-14:59;8",
                    "15:00-15:29;13",
                    "15:30-16:59;18",
                    "17:00-17:59;13",
                    "18:00-18:29;8"
                })
                .Create();

            _timeZoneOptions.Value.Returns(optionsObject);
        }

        #region IsTollFreeDateAsync

        public static IEnumerable<object[]> TollFreeData => new List<object[]>
        {
            new object[] {new DateTime(2021, 4, 9), false}, //friday
            new object[] {new DateTime(2021, 4, 10), true}, //saturday
            new object[] {new DateTime(2021, 4, 11), true}, //sunday
            new object[] {new DateTime(2021, 4, 2), true}, //friday, långfredag
            new object[] {new DateTime(2022, 12, 26), true} //monday, annandag jul 2022
        };

        [Theory]
        [MemberData(nameof(TollFreeData))]
        public async Task ForIsTollFreeDate_WhenDateIsPassageDate_ReturnExpectedValue(
            DateTime passageDate,
            bool expectedValue)
        {
            var result = await SUT.IsTollFreeDateAsync(passageDate);

            result.Should().Be(expectedValue);
        }

        #endregion

        #region GetPassageFeeByTimeAsync

        public static IEnumerable<object[]> PassageFeeData => new List<object[]>
        {
            new object[] {new DateTime(2021, 4, 9, 05, 30, 0), 0},
            new object[] {new DateTime(2021, 4, 9, 06, 29, 59), 8},
            new object[] {new DateTime(2021, 4, 9, 06, 30, 59), 13},
            new object[] {new DateTime(2021, 4, 9, 07, 0, 0), 18},
            new object[] {new DateTime(2021, 4, 9, 08, 0, 0), 13},
            new object[] {new DateTime(2021, 4, 9, 08, 30, 0), 8},
            new object[] {new DateTime(2021, 4, 9, 15, 15, 0), 13},
            new object[] {new DateTime(2021, 4, 9, 15, 31, 0), 18},
            new object[] {new DateTime(2021, 4, 9, 17, 31, 0), 13},
            new object[] {new DateTime(2021, 4, 9, 18, 28, 0), 8},
            new object[] {new DateTime(2021, 4, 9, 18, 31, 0), 0},
            new object[] {new DateTime(2021, 4, 9, 23, 59, 59), 0},
            new object[] {new DateTime(2021, 4, 9, 00, 1, 59), 0}
        };

        [Theory]
        [MemberData(nameof(PassageFeeData))]
        public async Task ForGetPassageFeeByTime_WhenDateIsPassageDate_ReturnExpectedValue(
            DateTime passageDate,
            decimal expectedValue)
        {
            var result = await SUT.GetPassageFeeByTimeAsync(passageDate);

            result.Should().Be(expectedValue);
        }

        #endregion
    }
}