using System;
using ConsoleApp1;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class VehicleType
    {
        public int VehicleTypeID { get; set; }

        public string VehicleName { get; set; }

        public VehicleType(int id, string name)
        {
            this.VehicleTypeID = id;
            this.VehicleName = name;
        }

        public bool VehicleTypeIsFreeToll()
        {
            return DummyDatabase.GetTollFreeVehicleTypes().Any(a => a.VehicleTypeID == this.VehicleTypeID);
        }
    }
}
