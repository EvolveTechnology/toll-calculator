using System;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator
{
    public class Vehicle : IVehicle
    {

        String _VehicleType;

        public Vehicle(string vehicleType)
        {
            _VehicleType = vehicleType;
        }


        public String GetVehicleType()
        {
            return _VehicleType;
        }
    }
}
