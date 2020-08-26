using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculator.Application.Models;
using TollCalculator.Application.Services;

namespace TollCalculator
{
    public class TollCalculator
    {
        // In the requirements it says that for one day maximum fee is 60SEK
        // so initial assumption is that MVP is calculations for one day only
        public int GetTollFeePerDay(Vehicle vehicle, List<DateTime> tollEntryTimes)
        {
            // If this is a call to an API return BadRequest instead of throwing exception
            // Exception here is thrown for tests
            if (vehicle == null || tollEntryTimes == null || !tollEntryTimes.Any())
            {
                var parameterName = vehicle == null ? nameof(vehicle) : nameof(tollEntryTimes);
                throw new ArgumentNullException(parameterName, "One of the required parameters passed to this method is null");
            }
            // If this is a call to an API return Bad Request instead of throwing exception
            // Exception here is thrown for tests
            if (tollEntryTimes.GroupBy(x => x.Date).Count() > 1)
            {
                throw new ArgumentException("List of dates/times of entry provided to this method has more than one date");
            }

            var feeService = new FeeService();
            
            if (feeService.IsTollFreeVehicle(vehicle.VehicleType))
            {
                return 0;
            }

            var feeDates = tollEntryTimes.Where(x => !feeService.IsTollFreeDay(x)).ToList();
            if (!feeDates.Any())
            {
                return 0;
            }

            return feeService.CalculateDailySum(feeDates);
        }
    }
}
