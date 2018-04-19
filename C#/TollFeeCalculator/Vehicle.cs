using System;

namespace TollFeeCalculator
{
    public interface Vehicle
    {
        String GetVehicleType();
        bool IsTollFree();
    }
}