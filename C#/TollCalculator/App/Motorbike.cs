
using TollCalculatorApp;

namespace TollFeeCalculator
{
    public class Motorbike : IVehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Motorbike;
        }
    }
}
