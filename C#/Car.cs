using TollFeeCalculator.Enums;

namespace TollFeeCalculator
{
    public class Car : IVehicle
    {
        public VehicleTypeEnum GetVehicleType()
        {
            return VehicleTypeEnum.Car;
        }
    }
}