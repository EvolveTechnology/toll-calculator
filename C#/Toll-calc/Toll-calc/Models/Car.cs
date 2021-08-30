using System;

namespace Toll_calc.Models
{
    public class Car : Vehicle
    {
        public String GetVehicleType()
        {
            return "Car";
        }

        public bool IsTollFree()
        {
            return false;
        }
    }
}