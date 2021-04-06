using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Toll.Calculator.Domain;
using Toll.Calculator.Service;
using Toll.Calculator.UnitTests.Common;
using Toll.Calculator.WebAPI.Controllers;
using Xunit;

namespace Toll.Calculator.WebAPI.UnitTests
{
    public class FeeCalculatorControllerTests : ControllerUnitTest<FeeCalculatorController>
    {
        private readonly ITollFeeService _tollFeeService;

        public FeeCalculatorControllerTests()
        {
            _tollFeeService = Fixture.Freeze<ITollFeeService>();
        }

        [Fact]
        public async Task WhenPassageDatesAreInvalid_ThenReturnBadRequest()
        {
            var vehicleType = Fixture.Create<Vehicle>();
            var passageDates = Fixture.Create<string>();

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task WhenServiceThrows_ThenReturnServerError()
        {
            var vehicleType = Fixture.Create<Vehicle>();
            var passageDates = "2021-04-06T17:53:00.146Z";
            _tollFeeService.GetTotalFee(Arg.Any<Vehicle>(), Arg.Any<List<DateTime>>()).Throws<Exception>();

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().BeOfType<ObjectResult>();
        }

        [Fact]
        public async Task WhenServiceRunsNormal_ThenReturnOkResult()
        {
            var vehicleType = Fixture.Create<Vehicle>();
            var passageDates = "2021-04-06T17:53:00.146Z";
            _tollFeeService.GetTotalFee(Arg.Any<Vehicle>(), Arg.Any<List<DateTime>>()).Returns(1);

            var response = await SUT.GetTotalFee(vehicleType, passageDates);

            response.Should().BeOfType<OkObjectResult>();
        }
    }
}
