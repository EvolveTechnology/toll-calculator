using System;
using NSubstitute;
using Shouldly;
using TollFeeCalculator.HolidayLookup;
using TollFeeCalculator.Vehicle;
using Xunit;

namespace TollFeeCalculator.Tests
{
    public class TollFeeCalculatorTests
    {
        [Fact]
        public void GetChargeablePassages_NoPassages_ReturnsNoPassages()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            tollFeeCalculator.GetChargeablePassages(null).ShouldBeEmpty();
            tollFeeCalculator.GetChargeablePassages(new DateTime[0]).ShouldBeEmpty();
        }

        [Fact]
        public void GetChargeablePassages_OnlyOnePassage_ReturnsOnlyOnePassage()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            tollFeeCalculator.GetChargeablePassages(new[] {new DateTime(2017, (int) Month.April, 5)})
                .ShouldContain(new DateTime(2017, (int) Month.April, 5));
        }

        [Fact]
        public void GetChargeablePassages_AllOccurWithinOneHour_ReturnsOnlyOnePassage()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            var passages = tollFeeCalculator.GetChargeablePassages(new[]
            {
                new DateTime(2017, (int) Month.April, 5, 14, 05, 40),
                new DateTime(2017, (int) Month.April, 5, 14, 30, 40),
                new DateTime(2017, (int) Month.April, 5, 14, 59, 40),
                new DateTime(2017, (int) Month.April, 5, 15, 00, 40)
            });
            passages.Count.ShouldBe(1);
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 14, 05, 40));
        }

        [Fact]
        public void GetChargeablePassages_NoneOccurWithinOneHour_ReturnsAllPassages()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            var passages = tollFeeCalculator.GetChargeablePassages(new[]
            {
                new DateTime(2017, (int) Month.April, 5, 14, 05, 40),
                new DateTime(2017, (int) Month.April, 5, 15, 30, 40),
                new DateTime(2017, (int) Month.April, 5, 16, 59, 40),
                new DateTime(2017, (int) Month.April, 5, 18, 00, 40)
            });
            passages.Count.ShouldBe(4);
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 14, 05, 40));
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 15, 30, 40));
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 16, 59, 40));
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 18, 00, 40));
        }

        [Fact]
        public void GetChargeablePassages_SomeOccurWithinOneHour_ReturnsPassagesNotOccurringWithinOneHour()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            var passages = tollFeeCalculator.GetChargeablePassages(new[]
            {
                new DateTime(2017, (int) Month.April, 5, 14, 05, 40),
                new DateTime(2017, (int) Month.April, 5, 15, 30, 40),
                new DateTime(2017, (int) Month.April, 5, 15, 59, 40),
                new DateTime(2017, (int) Month.April, 5, 18, 00, 40),
                new DateTime(2017, (int) Month.April, 5, 18, 40, 40),
                new DateTime(2017, (int) Month.April, 5, 19, 00, 40)
            });
            passages.Count.ShouldBe(4);
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 14, 05, 40));
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 15, 30, 40));
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 18, 00, 40));
            passages.ShouldContain(new DateTime(2017, (int) Month.April, 5, 19, 00, 40));
        }

        [Fact]
        public void CreateTollFeeCalculator_HolidayLookupIsNull_ThrowsArgumentNullException()
        {
            Should.Throw<ArgumentNullException>(() => new TollFeeCalculator(null));
        }

        [Fact]
        public void IsTollFreeDate_Weekend_ReturnsTrue()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            tollFeeCalculator.IsTollFreeDate(new DateTime(2017, (int) Month.November, 26)).ShouldBeTrue();
            tollFeeCalculator.IsTollFreeDate(new DateTime(2017, (int) Month.November, 25)).ShouldBeTrue();
        }

        [Fact]
        public void IsTollFreeDate_PublicHoliday_ReturnsTrue()
        {
            IHolidayLookup holidayLookup = Substitute.For<IHolidayLookup>();
            var date = new DateTime(2017, 11, 27);
            holidayLookup.IsPublicHoliday(date).Returns(true);
            var tollFeeCalculator = new TollFeeCalculator(holidayLookup);
            tollFeeCalculator.IsTollFreeDate(date).ShouldBeTrue();
        }

        [Fact]
        public void IsTollFreeDate_RegularDay_ReturnsFalse()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            tollFeeCalculator.IsTollFreeDate(new DateTime(2017, (int) Month.November, 27)).ShouldBeFalse();
        }

        [Fact]
        public void GetDailyTollFee_TollFreeVehicle_NoToll()
        {
            var tollFeeCalculator = new TollFeeCalculator(Substitute.For<IHolidayLookup>());
            tollFeeCalculator.GetDailyTollFee(new Diplomat(), new []{new DateTime(2017, 11, 27, 08, 25, 12)}).ShouldBe(0);
        }

        [Fact]
        public void GetDailyTollFee_PayingVehicle_PaysToll()
        {
            var tollFeeCalculator = new TollFeeCalculator(new SwedenHolidayLookupFor2017());
            tollFeeCalculator.GetDailyTollFee(new Car(), new []
            {
                new DateTime(2017, (int) Month.November, 1, 14, 05, 10), // 0 kr
                new DateTime(2017, (int) Month.November, 1, 16, 05, 10), // 18 kr 
                new DateTime(2017, (int) Month.November, 1, 18, 05, 10)  // 8 kr
            }).ShouldBe(26);

            tollFeeCalculator.GetDailyTollFee(new Car(), new[]
            {
                new DateTime(2017, (int) Month.November, 1, 06, 40, 40), // 13 kr
                new DateTime(2017, (int) Month.November, 1, 16, 05, 10), // 18 kr 
                new DateTime(2017, (int) Month.November, 1, 18, 05, 10)  // 8 kr
            }).ShouldBe(39);

            tollFeeCalculator.GetDailyTollFee(new Car(), new[]
            {
                new DateTime(2017, (int) Month.November, 1, 06, 40, 40), // 13 kr
                new DateTime(2017, (int) Month.November, 1, 16, 05, 10), // 18 kr 
                new DateTime(2017, (int) Month.November, 1, 18, 05, 10),  // 8 kr
                new DateTime(2017, (int) Month.November, 1, 18, 28, 10),  // 8 kr
                new DateTime(2017, (int) Month.November, 1, 18, 04, 10),  // 8 kr
                new DateTime(2017, (int) Month.November, 1, 06, 55, 40), // 13 kr
                new DateTime(2017, (int) Month.November, 1, 06, 50, 40) // 13 kr
            }).ShouldBe(39);

            // public holiday
            tollFeeCalculator.GetDailyTollFee(new Car(), new[]
            {
                new DateTime(2017, (int) Month.December, 31, 06, 40, 40), // 13 kr
                new DateTime(2017, (int) Month.December, 31, 16, 05, 10), // 18 kr 
                new DateTime(2017, (int) Month.December, 31, 18, 05, 10)  // 8 kr
            }).ShouldBe(0);

            // weekend
            tollFeeCalculator.GetDailyTollFee(new Car(), new[]
            {
                new DateTime(2017, (int) Month.December, 2, 06, 40, 40), // 13 kr
                new DateTime(2017, (int) Month.December, 2, 16, 05, 10), // 18 kr 
                new DateTime(2017, (int) Month.December, 2, 18, 05, 10)  // 8 kr
            }).ShouldBe(0);

            // max daily fee is 60 kr
            tollFeeCalculator.GetDailyTollFee(new Car(), new[]
            {
                new DateTime(2017, (int) Month.December, 1, 06, 40, 40), // 13 kr
                new DateTime(2017, (int) Month.December, 1, 07, 41, 40), // 18 kr
                new DateTime(2017, (int) Month.December, 1, 08, 45, 40), // 8 kr
                new DateTime(2017, (int) Month.December, 1, 16, 05, 10), // 18 kr 
                new DateTime(2017, (int) Month.December, 1, 18, 05, 10)  // 8 kr
            }).ShouldBe(60);
        }
    }
}
