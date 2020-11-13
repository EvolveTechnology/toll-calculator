using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace toll_calculator
{
    public class VehicleFactory : IVehicleFactory
    {
        private IConfiguration DefaultZoneConfiguration =  new ThisTownZoneConfiguration();
        private List<Vehicle> registeredVehicles = new List<Vehicle>();
        public IVehicle GetVehicle(VehicleType vehicle)
        {
            var registeredVehicle = registeredVehicles.FirstOrDefault(v => v.Type == vehicle);
            if (registeredVehicle == null)
                return new Vehicle(new TollFeeAggregator(new TollFeePeriod(new TimeTable(DefaultZoneConfiguration))),VehicleType.Unknown);
            return registeredVehicle;
        }

        public void RegisterVehicle(Vehicle vehicle)
        {
            registeredVehicles.Add(vehicle);
        }
    }
}
