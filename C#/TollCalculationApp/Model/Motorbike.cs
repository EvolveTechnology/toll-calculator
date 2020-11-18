using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Motorbike : IVehicle
    {
        bool IVehicle.IsTollFree
        {
            get { return true; }
        }
 
        string IVehicle.GetVehicleType()
        {
            return "Motorbike";
        }
    }
}
