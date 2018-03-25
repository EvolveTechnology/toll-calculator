using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Policies;

namespace Toll_calculator.Vehicles {
    public class ForeignVehicle : IVehicle {

        public bool IsTollable(IVehicleTollPolicy vehicleTollPolicy) {
            return vehicleTollPolicy.IsTollable(this);
        }

    }
}
