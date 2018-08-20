using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public abstract class Vehicle
    {
        public VehicleType vehicleType { get; set; }    
        
        /// <summary>
        /// Returns vehicle type name of the given vehicle.
        /// </summary>
        /// <returns></returns>
        public String GetVehicleType()
        {
            return vehicleType.VehicleName;
        }
    }
}