using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    public interface IVehicle
    {

        // Typical property of a Vehicle.
        bool IsTollFree { 
            get 
            { 
                return IsTollFreeVehicle(); 
            }
        }
        String GetVehicleType();

        private bool IsTollFreeVehicle()
        {
            String vehicleType = this.GetVehicleType();

            if (vehicleType == "Car")
                return false;
            else
                return true;
            
            // not working ...
            //vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
            //       vehicleType.Equals(TollFreeVehicles.Military.ToString());
        }

        private enum TollFreeVehicles
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5
        }
    }
}