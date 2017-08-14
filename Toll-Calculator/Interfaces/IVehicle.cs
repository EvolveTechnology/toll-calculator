using System;
using Toll_Calculator.Enums;

namespace Toll_Calculator.Interfaces
{
    public interface IVehicle
    {
        bool IsTollFree();
        VehicleType GetVehicleType();
    }
}