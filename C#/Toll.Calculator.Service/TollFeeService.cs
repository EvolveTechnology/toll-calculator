using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nager.Date;
using Toll.Calculator.DAL.Interface;
using Toll.Calculator.Domain;

namespace Toll.Calculator.Service
{
    public class TollFeeService : ITollFeeService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITollFeeRepository _tollFeeRepository;

        public TollFeeService(
            IVehicleRepository vehicleRepository,
            ITollFeeRepository tollFeeRepository)
        {
            _vehicleRepository = vehicleRepository;
            _tollFeeRepository = tollFeeRepository;
        }

        public async Task<decimal> GetTotalFee(Vehicle vehicleType, List<DateTime> passageDates)
        {
            passageDates.Sort((a, b) => a.CompareTo(b));

            if (_vehicleRepository.GetTollFreeVehicles().Contains(vehicleType))
                return 0;

            var intervalStart = passageDates.First();
            decimal totalFee = 0;

            foreach (var passageDate in passageDates)
            {
                if (_tollFeeRepository.IsTollFreeDate(passageDate))
                    continue;

                var passageFee = _tollFeeRepository.GetPassageFeeByTime(passageDate).Fee;
                var intervalFee = _tollFeeRepository.GetPassageFeeByTime(intervalStart).Fee;

                //Fixa en riktig check i diff, denna checkar bara millisekunder under samma timme
                var diff = passageDate - intervalStart;
                var minutes = diff.TotalMinutes;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= intervalFee;
                    if (passageFee >= intervalFee) intervalFee = passageFee;
                    totalFee += intervalFee;
                }
                else
                {
                    totalFee += passageFee;

                    intervalStart = passageDate;
                }
            }

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }
    }
}
