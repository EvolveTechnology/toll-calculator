using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TollCalculatorBlazorApp.Models;

namespace TollCalculatorBlazorApp.Services
{
    public interface ITollCalculatorService
    {
      int  CalculateTollDailyInvoice(DateTime[] tollDates);
    }

    public class TollCalculatorService : ITollCalculatorService
    {


        private static IVehicle _vehicle;

        public TollCalculatorService(IVehicle vehicle)
        {
            _vehicle = vehicle;

        }

        /// <summary>
        /// Identify weekend day of a givin date
        /// </summary>
        /// <param name="timeOfToll">a giving data to analyze </param>
        /// <returns>true if its a Saturday or a Sunday else its a weekday and the rexturn value is false</returns>

        private static bool IsWeekEnd(DateTime timeOfToll)
        {
            return timeOfToll.DayOfWeek switch
            {
                DayOfWeek.Saturday => true,
                DayOfWeek.Sunday => true,
                _ => false
            };
        }


        /// <summary>
        /// Identify if a specefic date is a free toll day , identification is built on a tuple pattern matching, 
        /// testing if its a weekend or an prespecifed (Month,Day) pattern
        /// </summary>
        /// <param name="timeOfToll">a giving data to analyze </param>
        /// <returns>true if its on of the free dates in the schema , else its false </returns>
        private static bool IsFreeDay(DateTime timeOfToll)
        {
            return (IsWeekEnd(timeOfToll), timeOfToll.Month, timeOfToll.Day) switch
            {
                (true, _, _) => true,
                (false, 1, 1) => true,
                (false, 3, 28) => true,
                (false, 3, 29) => true,
                (false, 4, 1) => true,
                (false, 4, 30) => true,
                (false, 6, 5) => true,
                (false, 6, 6) => true,
                (false, 6, 21) => true,
                (false, 7, _) => true,
                (false, 11, 1) => true,
                (false, 12, 24) => true,
                (false, 12, 25) => true,
                (false, 12, 26) => true,
                (false, 12, 31) => true,
                _ => false
            };

        }

        /// <summary>
        /// Identify if a specific vehicle is exempt from paying toll fees, identification is built on pattern matching for the vehicle type, 
        /// that is retrieved from the vehicle passed in the object constructor 
        /// </summary> 
        /// <returns>true if its one of the toll-free vehciles in the schema , else its false </returns>
        private static bool IsFeeFreeVehicle()
        {
            VehicleTypesEnum vehicleType = _vehicle.GetVehicleType();
            return vehicleType switch
            {
                VehicleTypesEnum.Car => false,
                VehicleTypesEnum.Diplomat => true,
                VehicleTypesEnum.Emergency => true,
                VehicleTypesEnum.Foreign => true,
                VehicleTypesEnum.Military => true,
                VehicleTypesEnum.Motorbike => true,
                _ => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicleType))
            };
        }

        /// <summary>
        /// Calculate toll hourly fee for a giving toll date time.
        /// calculation uses tuple pattern matching for the givin hour of the day , and a variable "firstHalf" to identify if its the first or the second 30 minutes of the hour .
        /// </summary>
        /// <param name="timeOfToll">a single toll date time </param>
        /// <returns>integer value of the toll fee in a specific time  </returns>
        private static int HourlyRate(DateTime timeOfToll)
        {
            int hour24 = int.Parse(timeOfToll.ToString("HH"));
            bool firstHalf = true;
            if (timeOfToll.Minute >= 30 && timeOfToll.Minute <= 59)
                firstHalf = false;

            return (hour24, firstHalf) switch
            {
                (6, true) => 8,
                (6, false) => 13,
                (7, _) => 18,
                (8, true) => 13,
                var (hour, fHalf) when hour >= 8 && hour <= 14 && !fHalf => 8,
                (15, true) => 13,
                (15, false) => 18,
                (16, _) => 18,
                (17, _) => 13,
                (18, true) => 8,
                _ => 0

            };

        }


        /// <summary>
        /// Calculate toll fee for the current vehicle for in a giving toll date time.       
        /// </summary>
        /// <param name="timeOfToll">a single toll tdate time </param>
        /// <returns>integer value of the toll fee in a specific time for the giving vehicle  </returns>

        //private static
            public int TollRate(DateTime timeOfToll)
        {
            VehicleTypesEnum vehicleType = _vehicle.GetVehicleType();
            return (IsFreeDay(timeOfToll), IsFeeFreeVehicle()) switch
            {
                (false, false) => HourlyRate(timeOfToll),
                _ => 0
            };

        }

        /// <summary>
        /// Calculate toll fee for the current vehicle for a givin day
        /// </summary>
        /// <param name="timesOfToll">an array of dates where toll times have been recorded for a givin vehicle, dates from different day are ignored from calculation</param>
        /// <returns>integer value of the toll fee in a day for the giving vehicle  </returns>

        public int CalculateTollDailyInvoice(DateTime[] timesOfToll)
        {
            VehicleTypesEnum vehicleType = _vehicle.GetVehicleType();

            if (timesOfToll is null || timesOfToll.Length == 0)
            {
                Console.WriteLine("Missing Toll Dates for calculation ");
                return -1;
            }

            Array.Sort(timesOfToll);

            if (IsFreeDay(timesOfToll[0]) && IsFeeFreeVehicle())
            {
                return 0;
            }


            int initRate = TollRate(timesOfToll[0]);
            DateTime intervalStart = timesOfToll[0];
            int tollRate = 0;
            TimeSpan tollTimesSpan;

            foreach (DateTime tollTime in timesOfToll)
            {
                if (tollRate > 60) return 60;
                // check if the times records are in the same day 
                if (tollTime.Date != intervalStart.Date)
                {
                    tollRate += initRate;
                    Console.WriteLine("Dates from additional day they will be ignored and the total fee will be for only day:" + timesOfToll[0].Year + "," + timesOfToll[0].Month + "," + timesOfToll[0].Day);

                    return Math.Min(60, tollRate);
                }

                tollTimesSpan = tollTime - intervalStart;
                if (tollTimesSpan.TotalHours < 1)
                {
                    initRate = Math.Max(initRate, TollRate(tollTime));

                }
                else
                {
                    tollRate += initRate;
                    initRate = TollRate(tollTime);
                    intervalStart = tollTime;

                }

            }

            tollRate += initRate;

            return Math.Min(60, tollRate);
        }

       



    }
}
