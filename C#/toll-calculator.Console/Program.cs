using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Targets;
using System.Collections.Generic;
using System.Threading;
using TollCalculator.Console.Commands;
using TollCalculator.Contracts.Vehicles;
using TollCalculator.Gothenburg;

namespace TollCalculator.Console
{
    public class Program
    {
        private static Microsoft.Extensions.Logging.ILogger Logger { get; set; }

        static void Main(string[] args)
        {
            SetupLogger();
            var tollCalendar = new SwedishTollFeeCalendar();
            var tollFeeRepository = new GothenburgTollFeeRepository(tollCalendar, Logger);
            var tollFeeCalculator = new TollFeeCalculator(tollFeeRepository, Logger);
            var context = new Context(tollFeeCalculator, tollFeeRepository, Logger)
            {
                Vehicle = new Vehicle("ABC012", "SE", VehicleType.Car)
            };

            CreateCommands().Execute(context);
        }

        private static ICommand CreateCommands()
        {
            return new MenuCommand()
            {
                Name = "Gothenburg Toll Calculator",
                BackType = MenuBackType.Exit,
                MenuItems = new List<ICommand>()
                {
                    new MenuCommand()
                    {
                        Name = "Show Toll Rules",
                        BackType = MenuBackType.Back,
                        MenuItems = new List<ICommand>()
                        {
                            new ShowRulesCommand()
                            {
                                Name = "Gothenburg Rules 2013",
                                Rules = GothenburgTollFeeRules.Rules2013
                            },
                            new ShowRulesCommand()
                            {
                                Name = "Gothenburg Rules 2014",
                                Rules = GothenburgTollFeeRules.Rules2014
                            },
                            new ShowRulesCommand()
                            {
                                Name = "Gothenburg Rules 2015-",
                                Rules = GothenburgTollFeeRules.Rules2015
                            }
                        }
                    },
                    new DailyTollCommand(),
                    new PerformanceTestCommand(),
                    new ConfigureVehicleCommand(),
                    new TollCalendarCommand()
                    {
                        TollCalendar = new SwedishTollFeeCalendar()
                    }
                }
            };
        }

        /// <summary>
        /// Setup Microsoft.Extensions.Logging with NLog provider (pre-release for .NET 2.0)
        /// </summary>
        private static void SetupLogger()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.FileName = "${basedir}/TollCalculator.Console.log";
            fileTarget.Layout =
                @"${date:format=O}: [${pad:padding=3:inner=${threadid}}] [${pad:padding=5:inner=${level:uppercase=true}}] ${message}";
            fileTarget.ArchiveAboveSize = 5 * 1024 * 1024;
            fileTarget.ConcurrentWrites = false;

            var rule1 = new LoggingRule("*", NLog.LogLevel.Info, fileTarget);
            config.LoggingRules.Add(rule1);
            config.DefaultCultureInfo = Thread.CurrentThread.CurrentCulture;

            LogManager.Configuration = config;

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddNLog();

            Logger = loggerFactory.CreateLogger("TollCalculator.Console");
        }
    }
}
