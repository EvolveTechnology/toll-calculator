using Evolve.TollCalculator.Application.Commands;
using Evolve.TollCalculator.Application.Handlers.CommandHandlers;
using Evolve.TollCalculator.Core.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Evolve.TollCalculation.Application.UnitTest
{
    public class ApplicationUnitTests
    {
        const int NoToll = 0;
        const int LowToll = 8;
        const int MidToll = 13;
        const int HighToll = 18;

        private readonly TollCalculationByDateRangeHandler rangeHandler = new TollCalculationByDateRangeHandler();
        private readonly TollCalculationByDateHandler handler = new TollCalculationByDateHandler();

        #region "Toll Free Vehicle Tests"

        [Fact]
        public async Task ShouldReturnNoTollOnMotoBikeAsync()
        {
            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                Vehicle = new MotorBike(),
                TollDate = new DateTime[]
                {
                    new DateTime(2022, 3, 1, 8, 0, 0),
                    new DateTime(2022, 3, 1, 8, 29, 0)
                }
            };

            var result = await rangeHandler.Handle(command);
            Assert.Equal(NoToll, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnNoTollOnTractorAsync()
        {
            TollCalculationByDateCommand command = new TollCalculationByDateCommand()
            {
                TollDate = new DateTime(2022, 3, 5, 13, 45, 0),
                Vehicle = new Tractor()
            };

            var result = await handler.Handle(command);
            Assert.Equal(NoToll, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnNoTollOnEmergencyAsync()
        {
            TollCalculationByDateCommand command = new TollCalculationByDateCommand()
            {
                TollDate = new DateTime(2022, 3, 5, 13, 45, 0),
                Vehicle = new Emergency()
            };

            var result = await handler.Handle(command);
            Assert.Equal(NoToll, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnNoTollOnDiplomatAsync()
        {
            TollCalculationByDateCommand command = new TollCalculationByDateCommand()
            {
                TollDate = new DateTime(2022, 3, 5, 13, 45, 0),
                Vehicle = new Diplomat()
            };

            var result = await handler.Handle(command);
            Assert.Equal(NoToll, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnNoTollOnForeignAsync()
        {
            TollCalculationByDateCommand command = new TollCalculationByDateCommand()
            {
                TollDate = new DateTime(2022, 3, 5, 13, 45, 0),
                Vehicle = new Foreign()
            };

            var result = await handler.Handle(command);
            Assert.Equal(NoToll, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnNoTollOnMilitaryAsync()
        {
            TollCalculationByDateCommand command = new TollCalculationByDateCommand()
            {
                TollDate = new DateTime(2022, 3, 5, 13, 45, 0),
                Vehicle = new Military()
            };

            var result = await handler.Handle(command);
            Assert.Equal(NoToll, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnTollAmountOnCarAsync()
        {
            TollCalculationByDateCommand command = new TollCalculationByDateCommand()
            {
                TollDate = new DateTime(2022, 3, 8, 13, 45, 0),
                Vehicle = new Car()
            };

            var result = await handler.Handle(command);
            Assert.NotEqual(0, result.TollAmount);
        }

        #endregion

        #region "Toll Calculation"

        [Fact]
        public async Task ShouldReturnTollThirtyMinsForSixOnCarAsync()
        {
            TollCalculationByDateRangeHandler handler = new TollCalculationByDateRangeHandler();
            Car car = new Car();
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 1, 6, 0, 0),
                new DateTime(2022, 3, 1, 6, 29, 0),
             };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dateTimes,
                Vehicle = car
            };

            var result = await handler.Handle(command);

            Assert.Equal(8, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnTollOneHourForSixOnCarAsync()
        {
            TollCalculationByDateRangeHandler handler = new TollCalculationByDateRangeHandler();
            Car car = new Car();
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 1, 6, 0, 0),
                new DateTime(2022, 3, 1, 6, 59, 0),
             };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dateTimes,
                Vehicle = car
            };

            var result = await handler.Handle(command);

            Assert.Equal(13, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnTollOneHourForSevenOnCarAsync()
        {
            TollCalculationByDateRangeHandler handler = new TollCalculationByDateRangeHandler();
            Car car = new Car();
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 8, 7, 0, 0),
                new DateTime(2022, 3, 8, 7, 45, 0),
             };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dateTimes,
                Vehicle = car
            };

            var result = await handler.Handle(command);

            Assert.Equal(18, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnTollThirtyMinsForEightOnCarAsync()
        {
            TollCalculationByDateRangeHandler handler = new TollCalculationByDateRangeHandler();
            Car car = new Car();
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 8, 8, 0, 0),
                new DateTime(2022, 3, 8, 8, 26, 0),
             };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dateTimes,
                Vehicle = car
            };

            var result = await handler.Handle(command);

            Assert.Equal(13, result.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnTollOneHourForEightToTwoOnCarAsync()
        {
            TollCalculationByDateRangeHandler handler = new TollCalculationByDateRangeHandler();
            Car car = new Car();
            DateTime[] dateTimes = new DateTime[]
             {
                new DateTime(2022, 3, 8, 9, 15, 0),
                new DateTime(2022, 3, 8, 9, 26, 0),
             };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dateTimes,
                Vehicle = car
            };

            var result = await handler.Handle(command);

            Assert.Equal(8, result.TollAmount);
        }

        #endregion

        [Fact]
        public async Task ShouldReturnZeroOnEmptyDatesArrayAsync()
        {
            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = Array.Empty<DateTime>(),
                Vehicle = new MotorBike()
            };

            var response = await rangeHandler.Handle(command);
            Assert.Equal(0, response.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnZeroOnWeekendsAsync()
        {
            var saturday = new DateTime(2020, 9, 5, 15, 0, 0);
            var sunday = new DateTime(2020, 9, 6, 15, 0, 0);
            var dates = new DateTime[] { saturday, sunday };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(0, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnZeroOnHolidaysAsync()
        {
            var christmasEve = new DateTime(2013, 12, 24);
            var christmasDay = new DateTime(2013, 12, 25);
            var dates = new DateTime[] { christmasEve, christmasDay };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(0, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnZeroOffFeeHoursAsync()
        {
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 5, 30,0 ),
                new DateTime(2020, 9, 1, 19, 0, 0)
            };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(0, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnMaxOnPeakHoursAsync()
        {
            var expectedTotalFee = 2 * HighToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 7, 30,0 ),
                new DateTime(2020, 9, 1, 16, 0, 0)
            };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(expectedTotalFee, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnMediumFeeOnModerateHoursAsync()
        {
            var expectedTotalFee = 4 * MidToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 40,0 ),
                new DateTime(2020, 9, 1, 8, 15, 0),
                new DateTime(2020, 9, 1, 15, 15, 0),
                new DateTime(2020, 9, 1, 17, 30, 0)};

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(expectedTotalFee, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnMinFeeInOffPeakHoursAsync()
        {
            var expectedTotalFee = 2 * LowToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 15, 0),
                new DateTime(2020, 9, 1, 18, 15, 0)
            };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(expectedTotalFee, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnOnlyTheHighestFeeWithinEveryHourAsync()
        {
            var expectedTotalFee = HighToll + HighToll + LowToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 6, 45, 0),  //13
                new DateTime(2020, 9, 1, 7, 15, 0),   //18
                new DateTime(2020, 9, 1, 16, 59, 0),  //18
                new DateTime(2020, 9, 1, 17, 0, 0),   //13
                new DateTime(2020, 9, 1, 18, 29, 0)   //8
            };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(expectedTotalFee, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnOnlyHighestFeeEveryHourWhenDatesAreUnsortedAsync()
        {
            var expectedTotalFee = HighToll + HighToll + LowToll;
            var dates = new DateTime[] {
                new DateTime(2020, 9, 1, 17, 0, 0),
                new DateTime(2020, 9, 1, 7, 15, 0),
                new DateTime(2020, 9, 1, 6, 45, 0 ),
                new DateTime(2020, 9, 1, 18, 29, 0),
                new DateTime(2020, 9, 1, 16, 59, 0)
            };

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(expectedTotalFee, tollFee.TollAmount);
        }

        [Fact]
        public async Task ShouldReturnMaximumTollFeeWhenOverLimitAsync()
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

            TollCalculationByDateRangeCommand command = new TollCalculationByDateRangeCommand()
            {
                TollDate = dates,
                Vehicle = new Car()
            };

            var tollFee = await rangeHandler.Handle(command);
            Assert.Equal(maximumTollFee, tollFee.TollAmount);
        }
    }
}
