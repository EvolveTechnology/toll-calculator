using System;
using Shouldly;
using TollFeeCalculator.HolidayLookup;
using Xunit;

namespace TollFeeCalculator.Tests
{
    public class Sweden2017HolidayLookupTests
    {
        [Fact]
        public void HolidayWithFixedDate_ReturnsTrue()
        {
            IHolidayLookup holidayLookup = new SwedenHolidayLookupFor2017();
            holidayLookup.IsPublicHoliday(new DateTime(2017, (int) Month.December, 31)).ShouldBeTrue();
            holidayLookup.IsPublicHoliday(new DateTime(2017, (int) Month.May, 1)).ShouldBeTrue();
            holidayLookup.IsPublicHoliday(new DateTime(2017, (int) Month.June, 6)).ShouldBeTrue();
        }

        [Fact]
        public void NonHoliday_ReturnsFalse()
        {
            IHolidayLookup holidayLookup = new SwedenHolidayLookupFor2017();
            holidayLookup.IsPublicHoliday(new DateTime(2017, (int) Month.December, 5)).ShouldBeFalse();
            holidayLookup.IsPublicHoliday(new DateTime(2017, (int) Month.May, 9)).ShouldBeFalse();
            holidayLookup.IsPublicHoliday(new DateTime(2017, (int) Month.June, 22)).ShouldBeFalse();
        }

        [Fact]
        public void WrongYear_ThrowsArgumentException()
        {
            IHolidayLookup holidayLookup = new SwedenHolidayLookupFor2017();
            Should.Throw<ArgumentException>(() => holidayLookup
                .IsPublicHoliday(new DateTime(2020, (int) Month.December, 5)).ShouldBeFalse());
        }
    }
}
