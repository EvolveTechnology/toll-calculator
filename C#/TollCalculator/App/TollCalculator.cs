using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculatorApp;
using TollCalculatorApp.Services.Interfaces;

namespace TollFeeCalculator
{
    public class TollCalculator
    {

        private readonly IHolidayService _holidayService;

        public TollCalculator(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        const int MaxTollFee = 60;
        private readonly List<VehicleType> _tollFreeVehicles = new List<VehicleType>
        {
            VehicleType.Motorbike,
            VehicleType.Tractor,
            VehicleType.Emergency,
            VehicleType.Diplomat,
            VehicleType.Foreign,
            VehicleType.Military
        };

        /**
        * Calculate the total toll fee for one day
        *
        * @param vehicle - the vehicle
        * @param dates   - date and time of all passes on one day
        * @return - the total toll fee for that day
        */
        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            var sortedDate = dates.OrderBy(date => date.Ticks).ToList();
            DateTime intervalStart = sortedDate[0];
            int totalFee = 0;
            foreach (DateTime date in sortedDate)
            {
                int nextFee = GetTollFee(date, vehicle);
                int tempFee = GetTollFee(intervalStart, vehicle);

                TimeSpan diffBetweenDates = date - intervalStart;
                if (diffBetweenDates.TotalMinutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > MaxTollFee) 
                return MaxTollFee;

            return totalFee;
        }
        public int GetTollFee(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeVehicle(vehicle) || IsTollFreeDate(date))
                return 0;

            
            if (date.IsBetweenTimes(TimeSpan.Parse("06:00"), TimeSpan.Parse("06:30"))) return 8;
            if (date.IsBetweenTimes(TimeSpan.Parse("06:30"), TimeSpan.Parse("07:00"))) return 13;
            if (date.IsBetweenTimes(TimeSpan.Parse("07:00"), TimeSpan.Parse("08:00"))) return 18;
            if (date.IsBetweenTimes(TimeSpan.Parse("08:00"), TimeSpan.Parse("08:30"))) return 13;

            //The following condition was translated from previous code - I think it might have contained an error
            // else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            // would have been translated to x:30 - x:59 where x would be the hours 8 to 14
            // leaving x:00 - x.29 toll free for each of the hours between 8-14. 
            if (date.IsBetweenTimes(TimeSpan.Parse("08:30"), TimeSpan.Parse("15:00"))) return 8;
            
            if (date.IsBetweenTimes(TimeSpan.Parse("15:00"), TimeSpan.Parse("15:30"))) return 13;
            if (date.IsBetweenTimes(TimeSpan.Parse("15:30"), TimeSpan.Parse("17:00"))) return 18;
            if (date.IsBetweenTimes(TimeSpan.Parse("17:00"), TimeSpan.Parse("18:00"))) return 13;
            if (date.IsBetweenTimes(TimeSpan.Parse("18:00"), TimeSpan.Parse("18:30"))) return 8;
            
            return 0;
        }
        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            if (vehicle == null) return false;

            if (_tollFreeVehicles.Any(tollFreeVehicle => tollFreeVehicle == vehicle.GetVehicleType()))
                return true;
            else
            {
                return false;
            }
        }
        private bool IsTollFreeDate(DateTime date)
        {
            if (date.IsWeekend() || _holidayService.IsHoliday(date))
            {
                return true;
            }

            return false;
        }
    }
}