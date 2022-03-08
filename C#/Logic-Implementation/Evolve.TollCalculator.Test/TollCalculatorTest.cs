using Evolve.TollCalculator.Models;
using System;
using Xunit;

namespace Evolve.TollCalculator.Test
{
    public class TollCalculatorTest
    {
        #region Constants

        const int NoToll = 0;
        const int MinimumToll = 8;
        const int AverageToll = 13;
        const int MaximumToll = 18;

        #endregion 

        private readonly TollCalculator calculator = new TollCalculator();

        #region "Toll Free Vehicle Tests"

        [Fact]
        public void ShouldReturnNoTollOnMotoBikeAsync()
        {
            DateTime[] tollDates = new DateTime[]
            {
                new DateTime(2022, 3, 1, 8, 0, 0),
                new DateTime(2022, 3, 1, 8, 29, 0)
            };

            var result = calculator.GetTollFeeByDate(new MotorBike(), tollDates);
            Assert.Equal(NoToll, result);
        }

        [Fact]
        public void ShouldReturnNoTollOnTractorAsync()
        {
            DateTime[] tollDates = new DateTime[]
            {
                new DateTime(2022, 3, 5, 13, 45, 0)
            };

            var result = calculator.GetTollFeeByDate(new Tractor(), tollDates);
            Assert.Equal(NoToll, result);
        }

        [Fact]
        public void ShouldReturnNoTollOnEmergencyAsync()
        {
            DateTime[] tollDates = new DateTime[]
            {
                new DateTime(2022, 3, 5, 13, 45, 0)
            };

            var result = calculator.GetTollFeeByDate(new Emergency(), tollDates);
            Assert.Equal(NoToll, result);
        }

        [Fact]
        public void ShouldReturnNoTollOnDiplomatAsync()
        {
            DateTime[] tollDates = new DateTime[]
            {
                new DateTime(2022, 3, 5, 13, 45, 0)
            };

            var result = calculator.GetTollFeeByDate(new Diplomat(), tollDates);
            Assert.Equal(NoToll, result);
        }

        [Fact]
        public void ShouldReturnNoTollOnForeignAsync()
        {
            DateTime[] tollDates = new DateTime[]
            {
                new DateTime(2022, 3, 5, 13, 45, 0)
            };

            var result = calculator.GetTollFeeByDate(new Foreign(), tollDates);
            Assert.Equal(NoToll, result);
        }

        [Fact]
        public void ShouldReturnNoTollOnMilitaryAsync()
        {
            DateTime[] tollDates = new DateTime[]
            {
                new DateTime(2022, 3, 5, 13, 45, 0)
            };

            var result = calculator.GetTollFeeByDate(new Military(), tollDates);
            Assert.Equal(NoToll, result);
        }

        [Fact]
        public void ShouldNotReturnTollAmountOnCarAsync()
        {
            DateTime[] tollDates = new DateTime[]
            {
                new DateTime(2022, 3, 8, 13, 45, 0)
            };

            var result = calculator.GetTollFeeByDate(new Car(), tollDates);
            Assert.NotEqual(0, result);
        }

        #endregion

        #region "Toll Calculation"

        [Fact]
        public void ShouldReturnTollThirtyMinsForSixOnCarAsync()
        {
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 1, 6, 0, 0),
                new DateTime(2022, 3, 1, 6, 29, 0),
             };

            var result = calculator.GetTollFeeByDate(new Car(), dateTimes);
            Assert.Equal(8, result);
        }

        [Fact]
        public void ShouldReturnTollOneHourForSixOnCarAsync()
        {
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 1, 6, 0, 0),
                new DateTime(2022, 3, 1, 6, 59, 0),
             };
   
            var result = calculator.GetTollFeeByDate(new Car(), dateTimes);
            Assert.Equal(13, result);
        }

        [Fact]
        public void ShouldReturnTollOneHourForSevenOnCarAsync()
        {
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 8, 7, 0, 0),
                new DateTime(2022, 3, 8, 7, 45, 0),
             };

            var result = calculator.GetTollFeeByDate(new Car(), dateTimes);
            Assert.Equal(18, result);
        }

        [Fact]
        public void ShouldReturnTollThirtyMinsForEightOnCarAsync()
        {
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 8, 8, 0, 0),
                new DateTime(2022, 3, 8, 8, 26, 0),
             };
   
            var result = calculator.GetTollFeeByDate(new Car(), dateTimes);
            Assert.Equal(13, result);
        }

        [Fact]
        public void ShouldReturnTollOneHourForEightToTwoOnCarAsync()
        {
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 8, 9, 15, 0),
                new DateTime(2022, 3, 8, 9, 26, 0),
             };

            var result = calculator.GetTollFeeByDate(new Car(), dateTimes);
            Assert.Equal(8, result);
        }

        #endregion

        [Fact]
        public void ShouldReturnZeroOnEmptyDatesArrayAsync()
        {
            var response = calculator.GetTollFeeByDate(new MotorBike(), Array.Empty<DateTime>());
            Assert.Equal(0, response);
        }

        [Fact]
        public void ShouldReturnZeroOnWeekendsAsync()
        {
            var saturday = new DateTime(2020, 9, 5, 15, 0, 0);
            var sunday = new DateTime(2020, 9, 6, 15, 0, 0);
            var dates = new DateTime[] { saturday, sunday };

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnZeroOnHolidaysAsync()
        {
            var christmasEve = new DateTime(2013, 12, 24);
            var christmasDay = new DateTime(2013, 12, 25);
            var dates = new DateTime[] { christmasEve, christmasDay };

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnZeroOffFeeHoursAsync()
        {
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 5, 30,0 ),
                new DateTime(2020, 9, 1, 19, 0, 0)
            };

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(0, tollFee);
        }

        [Fact]
        public void ShouldReturnMaxOnPeakHoursAsync()
        {
            var expectedTotalFee = 2 * MaximumToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 7, 30,0 ),
                new DateTime(2020, 9, 1, 16, 0, 0)
            };

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnMediumFeeOnModerateHoursAsync()
        {
            var expectedTotalFee = 4 * AverageToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 40,0 ),
                new DateTime(2020, 9, 1, 8, 15, 0),
                new DateTime(2020, 9, 1, 15, 15, 0),
                new DateTime(2020, 9, 1, 17, 30, 0)};

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnMinFeeInOffPeakHoursAsync()
        {
            var expectedTotalFee = 2 * MinimumToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 15, 0),
                new DateTime(2020, 9, 1, 18, 15, 0)
            };

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnOnlyTheHighestFeeWithinEveryHourAsync()
        {
            var expectedTotalFee = MaximumToll + MaximumToll + MinimumToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 45, 0),  //13
                new DateTime(2020, 9, 1, 7, 15, 0),   //18
                new DateTime(2020, 9, 1, 16, 59, 0),  //18
                new DateTime(2020, 9, 1, 17, 0, 0),   //13
                new DateTime(2020, 9, 1, 18, 29, 0)   //8
            };

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnOnlyHighestFeeEveryHourWhenDatesAreUnsortedAsync()
        {
            var expectedTotalFee = MaximumToll + MaximumToll + MinimumToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 17, 0, 0),
                new DateTime(2020, 9, 1, 7, 15, 0),
                new DateTime(2020, 9, 1, 6, 45, 0 ),
                new DateTime(2020, 9, 1, 18, 29, 0),
                new DateTime(2020, 9, 1, 16, 59, 0)
            };

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(expectedTotalFee, tollFee);
        }

        [Fact]
        public void ShouldReturnMaximumTollFeeWhenOverLimitAsync()
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

            var tollFee = calculator.GetTollFeeByDate(new Car(), dates);
            Assert.Equal(maximumTollFee, tollFee);
        }

    }
}
