using System;
using Xunit;
using TollFeeCalculator;

namespace TollFeeCalculatorTest
{
    public class TollTariffTests
    {
        private readonly TollTariff tariff = new();
        private readonly IVehicle car = new Car();

        [Theory]
        [InlineData(05, 59, 0)]
        [InlineData(06, 0, 8)]
        [InlineData(06, 29, 8)]
        [InlineData(06, 30, 13)]
        [InlineData(06, 59, 13)]
        [InlineData(07, 0, 18)]
        [InlineData(07, 59, 18)]
        [InlineData(08, 0, 13)]
        [InlineData(08, 29, 13)]
        [InlineData(08, 30, 8)]
        [InlineData(14, 59, 8)]
        [InlineData(15, 0, 13)]
        [InlineData(15, 29, 13)]
        [InlineData(15, 30, 18)]
        [InlineData(16, 59, 18)]
        [InlineData(17, 0, 13)]
        [InlineData(17, 59, 13)]
        [InlineData(18, 0, 8)]
        [InlineData(18, 29, 8)]
        [InlineData(18, 30, 0)]
        public void TollFeeTarifTest(int hour, int minute, int expectedFee)
        {
            var date = new DateTime(2013, 04, 29, hour, minute, 0);
            var result = tariff.GetTollFee(date, car);
            Assert.Equal(expectedFee, result);
        }

        [Fact]
        public void May13_2021_ShouldBeTollFreeDate()
        {
            var passage = new DateTime(2021, 05, 13, 08, 0, 0);
            var result = tariff.GetTollFee(passage, car);
            Assert.Equal(0, result);
        }
    }
}
