using System;
using TollFeeCalculator.Vehicle;

namespace TollFeeCalculator
{
    /// <summary>
    /// Represents a toll fee calculator.
    /// </summary>
    public interface ITollFeeCalculator
    {
        /// <summary>
        /// Calculates a total daily toll fee for a specific vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle whose daily toll fee is calculated</param>
        /// <param name="passages">Date and time of all passages on one given day</param>
        /// <returns>Total daily toll fee for the specified vehicle</returns>
        int GetDailyTollFee(IVehicle vehicle, DateTime[] passages);
    }
}
