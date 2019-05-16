using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollCalculator.Enums;

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
}