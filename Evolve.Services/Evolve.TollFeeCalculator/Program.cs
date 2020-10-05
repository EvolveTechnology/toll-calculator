using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Evolve.TollFeeCalculator.Models;
using Evolve.TollFeeCalculator.Config;
using Evolve.TollFeeCalculator.Interfaces;

using Evolve.TollFeeCalculator.Services;
using System.Threading.Tasks;

namespace Evolve.TollFeeCalculator
{
    class Program
    {
        private static IConfigurationRoot _configuration;
        static async Task Main(string[] args)
        {
            var EnviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnviromentName}.json", optional: true, reloadOnChange: true);
            _configuration = builder.Build();

            IAppConfiguration appConfiguration = new AppConfiguration(_configuration);

            File.AppendAllText(Globals.AppConfiguration.LogFilePath, $"Windows Service Started {DateTime.Now.ToString()}\n");
            

            Console.ReadKey();
        }

    }
}

