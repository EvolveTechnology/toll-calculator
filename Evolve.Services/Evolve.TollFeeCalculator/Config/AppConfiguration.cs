using Evolve.TollFeeCalculator.Interfaces;
using Evolve.TollFeeCalculator.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Evolve.TollFeeCalculator.Config
{
    /// <summary>
    /// Application specific configurations
    /// </summary>
    public class AppConfiguration : IAppConfiguration
    {
        /// <summary>
        /// Job cost at the time
        /// </summary>
        public FeeCostToTime FeeCostToTime { get; set; }

        /// <summary>
        /// File path to the log file.
        /// </summary>
       // public string LogFilePath { get; set; }


        /// <summary>
        /// Parameter for calculation
        /// </summary>
        public CostParameters CostParameters { get; set; }

        /// <summary>
        /// Free days for calculation
        /// </summary>
        public FreeDays FreeDays { get; set; }
        /// <summary>
        ///Application configuration.
        /// </summary>
        public AppConfiguration(IConfiguration configuration)
        {          

            FeeCostToTime = new FeeCostToTime
            {
                ZoneTime6a = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime6a").Value.ToString()),
                ZoneTime6b = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime6b").Value.ToString()),
                ZoneTime7 = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime7").Value.ToString()),
                ZoneTime8a = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime8a").Value.ToString()),
                ZoneTime8b = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime8b").Value.ToString()),
                ZoneTime15a = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime15a").Value.ToString()),
                ZoneTime15b = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime15b").Value.ToString()),
                ZoneTime17 = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime17").Value.ToString()),
                ZoneTime18 = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTime18").Value.ToString()),
                ZoneTimefree = int.Parse(configuration.GetSection("FeeCostToTime:ZoneTimefree").Value.ToString())
            };

            CostParameters = new CostParameters
            {
                MaxDiffInMinutes = int.Parse(configuration.GetSection("CostParameters:MaxDiffInMinutes").Value.ToString()),
                ExtraCostFactor = int.Parse(configuration.GetSection("CostParameters:ExtraCostFactor").Value.ToString()),
                MaxtotalCost = int.Parse(configuration.GetSection("CostParameters:MaxtotalCost").Value.ToString())
            };

            FreeDays = new FreeDays
            {
                Year = int.Parse(configuration.GetSection("FreeDays:Year").Value.ToString()),
                JANUARY = configuration.GetSection("FreeDays:JANUARY").AsEnumerable().Where(p => p.Value != null).Select(p => p.Value).Select(p => int.Parse(p.ToString())).ToList(),               
                MARCH = configuration.GetSection("FreeDays:MARCH").AsEnumerable().Where(p => p.Value != null).Select(p => p.Value).Select(p => int.Parse(p.ToString())).ToList(),
                APRIL = configuration.GetSection("FreeDays:APRIL").AsEnumerable().Where(p => p.Value != null).Select(p => p.Value).Select(p => int.Parse(p.ToString())).ToList(),
                MAY = configuration.GetSection("FreeDays:MAY").AsEnumerable().Where(p => p.Value != null).Select(p => p.Value).Select(p => int.Parse(p.ToString())).ToList(),
                JUNE = configuration.GetSection("FreeDays:JUNE").AsEnumerable().Where(p => p.Value != null).Select(p => p.Value).Select(p => int.Parse(p.ToString())).ToList(),
                NOVEMBER = configuration.GetSection("FreeDays:NOVEMBER").AsEnumerable().Where(p => p.Value != null).Select(p => p.Value).Select(p => int.Parse(p.ToString())).ToList(),
                DECEMBER = configuration.GetSection("FreeDays:DECEMBER").AsEnumerable().Where(p => p.Value != null).Select(p => p.Value).Select(p => int.Parse(p.ToString())).ToList()

            };

           // LogFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "log.txt");
            Globals.AppConfiguration = this;

        }
    }

}
