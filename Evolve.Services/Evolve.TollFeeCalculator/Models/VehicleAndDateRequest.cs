using System;
using System.Collections.Generic;
using System.Text;
using Evolve.TollFeeCalculator.Interfaces;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    /// Request klass (Vehicle, list datum) för kalkylering. 
    /// </summary>
    public class VehicleAndDateRequest
    {
        /// <summary>
        /// Vehicle type
        /// </summary>
        public IVehicle Vehicle { get; set; }
        /// <summary>
        /// datum list
        /// </summary>
        public List<DateTime> TollDates { get; set; }
    }
}
