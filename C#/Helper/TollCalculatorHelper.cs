using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TollCalculator.Data.Entities;

namespace TollCalculator.Helper
{
    public static class TollCalculatorHelper
    {
        private static readonly List<DateTime> FreeFeeDates = new List<DateTime>()
        {
            new DateTime(2020, 1, 1),
            new DateTime(2020, 1, 6),
            new DateTime(2020, 4, 10),
            new DateTime(2020, 4, 12),
            new DateTime(2020, 4, 13),
            new DateTime(2020, 5, 1),
            new DateTime(2020, 5, 21),
            new DateTime(2020, 5, 31),
            new DateTime(2020, 6, 6),
            new DateTime(2020, 6, 20),
            new DateTime(2020, 10, 31),
            new DateTime(2020, 12, 25),
            new DateTime(2020, 12, 26)
        };

        // We can replace the list of Tuple class with a list of objects derived from a class (IntervalStart, IntervalEnd, Fee) as properties
        private static readonly List<Tuple<TimeSpan, TimeSpan, decimal>> DurationFee = new List<Tuple<TimeSpan, TimeSpan, decimal>>()
        {
            // Tuple<IntervalStart, IntervalEnd, Fee>
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 0), 8),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0), 13),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0), 18),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0), 13),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0), 8),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0), 13),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(15, 0, 0), new TimeSpan(16, 59, 0), 18),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 0), 13),
            new Tuple<TimeSpan, TimeSpan, decimal>(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 0), 8)
        };

        public static bool IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday || FreeFeeDates.Contains(date.Date))
                return true;
            return false;
        }

        public static bool IsTollFreeVehicle(Vehicle vehicle)
        {
            switch (vehicle.Type)
            {
                case Data.Enums.VehiclesType.Motorbike:
                case Data.Enums.VehiclesType.Tractor:
                case Data.Enums.VehiclesType.Emergency:
                case Data.Enums.VehiclesType.Diplomat:
                case Data.Enums.VehiclesType.Foreign:
                case Data.Enums.VehiclesType.Military:
                    return true;
                case Data.Enums.VehiclesType.PrivateCar:
                case Data.Enums.VehiclesType.Truck:
                    return false;
                default:
                    return false;
            }
        }

        public static decimal GetDurationFee(DateTime dateTime)
        {
            var inputTimeSpan = new TimeSpan(dateTime.Hour, dateTime.Minute, 0);
            var durationFee = DurationFee.Where(t => inputTimeSpan >= t.Item1 && inputTimeSpan <= t.Item2).FirstOrDefault();
            if (durationFee != null)
                return durationFee.Item3;
            return 0;
        }
    }
}
