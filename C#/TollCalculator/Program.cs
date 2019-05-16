using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TollCalculator.Interfaces;
using TollCalculator.Models;
using TollCalculator.Repository;
using TollCalculator.Services;

namespace TollCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<ITollRepository, TollRepository>()
                .AddTransient<ITollService, TollService>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .CreateLogger(categoryName: "Console");

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //do the actual work here
            var bar = serviceProvider.GetService<ITollService>();
            var car = new Vehicle()
            {
                vehicleType = VehicleType.Car
            };

            DateTime[] dateTimes = new DateTime[]
            {
             new DateTime(2013, 12, 1, 17, 30, 55),
             new DateTime(2013, 12, 21, 16, 00, 00),
             new DateTime(2013, 12, 21, 16, 00, 00),
             new DateTime(2013, 1, 21, 16, 00, 00)
            };

            //var price = bar.CalculateFee(car, dateTimes);
            //Console.WriteLine(price);
            Console.ReadKey();


            logger.LogDebug("All done!");
        }
    }
}
