using TollCalculator.Enums;

namespace TollCalculator.Policies;

public sealed class VehicleTypePolicy
{
    public bool IsFeeFree(VehicleType vehicleType)
    {
        switch (vehicleType)
        {
            case VehicleType.Motorbike:
            case VehicleType.Tractor:
            case VehicleType.Emergency:
            case VehicleType.Diplomat:
            case VehicleType.Foreign:
            case VehicleType.Military:
                return true;
            case VehicleType.Private:
                return false;
            default:
                throw new ArgumentOutOfRangeException(nameof(vehicleType), vehicleType, null);
        }
    }
}