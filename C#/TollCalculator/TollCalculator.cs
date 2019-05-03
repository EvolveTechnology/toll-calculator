using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.TollFeeAmount;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator
{

    public class TollCalculator
    {
        public TollCalculator(ITollFeeAmountService tollFeeAmountService)
        {
            _tollFeeAmountService = tollFeeAmountService;
        }
        private const int HOUR = 60;
        private const int MAX_FEE = 60;
        private readonly ITollFeeAmountService _tollFeeAmountService;

        public int GetTollFee(IVehicle vehicle, List<DateTime> dates)
        {
            if (!dates.Any()) return 0;
            var previousDate = dates[0];
            var totalFee = 0;
            foreach (var date in dates)
            {
                var currentFee = _tollFeeAmountService.GetTollFeeAmount(date, vehicle);
                var previousFee = _tollFeeAmountService.GetTollFeeAmount(previousDate, vehicle);

                var timeDiff = date.TimeOfDay - previousDate.TimeOfDay;
                var minutes = timeDiff.TotalSeconds / HOUR;

                if (minutes <= HOUR)
                {
                    if (totalFee > 0) totalFee -= previousFee;
                    if (previousFee >= currentFee) currentFee = previousFee;
                }
                else
                {
                    previousDate = date;

                }
                totalFee += currentFee;
            }

            if (totalFee > MAX_FEE) totalFee = MAX_FEE;
            return totalFee;
        }

    }
}