using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Military : TollFreeVehicle
    {
        override public string GetVehicleType()
        {
            return "Military";
        }

    }
}
