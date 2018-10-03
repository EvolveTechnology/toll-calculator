using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator.Vehicles
{
    public abstract class Vehicle
    {
        public Vehicle(VehicleFeeType[] vehicleFeeTypes)
        {
            this.VehicleFeeTypes = vehicleFeeTypes;
        }

        public  VehicleFeeType[] VehicleFeeTypes { get; }
        public bool IsNotTollable => this.VehicleFeeTypes.Any(vc => vc != VehicleFeeType.Car);
    }
}