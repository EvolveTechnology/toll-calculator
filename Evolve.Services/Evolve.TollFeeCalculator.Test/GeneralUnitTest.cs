using Evolve.TollFeeCalculator.Config;
using Evolve.TollFeeCalculator.Interfaces;
using Evolve.TollFeeCalculator.Models;
using Evolve.TollFeeCalculator.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;

namespace Evolve.TollFeeCalculator.Test
{

    public class GeneralUnitTest
    {
        private IConfigurationRoot _configuration { get; set; }
        private IContainer _container { get; set; }
        private ITollFeeCalculatorService _tollFreeForVehicle { get; set; }
        private IAppConfiguration _app { get; set; }

        public GeneralUnitTest()
        {
            _configuration = new ConfigurationBuilder()
             .AddJsonFile("client-secrets.json")
              .Build();
           // IAppConfiguration _appConfiguration = new AppConfiguration(_configuration);

            _container = ContainerConfig.Configure(_configuration);

            using (var scope = _container.BeginLifetimeScope())
            {
                _app = scope.Resolve<IAppConfiguration>();
                _tollFreeForVehicle = scope.Resolve<ITollFeeCalculatorService>();

            }
        }



        [Fact(DisplayName = "Test Configuration")]
        public void Test_Configuration()
        {
            Assert.Equal(1, _app.CostParameters.ExtraCostFactor);
        }


        [Fact(DisplayName = "Get Toll fee for Vehicle as Car and for one day")]
        [Trait("Vehicle", "Car")]
        public async Task Test_GetTollFeeCalculatorforCar()
        {

            var vehicleAndDateRequest = new VehicleAndDateRequest
            {
                Vehicle = new Car(),
                TollDates = new List<DateTime>{
                                              new DateTime(2019, 05, 8, 10, 30, 0),
                                              new DateTime(2019, 05, 9, 10, 30, 0),
                                              new DateTime(2019, 05, 9, 10, 56, 0)}
            };

            var CostTollFee = await _tollFreeForVehicle.GetTotalTollFeeForDateAsync(vehicleAndDateRequest);
            Assert.Equal(24, CostTollFee);
        }


        [Fact(DisplayName = "Get Toll fee for Vehicle as Motorbike and for one day")]
        [Trait("Vehicle", "Motorbike")]
        public async Task Test_GetTollFeeCalculatorforMotorbike()
        {

            var vehicleAndDateRequest2 = new VehicleAndDateRequest
            {
                Vehicle = new Motorbike(),
                TollDates = new List<DateTime>{
                                              new DateTime(2019, 05, 9, 10, 30, 01,01),
                                              new DateTime(2019, 05, 9, 11, 36, 20,10),
                                              new DateTime(2019, 05, 9, 12, 56, 0,20)}
            };

            var CostTollFee = await _tollFreeForVehicle.GetTotalTollFeeForDateAsync(vehicleAndDateRequest2);
            Assert.Equal(0, CostTollFee);
        }

        [Fact(DisplayName = "Get Exception by Calculat Toll fee for diff Dates")]
        [Trait("Calculat", "Exception")]
        public async Task Test_GetExceptionTollFeeCalculatorforCar()
        {
                       
            var vehicleAndDateRequest = new VehicleAndDateRequest
            {
                Vehicle = new Car(),
                TollDates = new List<DateTime>{
                                              new DateTime(2020, 05, 9, 09, 30, 01,01),
                                              new DateTime(2020, 05, 9, 09, 36, 20,10),
                                               DateTime.Now.AddDays(1)
                                          }
            };
            
            await Assert.ThrowsAsync<Exception>(() => _tollFreeForVehicle.GetTotalTollFeeForDateAsync(vehicleAndDateRequest));

        }

    }
}
