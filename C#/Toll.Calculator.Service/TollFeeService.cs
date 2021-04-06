using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toll.Calculator.DAL;
using Toll.Calculator.Domain;

namespace Toll.Calculator.Service
{
    public class TollFeeService : ITollFeeService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public TollFeeService(
            IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<decimal> GetTotalFee(Vehicle vehicleType, List<DateTime> passageDates)
        {
            if (_vehicleRepository.GetTollFreeVehicles().Contains(vehicleType))
                return 0;

            decimal totalFee = 0;

            throw new NotImplementedException();
        }
    }
}
