using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nager.Date;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Services;
using Xunit;

namespace TollFeeCalculator.Test
{
    public class TollFreeDatesTests
    {
        public static IEnumerable<object[]> HolidaysInAustralia =>
            new List<object[]>
            {
                new object[] { CountryCode.AU, new DateTime(2020,04,10)}
            };

        public static IEnumerable<object[]> AdditionalHolidays =>
            new List<object[]>
            {
                new object[]
                {
                    new List<DateTime>
                    {
                        new DateTime(2019,05,06),
                        new DateTime(2019,06,02)
                    }
                   
                }
            };

        public static IEnumerable<object[]> HolidaysToRemove =>
            new List<object[]>
            {
                new object[]
                {
                    new List<DateTime>
                    {
                        new DateTime(2019,01,01),
                        new DateTime(2019,12,25)
                    }

                }
            };

        [Theory]
        [MemberData(nameof(HolidaysInAustralia))]
        public void WhenInjectWithDifferentCountryCode_ReturnHolidaysAccordingly(CountryCode countryCode, DateTime holiday)
        {
            ITollFreeDates tollFreeDates = new TollFreeDates(countryCode);

            var result = tollFreeDates.IsTollFreeDate(holiday);

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(AdditionalHolidays))]
        public void WhenInjectWithAdditionalHolidays_ReturnHolidaysAccordingly(List<DateTime> holiday)
        {
            ITollFreeDates tollFreeDates = new TollFreeDates(additionalHoldiays: holiday, new List<DateTime>());

            var result = tollFreeDates.IsTollFreeDate(holiday.First());

            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(AdditionalHolidays))]
        public void WhenInjectWithRemoveHolidays_ReturnHolidaysAccordingly(List<DateTime> holiday)
        {
            ITollFreeDates tollFreeDates = new TollFreeDates(additionalHoldiays: new List<DateTime>(),holiday);

            var result = tollFreeDates.IsTollFreeDate(holiday.First());

            Assert.False(result);
        }

        [Fact]
        public void SaturdayDateIsGiven_ReturnAsAHoliday()
        {
            ITollFreeDates tollFreeDates = new TollFreeDates();

            var result = tollFreeDates.IsTollFreeDate(new DateTime(2019,08,10));

            Assert.True(result);
        }

        [Fact]
        public void SundayDateIsGiven_ReturnAsAHoliday()
        {
            ITollFreeDates tollFreeDates = new TollFreeDates();

            var result = tollFreeDates.IsTollFreeDate(new DateTime(2019, 08, 11));

            Assert.True(result);
        }

        [Fact]
        public void WhenNormalDayIsGiven_ReturnAsNotAHoliday()
        {
            ITollFreeDates tollFreeDates = new TollFreeDates();

            var result = tollFreeDates.IsTollFreeDate(new DateTime(2019, 08, 14));

            Assert.False(result);
        }
    }
}
