using System;
using Xunit;
using TollFeeCalculator;
using System.Linq;

namespace TollFeeCalculatorTest
{
    public class TollCalculatorTests
    {
        private readonly TollCalculator calculator;
        private readonly Vehicle car = new Car();
        private readonly Vehicle motorbike = new Motorbike();

        public TollCalculatorTests()
        {
            calculator = new TollCalculator(new TollTariff());

        }

        [Theory]
        [InlineData(05,59,0)]
        [InlineData(06,0,8)]
        [InlineData(06,29,8)]
        [InlineData(06,30,13)]
        [InlineData(06,59,13)]
        [InlineData(07,0,18)]
        [InlineData(07,59,18)]
        [InlineData(08,0,13)]
        [InlineData(08,29,13)]
        [InlineData(08,30,8)]
        [InlineData(14,59,8)]
        [InlineData(15,0,13)]
        [InlineData(15,29,13)]
        [InlineData(15,30,18)]
        [InlineData(16,59,18)]
        [InlineData(17,0,13)]
        [InlineData(17,59,13)]
        [InlineData(18,0,8)]
        [InlineData(18,29,8)]
        [InlineData(18,30,0)]
        public void TollFeeTarifTest(int hour, int minute, int expectedFee)
        {
            var date = new DateTime(2013,04,29,hour,minute,0);
            var result = calculator.GetTollFee(date, car);
            Assert.Equal(expectedFee, result);     
        }

        [Fact]
        public void NoFeeForTollFreeVehicle()
        {
            var dates = Enumerable.Range(0,48).Select(i => new DateTime(2013,04,29,0,0,0).AddHours(i*0.5)).ToArray();
            var result = calculator.GetTollFee(motorbike, dates);
            Assert.Equal(0, result);
        }

        [Fact]
        public void MaxFeePerDayShouldEqual60()
        {
            var dates = Enumerable.Range(0,48).Select(i => new DateTime(2013,04,29,0,0,0).AddHours(i*0.5)).ToArray();
            var result = calculator.GetTollFee(car, dates);
            Assert.Equal(60, result);
        }

        [Fact]
        public void MaxFeeFor2013ShouldEqual60times222() //250?
        {
            var months = Enumerable.Range(1,12);
            var days = months.SelectMany(month => Enumerable.Range(1, DateTime.DaysInMonth(2013,month)).Select(day => new DateTime(2013,month, day,0,0,0)));
            var result = days.Sum(day => calculator.GetTollFee(car, Enumerable.Range(0,48).Select(i => day.AddHours(0.5*i)).ToArray()));
            Assert.Equal(60*222, result);
        }

        [Fact]
        public void DriveToAndFromWorkShouldGenerateTwoFees()
        {
            var toWork = new DateTime(2013,02,01,08,28,0);
            var fromWork = new DateTime(2013,02,01,16,58,0);

            var expected = calculator.GetTollFee(toWork,car) + calculator.GetTollFee(fromWork,car);
            var result = calculator.GetTollFee(car, new DateTime[] {toWork, fromWork});

            Assert.Equal(expected,result);

        }

        [Fact]
        public void OnlyOneAndHighestFeePerHour()
        {
            var passages = new DateTime[] {
                new DateTime(2013,04,29,14,59,0),
                new DateTime(2013,04,29,15,00,0),
                new DateTime(2013,04,29,15,29,0),
                new DateTime(2013,04,29,15,30,0),
            };

            var expected = passages.Max(dt => calculator.GetTollFee(dt, car));
            var result = calculator.GetTollFee(car, passages);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void OnlyOneAndHighestFeePerHourThreeIntervals()
        {
            var passages1 = new DateTime[] {
                new DateTime(2013,04,29,07,59,0),
                new DateTime(2013,04,29,08,00,0),
                new DateTime(2013,04,29,08,30,0),
            };

            var passages2 = new DateTime[] {
                new DateTime(2013,04,29,14,59,0),
                new DateTime(2013,04,29,15,00,0),
                new DateTime(2013,04,29,15,30,0),
            };

            var passages3 = new DateTime[] {
                new DateTime(2013,04,29,17,59,0),
                new DateTime(2013,04,29,18,00,0),
                new DateTime(2013,04,29,18,30,0),
            };

            var expected =
                passages1.Max(dt => calculator.GetTollFee(dt, car)) +
                passages2.Max(dt => calculator.GetTollFee(dt, car)) +
                passages3.Max(dt => calculator.GetTollFee(dt, car));

            var result = calculator.GetTollFee(car, passages1.Concat(passages2).Concat(passages3).ToArray());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void May13_2021_ShouldBeTollFreeDate()
        {
            var passage = new DateTime(2021,05,13,08,0,0);
            var result = calculator.GetTollFee(passage, car);
            Assert.Equal(0, result);
        }
    }
}
