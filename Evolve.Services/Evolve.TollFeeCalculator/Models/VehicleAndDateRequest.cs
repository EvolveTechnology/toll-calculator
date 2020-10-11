using System;
using System.Collections.Generic;
using System.Text;
using Evolve.TollFeeCalculator.Interfaces;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    /// Request class (Vehicle, list date) for calculation.
    /// </summary>
    public class VehicleAndDateRequest
    {        
        public IVehicle Vehicle { get; set; }      
        public List<DateTime> TollDates { get; set; }
    }
}
