using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollCalculatorApp.Models
{
    public class Vehicle : IVehicle
    {
        private VehiclesType _vehicleType { get; }

        public Vehicle(VehiclesType vehicleType)
        {
            _vehicleType = vehicleType;
        }

        public VehiclesType VehicleType => _vehicleType;
    }
}