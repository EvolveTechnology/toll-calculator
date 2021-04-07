using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toll.Calculator.DAL.Interface;
using Toll.Calculator.Domain;

namespace Toll.Calculator.Service
{
    public class TollFeeService : ITollFeeService
    {
        private readonly ITollFeeRepository _tollFeeRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public TollFeeService(
            IVehicleRepository vehicleRepository,
            ITollFeeRepository tollFeeRepository)
        {
            _vehicleRepository = vehicleRepository;
            _tollFeeRepository = tollFeeRepository;
        }

        public async Task<decimal> GetTotalFee(Vehicle vehicleType, List<DateTime> passageDates)
        {
            var tollFreeVehicles = await _vehicleRepository.GetTollFreeVehicles();

            if (tollFreeVehicles.Contains(vehicleType) ||
                !passageDates.Any())
                return 0;

            decimal totalFee = 0;

            var distinctDates = passageDates.GroupBy(x => x.ToString("yyyyMMdd")).Select(y => y.First()).ToList();

            foreach (var distinctDate in distinctDates)
            {
                if (await _tollFeeRepository.IsTollFreeDate(distinctDate))
                    continue;

                totalFee += await GetTotalFeeForDay(vehicleType,
                    passageDates.Where(p => p.Date == distinctDate.Date).ToList());
            }

            return totalFee;
        }

        private async Task<decimal> GetTotalFeeForDay(Vehicle vehicleType, List<DateTime> passageDates)
        {
            passageDates.Sort((a, b) => a.CompareTo(b));

            var intervalStart = passageDates.First();
            var intervalHighestFee = await _tollFeeRepository.GetPassageFeeByTime(intervalStart);
            decimal totalFee = 0;

            foreach (var passageDate in passageDates)
            {
                var passageFee = await _tollFeeRepository.GetPassageFeeByTime(passageDate);

                var diff = passageDate - intervalStart;
                var minutes = diff.TotalMinutes;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= intervalHighestFee;
                    if (passageFee >= intervalHighestFee) intervalHighestFee = passageFee;
                    totalFee += intervalHighestFee;
                }
                else
                {
                    totalFee += passageFee;
                    intervalStart = passageDate;
                    intervalHighestFee = passageFee;
                }
            }

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }
    }
}