
using TollFeeCalculator.Models;

namespace TollFeeCalculator
{
    public interface IVehicle
    {
        public VehicleType VehicleType { get; }

        VehicleType GetVehicleType();
    }
}