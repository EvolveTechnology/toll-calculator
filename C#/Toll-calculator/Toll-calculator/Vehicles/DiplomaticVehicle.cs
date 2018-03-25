using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator.Vehicles {
    public class DiplomaticVehicle : IVehicle {

        public bool IsTollable(IVehicleTollPolicy vehicleTollPolicy) {
            return vehicleTollPolicy.IsTollable(this);
        }

    }
}
