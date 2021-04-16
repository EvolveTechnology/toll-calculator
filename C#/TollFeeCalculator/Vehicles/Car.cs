using System;

namespace TollFeeCalculator.Vehicles
{
    public class Car : IVehicle
    {
        public String GetVehicleType()
        {
            return "Car";
        }

        public bool IsTollFree() => false;
    }
}