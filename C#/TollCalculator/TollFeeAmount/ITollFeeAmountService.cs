using System;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator.TollFeeAmount
{
    public interface ITollFeeAmountService
    {
        int GetTollFeeAmount(DateTime date, IVehicle vehicle);
    }
}