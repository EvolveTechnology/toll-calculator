using System;
using System.Linq;

namespace TollFeeCalculator.Toll
{

    public class VehicleType : IVehicleType
    {
        public Enums.TollFreeVehicle GetVehicleType(object t)
        {
            //Filter enum list and return type of vehicle
            var feeTypes = Enum.GetValues(typeof(Enums.TollFreeVehicle))
                    .Cast<Enums.TollFreeVehicle>()
                    .Select(d => d)
                    .ToList();
            var vehicleType = feeTypes.Where(x => x.ToString() == t.GetType().Name).ToList();
            return vehicleType.Count == 0 ? Enums.TollFreeVehicle.None : vehicleType.ToList()[0];
        }

        public bool IsFeeFree(object t)
        {
            //filter all enum of FeeFree categories excpect None category
            var feeTypes = Enum.GetValues(typeof(Enums.TollFreeVehicle))
                   .Cast<Enums.TollFreeVehicle>()
                   .Select(d => d)
                   .ToList();

            return feeTypes.Any(x => x.ToString() == t.GetType().Name && x != Enums.TollFreeVehicle.None);
        }
    }
}
