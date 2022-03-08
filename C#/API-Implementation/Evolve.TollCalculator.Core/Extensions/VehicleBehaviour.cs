using Evolve.TollCalculator.Core.Common;

namespace Evolve.TollCalculator.Core.Extensions
{
    public static class VehicleBehaviour
    {
        public static bool IsTollFreeVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return false;
            }
            return vehicle.VehicleTollFree;
        }
    }
}
