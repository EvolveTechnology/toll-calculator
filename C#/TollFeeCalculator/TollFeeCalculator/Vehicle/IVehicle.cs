namespace TollFeeCalculator.Vehicle
{
    /// <summary>
    /// Represents a vehicle that passes through toll stations.
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Returns true if this vehicle is exempted from paying toll fees.
        /// </summary>
        /// <returns></returns>
        bool IsTollFree();
    }
}
