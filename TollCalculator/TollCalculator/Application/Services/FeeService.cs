using System;
using System.Collections.Generic;
using System.Linq;
using Nager.Date;
using TollCalculator.Application.Models;

namespace TollCalculator.Application.Services
{
    public partial class FeeService
    {
        private readonly VehicleType[] _tollFreeVehicleTypes = {
            VehicleType.Motorbike,
            VehicleType.Diplomat,
            VehicleType.Emergency,
            VehicleType.Foreign,
            VehicleType.Military,
            VehicleType.Tractor
        };

        private readonly List<FeeTimeSpan> _fees = new List<FeeTimeSpan>
        {
            new FeeTimeSpan(new TimeSpan(6,0,0), new TimeSpan(6,29,59), 8),
            new FeeTimeSpan(new TimeSpan(6,30,0), new TimeSpan(6,59,59), 13),
            new FeeTimeSpan(new TimeSpan(7,0,0), new TimeSpan(7,59,59), 18),
            new FeeTimeSpan(new TimeSpan(8,0,0), new TimeSpan(8,29,59), 13),
            new FeeTimeSpan(new TimeSpan(8,30,0), new TimeSpan(14,59,59), 8),
            new FeeTimeSpan(new TimeSpan(15,0,0), new TimeSpan(15,29,59), 13),
            new FeeTimeSpan(new TimeSpan(15,30,0), new TimeSpan(16,59,59), 18),
            new FeeTimeSpan(new TimeSpan(17,0,0), new TimeSpan(17,59,59), 13),
            new FeeTimeSpan(new TimeSpan(18,0,0), new TimeSpan(18,29,59), 8),
            new FeeTimeSpan(new TimeSpan(18,30,0), new TimeSpan(5,59,59), 0),
        };

        public bool IsTollFreeVehicle(VehicleType vehicleType)
        {
            return _tollFreeVehicleTypes.Contains(vehicleType);
        }

        public bool IsTollFreeDay(DateTime entryDate)
        {
            var publicHolidays = DateSystem.GetPublicHoliday(DateTime.Now.Year, CountryCode.SE);

            if (publicHolidays.Any(publicHoliday => entryDate.Date == publicHoliday.Date))
            {
                return true;
            }

            return entryDate.DayOfWeek == DayOfWeek.Saturday || entryDate.DayOfWeek == DayOfWeek.Sunday;
        }

        public int CalculateDailySum(List<DateTime> feeDates)
        {
            var tollForEachPassage = CalculateTollForEachPassage(feeDates);
            
            var sum = tollForEachPassage
                .GroupBy(x => new {x.Date.Date, x.Date.Hour})
                .Sum(x=>x.Max(y => y.Fee));

            return sum > 60 ? 60 : sum;
        }

        private IEnumerable<HourlyToll> CalculateTollForEachPassage(IEnumerable<DateTime> feeDates)
        {
            var tollForEachPassage = new List<HourlyToll>();
            foreach (var feeDate in feeDates)
            {
                tollForEachPassage.AddRange(
                    _fees.Where(feeTimeSpan => feeTimeSpan.Start <= feeDate.TimeOfDay && feeDate.TimeOfDay <= feeTimeSpan.End)
                        .Select(feeTimeSpan => new HourlyToll {Date = feeDate, Fee = feeTimeSpan.Fee}));
            }

            return tollForEachPassage;
        }
    }
}
