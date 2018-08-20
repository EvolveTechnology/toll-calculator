using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator;

namespace ConsoleApp1
{
    /// <summary>
    /// This class is meant to simulate a database or other storage medium that enables settings and configurations to be changed without the need to release a new build of the code.
    /// 
    /// For actual production systems an actual database should be created and a database-interface should be created.
    /// </summary>
    public class DummyDatabase
    {
        /// <summary>
        /// Get a list of all vehicle types that are exempt from the toll-fee.
        /// </summary>
        /// <returns>List of toll exempt vehicle-types.</returns>
        public static List<VehicleType> GetTollFreeVehicleTypes()
        {
            return new List<VehicleType>
            {
                new VehicleType(2, "Motorbike"),
                new VehicleType(3, "Tractor"),
                new VehicleType(4, "Emergency"),
                new VehicleType(5, "Diplomat"),
                new VehicleType(6, "Foreign"),
                new VehicleType(7, "Military")
            };
        }

        /// <summary>
        /// Get the cost for a passage on a given time of day on a Non-free date and vehicle.
        /// </summary>
        /// <param name="hour">Hour of passage (24-hour format)</param>
        /// /// <param name="minute">Minute of passage</param>
        /// /// <param name="second">Second of passage</param>
        /// <returns>The price of the passage.</returns>
        public static double GetPriceOfPassageOnTime(int hour, int minute, int second)
        {
            TimeSpan time = new TimeSpan(hour, minute, second);

            if (time >= new TimeSpan(6, 0, 0) && time < new TimeSpan(6, 29, 59))
            {
                return 8;
            }
            else if (time >= new TimeSpan(6, 30, 0) && time < new TimeSpan(6, 59, 59))
            {
                return 13;
            }
            else if (time >= new TimeSpan(7, 0, 0) && time < new TimeSpan(7, 59, 59))
            {
                return 18;
            }
            else if (time >= new TimeSpan(8, 0, 0) && time < new TimeSpan(8, 29, 59))
            {
                return 13;
            }
            else if (time >= new TimeSpan(8, 30, 0) && time < new TimeSpan(14, 59, 59))
            {
                return 8;
            }
            else if (time >= new TimeSpan(15, 0, 0) && time < new TimeSpan(15, 29, 59))
            {
                return 13;
            }
            else if (time >= new TimeSpan(15, 30, 0) && time < new TimeSpan(16, 59, 59))
            {
                return 18;
            }
            else if (time >= new TimeSpan(17, 0, 0) && time < new TimeSpan(17, 59, 59))
            {
                return 13;
            }
            else if (time >= new TimeSpan(18, 0, 0) && time < new TimeSpan(18, 29, 59))
            {
                return 8;
            }
            else
            {
                return 0;
            }
        }

        public static bool DateIsTollFreeDate (int year, int month, int day)
        {
            // Static holidays
            if((month == 1 && day == 1) || //New year day
                (month == 11 && day == 1) || //All Saints' Day
                (month == 7) || // July is toll-free
                (month == 12 && (day == 24 || day == 25 || day == 26 || day == 31)) // Christmas and new years eve
                )
            {
                return true;
            }

            // Holidays are some of the worst things a programmer can encounter. There is no simple way around them, either write them down manually or create (or find) a program that handles them.
            if (year == 2013)
            {
                if (month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21))
                {
                    return true;
                }
            }
            return false;
        }

        public static double GetMaximumCostPerDay()
        {
            return 60;
        }

        internal static double GetGracePeriodMinutes()
        {
            return 60;
        }
    }
}
