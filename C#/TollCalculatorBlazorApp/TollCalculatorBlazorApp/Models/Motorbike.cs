using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace TollCalculatorBlazorApp.Models
{
    public class Motorbike : IVehicle
    {
        public VehicleTypesEnum GetVehicleType()
        {
            return VehicleTypesEnum.Motorbike;
        }
    }
}
