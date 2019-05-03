using System;
using System.Linq;
using TollFeeCalculator.TollFeeTime;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator.TollFeeAmount
{
    public class TollFeeAmountService : ITollFeeAmountService
    {
        public int GetTollFeeAmount(DateTime date, IVehicle vehicle)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

            return GetTollFeeTime(date.TimeOfDay);
        }
        private bool IsTollFreeVehicle(IVehicle vehicle)
        {
            return vehicle != null && Enum.IsDefined(typeof(TollFreeVehicles), vehicle.GetVehicleType());
        }

        private int GetTollFeeTime(TimeSpan date)
        {
            var feeTimes = new TollFeeTimeService().GetTollFeeTimes();
            var result = feeTimes.FirstOrDefault(x => x.Start <= date && x.End >= date)?.Amount;
            return result ?? 0;
        }
        private bool IsTollFreeDate(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year != 2013) return false;
            return month == 1 && day == 1 ||
                   month == 3 && (day == 28 || day == 29) ||
                   month == 4 && (day == 1 || day == 30) ||
                   month == 5 && (day == 1 || day == 8 || day == 9) ||
                   month == 6 && (day == 5 || day == 6 || day == 21) ||
                   month == 7 ||
                   month == 11 && day == 1 ||
                   month == 12 && (day == 24 || day == 25 || day == 26 || day == 31);
        }
    }
}
