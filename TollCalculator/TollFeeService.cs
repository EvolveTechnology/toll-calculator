using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
    public struct TollFeeByTime
    {
        public int Hour;
        public int Minute;
        public decimal Fee;
        public int MinutesSinceMidnight => Hour * 60 + Minute;
    }

    public interface ITollFeeService
    {
        List<TollFeeByTime> GetFeeTimeIntervals(VehicleType vehicleType, DateTime date);
        int FreeTimeSlotPassageInMinutes { get; }
        decimal MaximumFeeForOneDay { get; }
    }

    public class BasicTollFeeService : ITollFeeService
    {
        public readonly Dictionary<VehicleType, List<TollFeeByTime>> FeesByVehicleTypeAndTime = new Dictionary<VehicleType, List<TollFeeByTime>>();
        public readonly List<DateTime> RedDates = new List<DateTime>();

        public List<TollFeeByTime> GetFeeTimeIntervals(VehicleType vehicleType, DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || RedDates.Contains(date.Date)) 
                return new List<TollFeeByTime>();  // free passage today
            return FeesByVehicleTypeAndTime.TryGetValue(vehicleType, out var list)
                ? list
                : new List<TollFeeByTime>();  // free passage for this vehicle type
        }

        public int FreeTimeSlotPassageInMinutes { get; protected set; }
        public decimal MaximumFeeForOneDay {get; protected set; }
    }

}
