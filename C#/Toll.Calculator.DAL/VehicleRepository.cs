using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toll.Calculator.Domain;
using Toll.Calculator.Infrastructure;

namespace Toll.Calculator.DAL
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly TollFreeVehicleOptions _tollFreeVehicleOptions;

        public VehicleRepository(
            IOptions<TollFreeVehicleOptions> tollFreeVehicleOptions)
        {
            _tollFreeVehicleOptions = tollFreeVehicleOptions.Value;
        }

        public List<Vehicle> GetTollFreeVehicles()
        {
            //Taken from config for simplicity,
            //could be connected to seperate database and method made async.
            return _tollFreeVehicleOptions.TollFreeVehicles;
        }
    }
}
