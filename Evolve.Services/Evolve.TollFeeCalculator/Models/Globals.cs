using Evolve.TollFeeCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    /// Klass som håller globala variabler.
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// Programinställningar.
        /// </summary>
        public static IAppConfiguration AppConfiguration { get; set; }
    }
}
