using Evolve.TollCalculator.Core.Common;
using Evolve.TollCalculator.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Evolve.TollCalculator.API.Extensions
{
    public static class VehicleInstanceExtenstion
    {
        static readonly Dictionary<string, Vehicle> vehicleInstances = new Dictionary<string, Vehicle>
        {
            {"Car", new Car() },
            {"Diplomat", new Diplomat() },
            {"Emergency", new Emergency() },
            {"Foreign", new Foreign() },
            {"Military", new Military() },
            {"MotorBike", new MotorBike() },
            {"Tractor", new Tractor() },
        };

        public static Vehicle GetVehicleByName(string vehicleType)
        {
            if (vehicleInstances.ContainsKey(vehicleType))
            {
                return vehicleInstances.Single(x => x.Key == vehicleType).Value;
            }
            return null;
        }
    }
}
