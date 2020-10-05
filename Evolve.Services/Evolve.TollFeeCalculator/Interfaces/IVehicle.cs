using Evolve.TollFeeCalculator.Enums;


namespace Evolve.TollFeeCalculator.Interfaces
{
    /// <summary>
    /// Vehicle Interface
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// VehicleType 
        /// </summary>
        /// <returns></returns>
        VehicleType GetVehicleType();
        /// <summary>
        /// 
        /// </summary>
        string RegNo { get; }
    }
}