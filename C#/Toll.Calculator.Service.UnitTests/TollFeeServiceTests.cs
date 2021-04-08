using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Toll.Calculator.DAL.Repositories.Interface;
using Toll.Calculator.Domain;
using Toll.Calculator.UnitTests.Common;
using Xunit;

namespace Toll.Calculator.Service.UnitTests
{
    public class TollFeeServiceTests : UnitTestBase<TollFeeService>
    {
        public TollFeeServiceTests()
        {
            _vehicleRepository = Fixture.Freeze<IVehicleRepository>();
            _tollFeeRepository = Fixture.Freeze<ITollFeeRepository>();

            _vehicleRepository.GetTollFreeVehiclesAsync().Returns(new List<Vehicle>
            {
                Vehicle.Diplomat,
                Vehicle.Emergency,
                Vehicle.Foreign,
                Vehicle.Military,
                Vehicle.Motorbike,
                Vehicle.Tractor
            });
        }

        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITollFeeRepository _tollFeeRepository;

        [Fact]
        public async Task ForGetTotalFee_When2PassagesMoreThanAnHourApart_ReturnFeeForBothPassages()
        {
            var vehicleType = Vehicle.Car;
            var passage1 = new DateTime(1, 1, 1, 12, 50, 0);
            var passage2 = new DateTime(1, 1, 1, 14, 50, 0);
            var passageDates = new List<DateTime>
            {
                passage1,
                passage2
            };

            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage1)).Returns(8);
            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage2)).Returns(12);
            _tollFeeRepository.IsTollFreeDateAsync(Arg.Any<DateTime>()).Returns(false);

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().Be(20);
        }

        [Fact]
        public async Task ForGetTotalFee_When3PassagesAnd2LessThanAnHourApart_ReturnCorrectCombinedFee()
        {
            var vehicleType = Vehicle.Car;
            var passage1 = new DateTime(1, 1, 1, 12, 50, 0);
            var passage2 = new DateTime(1, 1, 1, 12, 55, 0);
            var passage3 = new DateTime(1, 1, 1, 15, 55, 0);
            var passageDates = new List<DateTime>
            {
                passage1,
                passage2,
                passage3
            };

            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage1)).Returns(8);
            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage2)).Returns(12);
            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage3)).Returns(18);
            _tollFeeRepository.IsTollFreeDateAsync(Arg.Any<DateTime>()).Returns(false);

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().Be(30);
        }

        [Fact]
        public async Task ForGetTotalFee_WhenOneDateIsTollFree_ReturnNoFeeForThatDay()
        {
            var vehicleType = Vehicle.Car;
            var passage1 = new DateTime(1, 1, 1, 12, 50, 0);
            var tollFreeDate = new DateTime(2, 2, 2, 12, 50, 0);
            var passageDates = new List<DateTime>
            {
                passage1,
                tollFreeDate
            };

            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage1)).Returns(8);
            _tollFeeRepository.IsTollFreeDateAsync(Arg.Is(passage1)).Returns(false);
            _tollFeeRepository.IsTollFreeDateAsync(Arg.Is(tollFreeDate)).Returns(true);

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().Be(8);
        }

        [Fact]
        public async Task ForGetTotalFee_WhenOneTimeDuringDay_ReturnFee()
        {
            var vehicleType = Vehicle.Car;
            var passage1 = new DateTime(1, 1, 1, 12, 50, 0);
            var passageDates = new List<DateTime>
            {
                passage1
            };

            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage1)).Returns(8);
            _tollFeeRepository.IsTollFreeDateAsync(Arg.Is(passage1)).Returns(false);

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().Be(8);
        }

        [Fact]
        public async Task ForGetTotalFee_WhenTotalDailyFeeExceedsMaximum_ReturnMaximumForThatDay()
        {
            var vehicleType = Vehicle.Car;
            var passage1Day1 = new DateTime(1, 1, 1, 12, 50, 0);
            var passage2Day1 = new DateTime(1, 1, 1, 16, 55, 0);
            var passage1Day2 = new DateTime(1, 1, 2, 15, 55, 0);
            var passageDates = new List<DateTime>
            {
                passage1Day1,
                passage2Day1,
                passage1Day2
            };

            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage1Day1)).Returns(50);
            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage2Day1)).Returns(40);
            _tollFeeRepository.GetPassageFeeByTimeAsync(Arg.Is(passage1Day2)).Returns(18);
            _tollFeeRepository.IsTollFreeDateAsync(Arg.Any<DateTime>()).Returns(false);

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().Be(78);
        }

        [Fact]
        public async Task ForGetTotalFee_WhenVehicleIsTollFree_ReturnNoFee()
        {
            var vehicleType = Vehicle.Emergency;
            var passageDates = Fixture.Create<List<DateTime>>();

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().Be(0);
        }
    }
}