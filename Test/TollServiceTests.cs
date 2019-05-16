using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TollCalculator.Interfaces;
using TollCalculator.Repository;
using TollCalculator.Services;

namespace Test
{
    [TestClass]
    public class TollServiceTests
    {
        ITollService _tollService;
        ITollRepository _tollRepository;
        readonly int maxFee = 18;
        readonly int minimumFee = 8;
        readonly int maxFeeForDay = 60;
        readonly int tollFreeVehicleCharge = 0;
        readonly int weekendOrHoliday = 0;

        [TestInitialize]
        public void Setup() {

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ITollRepository, TollRepository>()
                .AddTransient<ITollService, TollService>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>();

            _tollService = serviceProvider.GetService<ITollService>();
            _tollRepository = serviceProvider.GetService<ITollRepository>();
        }

        [TestMethod]
        public void MaxFeeWithinHour()
        {
            //For rush hour
            //arrange
            var car = _tollRepository.GetCar();
            var dates = _tollRepository.GetDatesWithinHour();

            //act
            var price = _tollService.CalculateFee(car, dates);

            //assert
            Assert.AreEqual(maxFee, price);

        }

        [TestMethod]
        public void GetMinimumFee()
        {
            //arrange
            var car = _tollRepository.GetCar();
            var dates = _tollRepository.GetMinimumTollHours();

            var minimumTollCharge = new List<int>();

            //act
            foreach (var time in dates)
            {
                var tollTime = new List<DateTime>();
                tollTime.Add(time);
                minimumTollCharge.Add(_tollService.CalculateFee(car, tollTime));
            }

            //assert
            foreach (var tollCharge in minimumTollCharge)
            {
                Assert.AreEqual(minimumFee, tollCharge);
            }

        }

        [TestMethod]
        public void GetMaximumFeeForRushHour()
        {
            //arrange
            var car = _tollRepository.GetCar();
            var dates = _tollRepository.GetMaximumTollHours();

            var maxTollCharge = new List<int>();
            var timeToList = new List<DateTime>();

            foreach (var time in dates)
            {
                timeToList.Add(time);
                maxTollCharge.Add(_tollService.CalculateFee(car, timeToList));
            }

            //assert
            foreach (var tollCharge in maxTollCharge)
            {
                Assert.AreEqual(maxFee, tollCharge);
            }
        }

        [TestMethod]
        public void MaximumFeeForOneDay()
        {
            //arrange
            var car = _tollRepository.GetCar();
            var dates = _tollRepository.GetOverChargeDayToll();

            //act
            var price = _tollService.CalculateFee(car, dates);

            //assert
            Assert.AreEqual(maxFeeForDay, price);
        }

        [TestMethod]
        public void TollFreeVehicleNoCharge()
        {

            //arrange
            var tollFreeVehicles = _tollRepository.GetAllTollFreeVehicles();
            var dates = _tollRepository.GetDates();

            //act
            foreach (var vehicle in tollFreeVehicles)
            {
                var price = _tollService.CalculateFee(vehicle, dates);
                
                //assert
                Assert.AreEqual(tollFreeVehicleCharge, price);
            }
        }

        [TestMethod]
        public void WeekendShouldBeTollFree()
        {
            //arrange
            var car = _tollRepository.GetCar();
            var dates = _tollRepository.GetWeekends();

            //act
            var price = _tollService.CalculateFee(car, dates);

            //assert
            Assert.AreEqual(weekendOrHoliday, price);
        }

        [TestMethod]
        public void HolidayShouldBeTollFree()
        {
            //arrange
            var car = _tollRepository.GetCar();
            var dates = _tollRepository.GetHolidays();

            //act
            var price = _tollService.CalculateFee(car, dates);

            //assert
            Assert.AreEqual(weekendOrHoliday, price);
        }

    }
}
