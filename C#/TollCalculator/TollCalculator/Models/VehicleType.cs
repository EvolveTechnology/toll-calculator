using System.Collections;
using System.Linq.Expressions;
using System.Net;
using TollFeeCalculator.Extensions;

namespace TollFeeCalculator.Models
{
    public class VehicleType : Enumeration
    {
        public static int enumValue = 0;
        public static readonly VehicleType Car = new VehicleType( "Car");
        public static readonly VehicleType Motorbike = new VehicleType( "Motorbike");
        public static readonly VehicleType Tractor = new VehicleType( "Tractor");
        public static readonly VehicleType Emergency = new VehicleType( "Emergency");
        public static readonly VehicleType Diplomat = new VehicleType( "Diplomat");
        public static readonly VehicleType Foreign = new VehicleType( "Foreign");
        public static readonly VehicleType Military = new VehicleType( "Military");

        public VehicleType(string name)
            : base(enumValue, name)
        {
            enumValue++;
        }
    }

}
