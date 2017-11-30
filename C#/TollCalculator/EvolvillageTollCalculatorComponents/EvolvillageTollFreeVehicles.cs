using System.Collections.Generic;

namespace TollFeeCalculator
{
   public class EvolvillageTollFreeVehicles : ITollFreeVehicles
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

      public bool IsTollFree(IVehicle vehicle)
      {
         return _tollFreeVehicles.Contains(vehicle.GetVehicleType());
      }
   }
}