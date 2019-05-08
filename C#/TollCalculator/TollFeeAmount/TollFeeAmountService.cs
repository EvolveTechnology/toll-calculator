using System;
using TollFeeCalculator.TollFeeTime;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator.TollFeeAmount
{
    public class TollFeeAmountService : ITollFeeAmountService
    {
        private readonly ITollFeeTimeService _tollFeeTimeService;

        public TollFeeAmountService(ITollFeeTimeService tollFeeTimeService)
        {
            _tollFeeTimeService = tollFeeTimeService;
        }
        public int GetTollFeeAmount(DateTime date, IVehicle vehicle)
        {
            if (_tollFeeTimeService.IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
                return 0;

            var feeTime = _tollFeeTimeService.GetFeeTime(date.TimeOfDay);
            return feeTime.Amount;
        }
        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            return vehicle != null && Enum.IsDefined(typeof(TollFreeVehicles), vehicle.GetVehicleType());
        }
     
    }
}
