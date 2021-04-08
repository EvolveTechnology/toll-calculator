using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using Toll.Calculator.Domain;
using Toll.Calculator.UnitTests.Common;
using Toll.Calculator.WebAPI.ApiModels;
using Xunit;

namespace Toll.Calculator.WebAPI.UnitTests
{
    public class TotalFeeRequestModelTests : UnitTestBase<TotalFeeRequestModel>
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ForVehicleTypeToDomain_WhenCalledForAllTypes_DoesNotThrow(
            Vehicle domainVehicle, 
            TotalFeeRequestModel.Vehicle requestVehicle)
        {
            var requestModel = new TotalFeeRequestModel
            {
                PassageDates = new DateTime[0],
                VehicleType = requestVehicle
            };

            var result = requestModel.VehicleTypeToDomain();

            result.Should().Be(domainVehicle);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>();

            var allDomainVehicles = Enum.GetValues(typeof(Vehicle));
            var allRequestVehicles = Enum.GetValues(typeof(TotalFeeRequestModel.Vehicle));

            if (allDomainVehicles.Length != allRequestVehicles.Length)
            {
                throw new AssertionFailedException("Domain Enum and RequestModel Enum count does not match");
            }

            foreach (Vehicle domainVehicle in allDomainVehicles)
            {
                var obj = new object[]
                {
                    domainVehicle,
                    Array.Find((TotalFeeRequestModel.Vehicle[])allRequestVehicles, r => (int) r == (int) domainVehicle)
                };

                allData.Add(obj);
            }

            return allData;
        }
    }
}