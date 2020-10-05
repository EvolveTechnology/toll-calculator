using Evolve.TollFeeCalculator.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Interfaces
{
    /// <summary>
    /// Inläst konfiguration
    /// </summary>
    public interface IAppConfiguration
    {
        /// <summary>
        /// Jobbkostnad vid tidpunkten
        /// </summary>
        FeeCostToTime FeeCostToTime { get; }
        /// <summary>
        /// håller Kostnadsparametrar
        /// </summary>
        CostParameters CostParameters { get; }

        /// <summary>
        /// Filsökväg till loggfilen.
        /// </summary>
        string LogFilePath { get; }

        /// <summary>
        /// 
        /// </summary>
        FreeDays FreeDays { get; }
    }
}
