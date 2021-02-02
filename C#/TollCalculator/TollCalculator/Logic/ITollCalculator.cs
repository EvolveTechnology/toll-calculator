using System;

namespace TollFeeCalculator.Logic
{
    /// <summary>
    /// To calculate the Toll Fees for an entry or for the entire day
    /// </summary>
    public interface ITollCalculator
    {
        int GetTollFee(string vehicleType, DateTime[] dates);

        int GetTollFee(string vehicleType, DateTime date);
    }
}
