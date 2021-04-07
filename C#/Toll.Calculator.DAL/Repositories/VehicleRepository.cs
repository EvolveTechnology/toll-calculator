using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Toll.Calculator.DAL.Repositories.Interface;
using Toll.Calculator.Domain;
using Toll.Calculator.Infrastructure.Options;

namespace Toll.Calculator.DAL.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly TollFreeVehicleOptions _tollFreeVehicleOptions;

        public VehicleRepository(
            IOptions<TollFreeVehicleOptions> tollFreeVehicleOptions)
        {
            _tollFreeVehicleOptions = tollFreeVehicleOptions.Value;
        }

        public async Task<List<Vehicle>> GetTollFreeVehicles()
        {
            //Simulate db access
            await Task.Delay(TimeSpan.FromMilliseconds(5));

            //Taken from config for simplicity,
            //could be connected to seperate database and method made async.
            return _tollFreeVehicleOptions.TollFreeVehicles;
        }
    }
}