using System;
using TollFeeCalculator.Enums;
using TollFeeCalculator.Logic;

namespace TollFeeCalculator
{
    public class TollCalculator : ITollCalculator
    {
        /// <summary>
        /// Calculate the total toll fee for one day
        /// </summary>
        /// <param name="vehicle">the vehicle</param>
        /// <param name="date">date and time of all passes on one day</param>
        /// <returns>the total toll fee for that day</returns>
        public int GetTollFee(string vehicleType, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(vehicleType, date);
                int tempFee = GetTollFee(vehicleType, intervalStart);

                long dateInMillies = (date.Hour * 60 * 60 * 1000) + (date.Minute * 60 * 1000) + (date.Second * 1000) + date.Millisecond;
                long intervalInMillies = (intervalStart.Hour * 60 * 60 * 1000) + (intervalStart.Minute * 60 * 1000) 
                                                 + intervalStart.Second * 1000 + intervalStart.Millisecond;

                long diffInMillies = dateInMillies - intervalInMillies;
                long minutes = diffInMillies / 1000 / 60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) 
                    { 
                        totalFee -= tempFee; 
                    }
                    if (nextFee >= tempFee)
                    { 
                        tempFee = nextFee; 
                    }
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
                intervalStart = date;
            }
            if (totalFee > 60)
            { 
                totalFee = 60;
            }
            return totalFee;
        }

        /// <summary>
        /// Calculate the total toll fee for the one day
        /// </summary>
        /// <param name="vehicleType">the vehicle</param>
        /// <param name="date">date and time of the current pass</param>
        /// <returns>the toll fee</returns>
        public int GetTollFee(string vehicleType, DateTime date)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicleType)) 
            { 
                return 0;
            }

            int hour = date.Hour;
            int minute = date.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            else return 0;
        }

        private bool IsTollFreeVehicle(string vehicleType)
        {
            if (string.IsNullOrEmpty(vehicleType))
            { 
                return false; 
            }
            return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
                   vehicleType.Equals(TollFreeVehicles.Military.ToString());
        }

        private bool IsTollFreeDate(DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            { 
                return true; 
            }

            if (year == 2013)
            {
                if (month == 1 && day == 1 ||
                    month == 3 && (day == 28 || day == 29) ||
                    month == 4 && (day == 1 || day == 30) ||
                    month == 5 && (day == 1 || day == 8 || day == 9) ||
                    month == 6 && (day == 5 || day == 6 || day == 21) ||
                    month == 7 ||
                    month == 11 && day == 1 ||
                    month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
                {
                    return true;
                }
            }
            return false;
        }
    }
}