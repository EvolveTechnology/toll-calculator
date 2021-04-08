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
using Toll.Calculator.Service.Interface;
using Toll.Calculator.UnitTests.Common;
using Toll.Calculator.WebAPI.ApiModels;
using Toll.Calculator.WebAPI.Controllers;
using Xunit;

namespace Toll.Calculator.WebAPI.UnitTests
{
    public class FeeCalculatorControllerTests : ControllerUnitTest<FeeCalculatorController>
    {
        public FeeCalculatorControllerTests()
        {
            _tollFeeService = Fixture.Freeze<ITollFeeService>();
        }

        private readonly ITollFeeService _tollFeeService;

        [Fact]
        public async Task WhenServiceRunsNormal_ThenReturnOkResult()
        {
            var requestModel = new TotalFeeRequestModel()
            {
                VehicleType = Fixture.Create<TotalFeeRequestModel.Vehicle>(),
                PassageDates = new[] { new DateTime(2021, 04, 06, 17, 53, 0) }
            };
            _tollFeeService.GetTotalFee(Arg.Any<Vehicle>(), Arg.Any<List<DateTime>>()).Returns(1);

            var response = await SUT.GetTotalFee(requestModel);

            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task WhenServiceThrows_ThenReturnServerError()
        {
            var requestModel = new TotalFeeRequestModel()
            {
                VehicleType = Fixture.Create<TotalFeeRequestModel.Vehicle>(),
                PassageDates = new[] { new DateTime(2021, 04, 06, 17, 53, 0) }
            };
            _tollFeeService.GetTotalFee(Arg.Any<Vehicle>(), Arg.Any<List<DateTime>>()).Throws<Exception>();

            var response = await SUT.GetTotalFee(requestModel);

            response.Should().BeOfType<ObjectResult>();
        }
    }
}