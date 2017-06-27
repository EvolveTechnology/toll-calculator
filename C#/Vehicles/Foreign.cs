using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Foreign : TollFreeVehicle
    {
        override public string GetVehicleType()
        {
            return "Foreign";
        }

    }
}
