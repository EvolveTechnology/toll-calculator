using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{
    public interface ITollCalculator
    {
        /// <summary>
        /// Calculates the daily passage fee for one vehicle
        /// </summary>
        /// <param name="vehicle">The vehicle</param>
        /// <param name="dates">The passages during one day. All items must be from the same day AND SORTED</param>
        /// <returns>The fee for the given day and vehicle</returns>
        decimal GetTollFee(IVehicle vehicle, IList<DateTime> dates);
    }

    public class TollCalculator : ITollCalculator
    {
        private readonly ITollFeeService _tollFeeService;

        public TollCalculator(ITollFeeService tollFeeService)
        {
            _tollFeeService = tollFeeService;
        }

        public decimal GetTollFee(IVehicle vehicle, IList<DateTime> dates)
        {
            if (!dates.Any())
                return 0;  // fail fast

            var timeTable = _tollFeeService.GetFeeTimeIntervals(vehicle.VehicleType, dates.First());
            if (!timeTable.Any())
                return 0;  // free ride for this vehicle type and/or date

            decimal sum = 0;
            var pendingFeeAndTime = default(TollFeeByTime); // begin with a bogus free passage a midnight

            foreach (var date in dates)
            {
                var minutesSinceMidnight = date.Hour * 60 + date.Minute;
                var fee = findFeeByTime(minutesSinceMidnight, timeTable);

                if (minutesSinceMidnight - pendingFeeAndTime.MinutesSinceMidnight >= _tollFeeService.FreeTimeSlotPassageInMinutes)
                {
                    sum += pendingFeeAndTime.Fee;
                    pendingFeeAndTime = new TollFeeByTime {Hour = date.Hour, Minute = date.Minute, Fee = fee};
                }
                else // passage was close to the previous - figure out max fee instead of adding
                    pendingFeeAndTime.Fee = Math.Max(pendingFeeAndTime.Fee, fee);
            }

            return Math.Min(sum + pendingFeeAndTime.Fee, _tollFeeService.MaximumFeeForOneDay);
        }

        private static decimal findFeeByTime(int minutesSinceMidnight, List<TollFeeByTime> timeTable)
        {
            // find the index of the slot AFTER the one we're looking for
            var index = timeTable.FindIndex(_ => _.MinutesSinceMidnight > minutesSinceMidnight);
            switch (index)
            {
                case -1: // we searched for a larger time that any given. use last time slot
                    return timeTable.Last().Fee;
                case 0: // before first given time. then it's free
                    return 0;
                default:
                    return timeTable[index - 1].Fee;
            }
        }

    }

}