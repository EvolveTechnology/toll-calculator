using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Motorbike : Vehicle
    {
        public override bool IsTollFree()
        {
            return true;
        }
    }
}
