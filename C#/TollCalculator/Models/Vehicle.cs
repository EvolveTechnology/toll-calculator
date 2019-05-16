using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollCalculator.Models
{
    public class Vehicle
    {
        public VehicleType vehicleType { get; set; }
        public bool IsTollFree()
        {
            return Enum.IsDefined(typeof(TollFreeVehicles), (int)this.vehicleType);
        }

    };

    public enum VehicleType
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5,
        Car = 6,
    }
}