using System;
using TollFeeCalculator;
using TollFeeCalculator.Vehicles;
using Xunit;

namespace tests
{
    public class TollCalculatorTests
    {
        const int HighTollFee = 18;
        const int MidTollFee = 13;
        const int LowTollFee = 8;
        private readonly TollCalculator _tollCalculator = new TollCalculator(new TollFee());

        [Fact]
        public void ShouldReturnNoFeeOnEmptyDates()
        {
            var motorbike = new Motorbike();

            var tollFee = _tollCalculator.GetTollFee(motorbike, new DateTime[0]);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnNoFeeOnTollFreeVehicles()
        {
            var dates = new DateTime[] { new DateTime(2020, 1, 1, 6, 35, 0)};

            Assert.Equal(0, _tollCalculator.GetTollFee(new Motorbike(), dates));
            Assert.Equal(0, _tollCalculator.GetTollFee(new Tractor(), dates));
            Assert.Equal(0, _tollCalculator.GetTollFee(new Emergency(), dates));
            Assert.Equal(0, _tollCalculator.GetTollFee(new Diplomat(), dates));
            Assert.Equal(0, _tollCalculator.GetTollFee(new Foreign(), dates));
            Assert.Equal(0, _tollCalculator.GetTollFee(new Military(), dates));
        }

        [Fact]
        public void ShouldReturnNoFeeOnWeekends()
        {
            var saturday = new DateTime(2020, 9, 5, 15, 0, 0);
            var sunday = new DateTime(2020, 9, 6, 15, 0, 0);
            var dates = new DateTime[] { saturday, sunday };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnNoFeeOnHolidays()
        {
            var christmasEve = new DateTime(2013, 12, 24);
            var christmasDay = new DateTime(2013, 12, 25);
            var dates = new DateTime[] { christmasEve, christmasDay };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnNoFeeOutsideFeeHours()
        {
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 5, 30,0 ),
                new DateTime(2020, 9, 1, 19, 0, 0)
            };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnHighestFeeInRushHour()
        {
            var expectedTotalFee = 2 * HighTollFee;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 7, 30,0 ),
                new DateTime(2020, 9, 1, 16, 0, 0)
            };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnMidFeeInModerateTraffic()
        {
            var expectedTotalFee = 4 * MidTollFee;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 40,0 ),
                new DateTime(2020, 9, 1, 8, 15, 0),
                new DateTime(2020, 9, 1, 15, 15, 0),
                new DateTime(2020, 9, 1, 17, 30, 0)};

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnLowestFeeInLowTraffic()
        {
            var expectedTotalFee = 2 * LowTollFee;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 15, 0 ),
                new DateTime(2020, 9, 1, 18, 15, 0)
            };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnOnlyTheHighestFeeWithinEveryHour()
        {
            var expectedTotalFee = HighTollFee + HighTollFee + LowTollFee;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 45, 0 ),
                new DateTime(2020, 9, 1, 7, 15, 0),
                new DateTime(2020, 9, 1, 16, 59, 0),
                new DateTime(2020, 9, 1, 17, 0, 0),
                new DateTime(2020, 9, 1, 18, 29, 0)
            };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnOnlyTheHighestFeeWithinEveryHourWhenDatesAreUnsorted()
        {
            var expectedTotalFee = HighTollFee + HighTollFee + LowTollFee;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 17, 0, 0),
                new DateTime(2020, 9, 1, 7, 15, 0),
                new DateTime(2020, 9, 1, 6, 45, 0 ),
                new DateTime(2020, 9, 1, 18, 29, 0),
                new DateTime(2020, 9, 1, 16, 59, 0)
            };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnMaximumTollFeeWhenOverLimit()
        {
            var maximumTollFee = 60;

            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 0, 0 ),
                new DateTime(2020, 9, 1, 7, 15, 0),
                new DateTime(2020, 9, 1, 8, 29, 0),
                new DateTime(2020, 9, 1, 15, 0, 0),
                new DateTime(2020, 9, 1, 16, 30, 0),
                new DateTime(2020, 9, 1, 18, 15, 0)
            };

            var tollFee = _tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(maximumTollFee, tollFee);
        }
    }
}