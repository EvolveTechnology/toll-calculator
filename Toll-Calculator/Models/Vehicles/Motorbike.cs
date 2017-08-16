using Toll_Calculator.Enums;
using Toll_Calculator.Interfaces;

namespace Toll_Calculator.Models.Vehicles
{
    public class Motorbike : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }

        public VehicleType GetVehicleType()
        {
            return VehicleType.Motorbike;
        }
    }
}
