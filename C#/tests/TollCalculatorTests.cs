using System;
using TollFeeCalculator;
using Xunit;

namespace tests
{
    public class TollCalculatorTests
    {
        private readonly TollCalculator tollCalculator = new TollCalculator();

        [Fact]
        public void ShouldThrowExceptionOnEmptyDates()
        {
            var motorbike = new Motorbike();

            Assert.Throws<IndexOutOfRangeException>(() => tollCalculator.GetTollFee(motorbike, new DateTime[0]));
        }

        [Fact]
        public void ShouldReturnNoFeeOnTollFreeVehicles()
        {
            var dates = new DateTime[] { new DateTime(2020, 1, 1, 6, 35, 0)};

            Assert.Equal(0, tollCalculator.GetTollFee(new Motorbike(), dates));
            Assert.Equal(0, tollCalculator.GetTollFee(new Tractor(), dates));
            Assert.Equal(0, tollCalculator.GetTollFee(new Emergency(), dates));
            Assert.Equal(0, tollCalculator.GetTollFee(new Diplomat(), dates));
            Assert.Equal(0, tollCalculator.GetTollFee(new Foreign(), dates));
            Assert.Equal(0, tollCalculator.GetTollFee(new Military(), dates));
        }

        [Fact]
        public void ShouldReturnNoFeeOnWeekends()
        {
            var saturday = new DateTime(2020, 9, 5);
            var sunday = new DateTime(2020, 9, 6);
            var dates = new DateTime[] { saturday, sunday };

            var tollFee = tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnNoFeeOnHolidays()
        {
            var christmasEve = new DateTime(2013, 12, 24);
            var christmasDay = new DateTime(2013, 12, 25);
            var dates = new DateTime[] { christmasEve, christmasDay };

            var tollFee = tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnNoFeeOutsideFeeHours()
        {
            var dates = new DateTime[] { new DateTime(2020, 9, 1, 5, 30,0 ), new DateTime(2020, 9, 1, 19, 0, 0) };

            var tollFee = tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnHighestFeeInRushHour()
        {
            var highestFee = 18;
            var expectedTotalFee = 2 * highestFee;
            var dates = new DateTime[] { new DateTime(2020, 9, 1, 7, 30,0 ), new DateTime(2020, 9, 1, 16, 0, 0) };

            var tollFee = tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnMidFeeInModerateTraffic()
        {
            var midFee = 13;
            var expectedTotalFee = 4 * midFee;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 40,0 ),
                new DateTime(2020, 9, 1, 8, 15, 0),
                new DateTime(2020, 9, 1, 15, 15, 0),
                new DateTime(2020, 9, 1, 17, 30, 0)};

            var tollFee = tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnLowestFeeInLowTraffic()
        {
            var lowFee = 8;
            var expectedTotalFee = 2 * lowFee;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 15, 0 ),
                new DateTime(2020, 9, 1, 18, 15, 0)};

            var tollFee = tollCalculator.GetTollFee(new Car(), dates);

            Assert.Equal(expectedTotalFee, tollFee);
        }
    }
}