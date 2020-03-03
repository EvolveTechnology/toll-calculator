using System;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator.Interfaces
{
    public interface ITollFreeVehicles
    {
        bool IsTollFreeVehicle(IVehicle vehicle);
    }
}
