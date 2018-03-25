using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Vehicles;

namespace Toll_calculator {
    class StandardVehicleTollPolicy : IVehicleTollPolicy {

        /**
         * Default fallback for unspecified vehicle types.
         */
        public bool IsTollable(IVehicle vehicle) {
            return true;
        }

        public bool IsTollable(Car car) {
            return true;
        }
        public bool IsTollable(Motorbike motorbike) {
            return false;
        }

        public bool IsTollable(Tractor tractor) {
            return false;
        }

    }
}
