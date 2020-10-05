using Evolve.TollFeeCalculator.Config;
using Evolve.TollFeeCalculator.Interfaces;
using Evolve.TollFeeCalculator.Models;
using Evolve.TollFeeCalculator.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;

namespace Evolve.TollFeeCalculator.Test
{
    public class GeneralUnitTest
    {
        private IConfiguration _configuration { get; set; }
        public GeneralUnitTest()
        {
            _configuration = new ConfigurationBuilder()
             .AddJsonFile("client-secrets.json")
              .Build();
            IAppConfiguration appConfiguration = new AppConfiguration(_configuration);
        }


        /// <summary>
        ///  Facts are tests which are always true. They test invariant conditions.
        ///  test Configuration
        /// </summary>
        [Fact]
        public void Test_Configuration()
        {
            Assert.Equal(4, Add(2, 2));
            Assert.Equal(1, Globals.AppConfiguration.CostParameters.ExtraCostFactor);
        }


        [Fact]
        public async System.Threading.Tasks.Task Test_TollFeeCalculatorService()
        {

            ITollFeeCalculatorService tollFreeForVehicle = new TollFeeCalculatorService();

            // The Total toll fee for Vehicle 

            var vehicleAndDateRequest = new VehicleAndDateRequest
            {
                Vehicle = new Car(),
                TollDates = new List<DateTime>{
                                              new DateTime(2019, 05, 8, 10, 30, 0),
                                              new DateTime(2019, 05, 9, 10, 30, 0),
                                              new DateTime(2019, 05, 9, 10, 56, 0)}
            };

            var CostTollFee = await tollFreeForVehicle.GetTotalTollFeeForDateAsync(vehicleAndDateRequest);
            Assert.Equal(24, CostTollFee);



            var vehicleAndDateRequest2 = new VehicleAndDateRequest
            {
                Vehicle = new Motorbike(),
                TollDates = new List<DateTime>{
                                              new DateTime(2019, 05, 9, 10, 30, 01,01),
                                              new DateTime(2019, 05, 9, 11, 36, 20,10),
                                              new DateTime(2019, 05, 9, 12, 56, 0,20)}
            };

            CostTollFee = await tollFreeForVehicle.GetTotalTollFeeForDateAsync(vehicleAndDateRequest2);
            Assert.Equal(0, CostTollFee);


            var vehicleAndDateRequest3 = new VehicleAndDateRequest
            {
                Vehicle = new Car(),
                TollDates = new List<DateTime>{
                                              new DateTime(2020, 05, 9, 09, 30, 01,01),
                                              new DateTime(2020, 05, 9, 09, 36, 20,10),
                                              new DateTime(2020, 10, 13, 12, 56, 0,20)}
            };          

            await Assert.ThrowsAsync<Exception>(() => tollFreeForVehicle.GetTotalTollFeeForDateAsync(vehicleAndDateRequest3));
           // Assert.That(ex.Message, Is.EqualTo("Actual exception message"));

        }

        /// <summary>
        /// Theories are tests which are only true for a particular set of data.
        /// </summary>
        /// <param name="value"></param>
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(6)]
        public void MyFirstTheory_test(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }

        int Add(int x, int y)
        {
            return x + y;
        }
    }
}
