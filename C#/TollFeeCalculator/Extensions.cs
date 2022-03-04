using Nager.Date;
using Nager.Date.Extensions;
using System;
using System.Linq;

namespace TollFeeCalculator
{
    public static class Extensions
    {
        public static bool IsTypeOfAnEnum<TEnum>(this string value) where TEnum : struct
        {
            return Enum.TryParse<TEnum>(value, out var _);
        }

        public static bool IsWeekendOrHoliday(this DateTime dateTime)
        {
            return dateTime.IsWeekend(Nager.Date.CountryCode.SE) || DateSystem.IsPublicHoliday(dateTime, CountryCode.SE);
        }

        public static bool IsTollFreeVehicle(this Vehicle vehicle)
        {
            if (vehicle == null) return false;
            var vehicleType = vehicle.GetVehicleType();
            return vehicleType.IsTypeOfAnEnum<TollFreeVehicles>();
        }

        public static int GetFeeOfSpecificTime(this TimeSpan timeSpan) => FeeRangeCollection.Range
                .FirstOrDefault(x => timeSpan >= x.From && timeSpan <= x.To)?.Fee ?? 0;
    }
}
