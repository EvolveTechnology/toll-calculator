using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Vehicles;

namespace Toll_calculator {

    interface ITollCalculator {

        int GetTollFee(IVehicle vehicle, DateTime[] dates);

    }
}
