using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_Calculator.Enums;
using Toll_Calculator.Interfaces;

namespace Toll_Calculator.Models.Vehicles
{
    public class Tractor : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }

        public VehicleType GetVehicleType()
        {
            return VehicleType.Tractor;
        }
    }
}
