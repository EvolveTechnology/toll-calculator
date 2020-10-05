using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Enums
{
    /// <summary>
    /// Enum for vanligaste freeavgifterna som är kopplade till fordon.
    /// </summary>
    public enum TollFreeVehicles
    {
        /// <summary>
        /// Motorbike
        /// </summary>
        Motorbike = 0,
        /// <summary>
        /// Tractor
        /// </summary>
        Tractor = 1,
        /// <summary>
        /// Emergency
        /// </summary>
        Emergency = 2,
        /// <summary>
        /// Diplomat
        /// </summary>
        Diplomat = 3,
        /// <summary>
        /// Foreign
        /// </summary>
        Foreign = 4,
        /// <summary>
        /// Military
        /// </summary>
        Military = 5
    }
}
