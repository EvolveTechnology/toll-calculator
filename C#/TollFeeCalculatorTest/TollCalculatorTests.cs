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
        private readonly ITollTariff tariff = new TollTariff();

        public TollCalculatorTests()
        {
            calculator = new TollCalculator(tariff);
        }


        [Fact]
        public void NoFeeForTollFreeVehicle()
        {
            var dates = Enumerable.Range(0, 48).Select(i => new DateTime(2013, 04, 29, 0, 0, 0).AddHours(i * 0.5)).ToArray();
            var result = calculator.GetTollFee(motorbike, dates);
            Assert.Equal(0, result);
        }

        [Fact]
        public void MaxFeePerDayShouldBeCorrect()
        {
            var dates = Enumerable.Range(0, 48).Select(i => new DateTime(2013, 04, 29, 0, 0, 0).AddHours(i * 0.5)).ToArray();
            var result = calculator.GetTollFee(car, dates);
            Assert.Equal(tariff.MaxFeePerDay, result);
        }

        [Fact]
        public void TotalFeeFor2013ShouldEqual13times222()
        {
            var months = Enumerable.Range(1, 12);
            var days = months.SelectMany(month => Enumerable.Range(1, DateTime.DaysInMonth(2013, month)).Select(day => new DateTime(2013, month, day, 15, 0, 0)));
            var result = days.Sum(day => calculator.GetTollFee(car, new DateTime[]{day}));
            Assert.Equal(13 * 222, result);
        }

        [Fact]
        public void DriveToAndFromWorkShouldGenerateTwoFees()
        {
            var toWork = new DateTime(2013, 02, 01, 08, 28, 0);
            var fromWork = new DateTime(2013, 02, 01, 16, 58, 0);

            var expected = tariff.GetTollFee(toWork, car) + tariff.GetTollFee(fromWork, car);
            var result = calculator.GetTollFee(car, new DateTime[] { toWork, fromWork });

            Assert.Equal(expected, result);
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

            var expected = passages.Max(dt => tariff.GetTollFee(dt, car));
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
                passages1.Max(dt => tariff.GetTollFee(dt, car)) +
                passages2.Max(dt => tariff.GetTollFee(dt, car)) +
                passages3.Max(dt => tariff.GetTollFee(dt, car));

            var result = calculator.GetTollFee(car, passages1.Concat(passages2).Concat(passages3).ToArray());

            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void EdgeCaseOptimalTotalFeeShouldBe31()
        {
            var passages = new DateTime[] {
                new DateTime(2013,04,29,06,55,0), //13
                new DateTime(2013,04,29,07,00,0), //18
                new DateTime(2013,04,29,07,59,0), //18
            };
            var result = calculator.GetTollFee(car, passages);
            Assert.Equal(13 + 18, result);
        }

        [Fact]
        public void ShouldOnlyAcceptSameDatePassages()
        {
            var passages = new DateTime[] {
                new DateTime(2013,04,29,06,55,0), 
                new DateTime(2014,04,29,07,00,0), 
                new DateTime(2015,04,29,07,59,0), 
            };
            Assert.Throws<ArgumentException>(() => calculator.GetTollFee(car, passages));
        }
    }
}
