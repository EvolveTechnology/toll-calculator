using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Vehicles;

namespace Toll_calculator {
    public interface IVehicleTollPolicy {

        bool IsTollable(IVehicle vehicle);

        bool IsTollable(Car car);

        bool IsTollable(Motorbike motorbike);

        bool IsTollable(Tractor tractor);

        bool IsTollable(MilitaryVehicle militaryVehicle);

        bool IsTollable(EmergencyVehicle emergencyVehicle);

        bool IsTollable(DiplomaticVehicle diplomaticVehicle);

    }
}
