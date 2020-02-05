using System;
using TollCalculator.Lib.Models;

namespace TollCalculator.Lib
{
    public class TollRules
    {
        public const int FeeMin = 8;
        public const int FeeMax = 18;
        public const int DefaultFee = 8;
        public const int DailyFeeMax = 60;
        
        public static VehicleType[] TollVehicleTypes { get; } =
        {
            VehicleType.Car
        };
        
        public static DayOfWeek[] TollFreeDays { get; } =
        {
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };

        public static RushHourFee[] RushHourFees { get; } =
        {
            new RushHourFee(new TimeOfDay(6, 30), new TimeOfDay(6, 59), 13),
            new RushHourFee(new TimeOfDay(7, 0), new TimeOfDay(7, 59), 18),
            new RushHourFee(new TimeOfDay(8, 0), new TimeOfDay(8, 29), 13),
            new RushHourFee(new TimeOfDay(15, 0), new TimeOfDay(15, 29), 13),
            new RushHourFee(new TimeOfDay(15, 30), new TimeOfDay(16, 59), 18),
            new RushHourFee(new TimeOfDay(17, 0), new TimeOfDay(17, 59), 13),
        };
    }
}