using System;

namespace TollFeeCalculator.Interfaces
{
    public interface ITollCalculator
    {
        decimal GetDailyTollFee(IVehicle vehicle, DateTime[] dates);
    }
}
