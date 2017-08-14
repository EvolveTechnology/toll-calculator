using System;
using Toll_Calculator.Enums;
using Toll_Calculator.Interfaces;

namespace Toll_Calculator.Models.Vehicles
{
    public class Car : IVehicle
    {
        public bool IsTollFree()
        {
            return false;
        }

        VehicleType IVehicle.GetVehicleType()
        {
            return VehicleType.Car;
        }
    }
}