using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Evolve.TollFeeCalculator.Models;
using Evolve.TollFeeCalculator.Config;
using Evolve.TollFeeCalculator.Interfaces;
using Evolve.TollFeeCalculator.Services;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;

namespace Evolve.TollFeeCalculator
{
    /// <summary>
    /// start 
    /// </summary>
    public class Program
    {          
        static void Main(string[] args)
        {
            var EnviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{EnviromentName}.json", optional: true, reloadOnChange: true);
            IConfigurationRoot _configuration = builder.Build();           

            var _builder = ContainerConfig.Configure(_configuration);  

            using (var scope =  _builder.BeginLifetimeScope())
            { 
                var app = scope.Resolve<IAppConfiguration>();               
                File.AppendAllText(app.LogFilePath, $"Windows App-FeeCalculator Started {DateTime.Now.ToString()}\n");
            }
        
            Console.ReadKey();
        }



    }
}

