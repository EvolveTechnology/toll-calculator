using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator.Vehicles {

    public interface IVehicle {

        bool IsTollable(IVehicleTollPolicy vehicleTollPolicy);

    }
}
