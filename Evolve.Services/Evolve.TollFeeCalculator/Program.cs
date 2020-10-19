using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Evolve.TollFeeCalculator.Models;
using Evolve.TollFeeCalculator.Config;
using Evolve.TollFeeCalculator.Interfaces;
using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Evolve.TollFeeCalculator
{
    /// <summary>
    /// start 
    /// </summary>
    public class Program
    {
        
        static async Task Main(string[] args)
        {
            var EnviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnviromentName}.json", optional: true, reloadOnChange: true);
            IConfigurationRoot _configuration = builder.Build();
        
             var serilogLogger = new LoggerConfiguration()
             .WriteTo.File("TollFee.txt")
             .WriteTo.Console()
             .CreateLogger();

            // DI, Serilog, Settings
            var services = new ServiceCollection()             
            .AddLogging(builder =>
            {
                builder.AddDebug();
                builder.SetMinimumLevel(LogLevel.Debug);
            builder.AddSerilog(logger: serilogLogger, dispose: true);
            })
           .AddOptions();
            var serviceProvider = services.BuildServiceProvider();

            // serviceProvider.GetService<ILoggerFactory>().AddSerilog(logger: serilogLogger);
            // .AddConsole(LogLevel.Debug);
            // var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            serilogLogger.Information("Starting application.....");      

            var _builder = ContainerConfig.Configure(_configuration, services);
/*
            var vehicleAndDateRequest = new VehicleAndDateRequest
            {
                Vehicle = new Car(),
                TollDates = new List<DateTime>{
                                              new DateTime(2020, 05, 9, 09, 30, 01,01),
                                              new DateTime(2020, 05, 9, 09, 36, 20,10),
                                               DateTime.Now.AddDays(1)
                                          }
            };
            try
            {
                using (var scope = _builder.BeginLifetimeScope())
                {
                    var app = scope.Resolve<IAppConfiguration>();
                    ITollFeeCalculatorService _tollFreeForVehicle = scope.Resolve<ITollFeeCalculatorService>();
                    File.AppendAllText(app.LogFilePath, $"Windows App-FeeCalculator Started {DateTime.Now.ToString()}\n");
                    var toll = await _tollFreeForVehicle.GetTotalTollFeeForDateAsync(vehicleAndDateRequest);
                    Console.WriteLine(toll);
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }


            serilogLogger.Debug("All done!");
            */
            Console.ReadKey();
        }



    }
}

