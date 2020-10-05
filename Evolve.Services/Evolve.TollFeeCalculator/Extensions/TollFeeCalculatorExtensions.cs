using Evolve.TollFeeCalculator.Enums;
using Evolve.TollFeeCalculator.Interfaces;
using Evolve.TollFeeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.TollFeeCalculator.Extensions
{
    /// <summary>
    /// Extensions för att kontrollera kostnader specifik dag/bil
    /// </summary>
    public static class TollFeeCalculatorExtensions
    {

        /// <summary>
        /// kontrollera Kostnader free för specifik dag
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static async ValueTask<bool> IsTollFreeDateAsync(this DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            int hour = date.Hour;
            DayOfWeek dayOfWeek = date.DayOfWeek;
            if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday) return true;

            if (year == Globals.AppConfiguration.FreeDays.Year)
            {
                if (month == (int)Months.JANUARY && Globals.AppConfiguration.FreeDays.JANUARY.Where(p => p == day).Any() ||
                   month == (int)Months.MARCH && Globals.AppConfiguration.FreeDays.MARCH.Where(p => p == day).Any() ||
                   month == (int)Months.APRIL && Globals.AppConfiguration.FreeDays.APRIL.Where(p => p == day).Any() ||
                   month == (int)Months.MAY && Globals.AppConfiguration.FreeDays.MAY.Where(p => p == day).Any() ||
                   month == (int)Months.JUNE && Globals.AppConfiguration.FreeDays.JUNE.Where(p => p == day).Any() ||
                   month == (int)Months.JULY || 
                   month == (int)Months.NOVEMBER && Globals.AppConfiguration.FreeDays.NOVEMBER.Where(p => p == day).Any() ||
                   month == (int)Months.DECEMBER && Globals.AppConfiguration.FreeDays.DECEMBER.Where(p => p == day).Any())                
                {
                    return await new ValueTask<bool>(true); 
                }
            }
            return await new ValueTask<bool>(false); ;

        }

        /// <summary>
        /// kontrollera skattefri fordon 
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        public static async ValueTask<bool> IsTollFreeVehicleAsync(this IVehicle vehicle) 
          {
            if (vehicle == null) return await new ValueTask<bool>(false);
            var vehicleType = (VehicleType)vehicle.GetVehicleType();
            return await new ValueTask<bool>(vehicleType.ToString().Equals(TollFreeVehicles.Motorbike.ToString()) ||
                   vehicleType.ToString().Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.ToString().Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.ToString().Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.ToString().Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.ToString().Equals(TollFreeVehicles.Military.ToString()));
          }
        /// <summary>
        ///  Räkna kostnad tid
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static async ValueTask<int> GetAmountOfTimeAsync(this CostTime o)
        {
            return await new ValueTask<int> ( o switch
            {
                CostTime t when t.Hour == 6 && t.Minute >= 0 && t.Minute <= 29 => Globals.AppConfiguration.FeeCostToTime.ZoneTime6a,
                CostTime t when t.Hour == 6 && t.Minute >= 30 && t.Minute <= 59 => Globals.AppConfiguration.FeeCostToTime.ZoneTime6a,
                CostTime t when t.Hour == 7 && t.Minute >= 0 && t.Minute <= 59 => Globals.AppConfiguration.FeeCostToTime.ZoneTime7,
                CostTime t when t.Hour == 8 && t.Minute >= 0 && t.Minute <= 29 => Globals.AppConfiguration.FeeCostToTime.ZoneTime8a,
                CostTime t when t.Hour >= 8 && t.Hour <= 14 && t.Minute >= 30 && t.Minute <= 59 => Globals.AppConfiguration.FeeCostToTime.ZoneTime8b,
                CostTime t when t.Hour == 15 && t.Minute >= 0 && t.Minute <= 29 => Globals.AppConfiguration.FeeCostToTime.ZoneTime15a,
                CostTime t when t.Hour == 15 && t.Minute >= 0 || t.Hour == 16 && t.Minute <= 59 => Globals.AppConfiguration.FeeCostToTime.ZoneTime15b,
                CostTime t when t.Hour == 17 && t.Minute >= 0 && t.Minute <= 59 => Globals.AppConfiguration.FeeCostToTime.ZoneTime17,
                CostTime t when t.Hour == 18 && t.Minute >= 0 && t.Minute <= 29 => Globals.AppConfiguration.FeeCostToTime.ZoneTime18,
                _ => Globals.AppConfiguration.FeeCostToTime.ZoneTimefree

            });
        }

    }
}
