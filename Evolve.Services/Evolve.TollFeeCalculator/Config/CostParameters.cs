using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Config
{
    /// <summary>
    /// Config class for fee calculations 
    /// </summary>
    public class CostParameters
    {
        /// <summary>
        /// Max Diff In Minutes For Calculation
        /// </summary>
        public int MaxDiffInMinutes { get; set; }
        /// <summary>
        /// Extra cost factor ex 1.1
        /// </summary>
        public int ExtraCostFactor { get; set; }
        /// <summary>
        /// Max total cost
        /// </summary>
        public int MaxtotalCost { get; set; }
    }
}
