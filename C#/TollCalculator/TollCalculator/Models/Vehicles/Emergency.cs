using System;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator.Models.Vehicles
{
    public class Emergency : IVehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Emergency;
        }
    }
}
