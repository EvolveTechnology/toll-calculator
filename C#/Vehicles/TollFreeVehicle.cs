using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public abstract class TollFreeVehicle : IVehicle
    {
        abstract public string GetVehicleType();

        public bool IsFeeFree()
        {
          return true;
        }
    }
}
