using TollFeeCalculator.Enums;

namespace TollFeeCalculator
{
    public class Motorbike : IVehicle
    {
        public VehicleTypeEnum GetVehicleType()
        {
            return VehicleTypeEnum.Motorbike;
        }
    }
}
