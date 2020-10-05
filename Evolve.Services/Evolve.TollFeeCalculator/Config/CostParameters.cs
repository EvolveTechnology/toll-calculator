using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Config
{
    /// <summary>
    /// parameter för beräkninga
    /// </summary>
    public class CostParameters
    {
        /// <summary>
        /// Max Diff I Minuter För Beräkning
        /// </summary>
        public int MaxDiffInMinutes { get; set; }
        /// <summary>
        /// Extrakostnadsfaktor ex  1.1
        /// </summary>
        public int ExtraCostFactor { get; set; }
        /// <summary>
        /// Max totala kostnad
        /// </summary>
        public int MaxtotalCost { get; set; }
    }
}
