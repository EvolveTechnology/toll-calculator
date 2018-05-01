using TollFeeCalculator.Enums;

namespace TollFeeCalculator
{
    public interface IVehicle
    {
        VehicleTypeEnum GetVehicleType();
    }
}