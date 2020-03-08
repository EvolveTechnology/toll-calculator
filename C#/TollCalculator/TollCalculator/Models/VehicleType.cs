using System.Collections;
using System.Linq.Expressions;
using TollFeeCalculator.Extensions;

namespace TollFeeCalculator.Models
{
    public class VehicleType : Enumeration
    {
        public static readonly VehicleType Car = new VehicleType(1, "Car");
        public static readonly VehicleType Motorbike = new VehicleType(2, "Motorbike");
        public static readonly VehicleType Tractor = new VehicleType(3, "Tractor");
        public static readonly VehicleType Emergency = new VehicleType(4, "Emergency");
        public static readonly VehicleType Diplomat = new VehicleType(5, "Diplomat");
        public static readonly VehicleType Foreign = new VehicleType(6, "Foreign");
        public static readonly VehicleType Military = new VehicleType(7, "Military");

        public VehicleType(int id, string name)
            : base(id, name)
        {
        }
    }

}
