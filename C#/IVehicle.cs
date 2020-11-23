using System;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator
{

    interface IVehicle
    {
        public String GetVehicleType();
        //Adding member
        public static String _VehicleType;
    }
}
