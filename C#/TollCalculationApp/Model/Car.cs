using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public class Car : IVehicle
    {
        public Car(string plate)
        {
            Plate = plate;
        }

        public string Plate;

        bool IVehicle.IsTollFree
        {
            get { return false; }
        }

        string IVehicle.GetVehicleType()
        {
            return "Car";
        }
    }
}