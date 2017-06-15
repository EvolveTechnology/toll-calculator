using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Diplomat : TollFreeVehicle
    {
        override public string GetVehicleType()
        {
            return "Diplomat";
        }

    }
}
