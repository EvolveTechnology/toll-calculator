using System;

namespace TollFeeCalculator.Vehicles
{
    public interface IVehicle
    {
        String GetVehicleType();
        bool IsTollFree();
    }
}