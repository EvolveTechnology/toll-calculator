using Evolve.TollFeeCalculator.Enums;


namespace Evolve.TollFeeCalculator.Interfaces
{
    /// <summary>
    /// Vehicle Interface
    /// </summary>
    public interface IVehicle
    {        
        VehicleType GetVehicleType();        
        
    }
}