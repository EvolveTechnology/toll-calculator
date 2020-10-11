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
        /// Job cost at the time
        /// </summary>
        FeeCostToTime FeeCostToTime { get; }
        /// <summary>
        /// Cost Parameters
        /// </summary>
        CostParameters CostParameters { get; }

        /// <summary>
        /// File path to the log file.
        /// </summary>
        string LogFilePath { get; }

        /// <summary>
        /// free days
        /// </summary>
        FreeDays FreeDays { get; }
    }
}
