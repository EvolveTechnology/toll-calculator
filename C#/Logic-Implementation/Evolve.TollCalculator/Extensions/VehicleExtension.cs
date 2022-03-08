using Evolve.TollCalculator.Models;

namespace Evolve.TollCalculator.Extensions
{
    public static class VehicleExtension
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
