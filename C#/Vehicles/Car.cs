using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Car : IVehicle
    {
        public String GetVehicleType()
        {
            return "Car";
        }

        public bool IsFeeFree()
        {
          return false;
        }
    }
}
