using System;
using System.Collections.Generic;
using System.Text;

namespace TollCalculator
{
    public enum Vehicle
    {
        Car,
        Truck,

        [TollFree]
        Motorbike ,
        [TollFree]
        Tractor,
        [TollFree]
        Emergency,
        [TollFree]
        Diplomat,
        [TollFree]
        Foreign,
        [TollFree]
        Military
    }
}
