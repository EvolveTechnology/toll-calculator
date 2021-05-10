using System;

namespace TollFeeCalculator
{
    public interface IVehicle
    {
        bool IsTollFree { get; }
    }
}