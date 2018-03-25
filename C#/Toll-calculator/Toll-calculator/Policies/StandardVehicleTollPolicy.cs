using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Vehicles;

namespace Toll_calculator.Policies {

    /**
     * The visitor component in the visitor patterns used for checking
     * if a vehicle is tollable.
     */
    public class StandardVehicleTollPolicy : IVehicleTollPolicy {

        /**
         * Default fallback for unspecified vehicle types.
         */
        public bool IsTollable(IVehicle vehicle) {
            return false;
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

        public bool IsTollable(MilitaryVehicle militaryVehicle) {
            return false;
        }

        public bool IsTollable(EmergencyVehicle emergencyVehicle) {
            return false;
        }

        public bool IsTollable(DiplomaticVehicle diplomaticVehicle) {
            return false;
        }

    }
}
