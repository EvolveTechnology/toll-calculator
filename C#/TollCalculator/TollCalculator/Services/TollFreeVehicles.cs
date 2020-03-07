using System.Collections.Generic;
using TollFeeCalculator.Interfaces;

namespace TollFeeCalculator.Services
{
    public class TollFreeVehicles : ITollFreeVehicles
    {
        private readonly List<VehicleType> _tollFreeVehicles = new List<VehicleType>
        {
            VehicleType.Motorbike,
            VehicleType.Tractor,
            VehicleType.Emergency,
            VehicleType.Diplomat,
            VehicleType.Foreign,
            VehicleType.Military
        };

        public bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null) return false;

            return _tollFreeVehicles.Contains(vehicle.GetVehicleType());
        }
    }
}
