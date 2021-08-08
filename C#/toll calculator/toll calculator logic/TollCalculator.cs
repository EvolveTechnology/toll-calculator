using System;
using static toll_calculator_logic.Enums;


namespace toll_calculator_logic
{
    public class TollCalculator
    {

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;

            foreach (DateTime date in dates)
            {
                var nextFee = GetTollFee(date, vehicle);
                var tempFee = GetTollFee(intervalStart, vehicle);

                var diffInMillies = (date - intervalStart).TotalMinutes;

                if (diffInMillies <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                    intervalStart = date;
                }
            }

            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }    

        public int GetTollFee(DateTime date, IVehicle vehicle) =>
            (IsWeekDay(date), IsTollFreeVehicle(vehicle), IsTollFreeDate(date), GetTimeRange(date)) switch
            {
                (false, _, _, _) => 0,
                (true, true, _, _) => 0,
                (true, false, true, _) => 0,
                (true, false, false, TimeRange.free) => 0,
                (true, false, false, TimeRange.Low) => 8,
                (true, false, false, TimeRange.Normal) => 13,
                (true, false, false, TimeRange.Heavy) => 18,

                _ => 0,
            };

        private static Boolean IsWeekDay(DateTime date) =>
            date.DayOfWeek switch
            {
                DayOfWeek.Saturday => false,
                DayOfWeek.Sunday => false,

                _ => true,
            };

        private bool IsTollFreeVehicle(IVehicle vehicle) =>
            vehicle switch
            {
                Car _ => false,

                _ => true,
            };


        
        private Boolean IsTollFreeDate(DateTime date) =>
            (date.Year, date.Month, date.Day) switch
            {
                (2013, 1, 1) => true,
                (2013, 3, 28) => true,
                (2013, 3, 29) => true,
                (2013, 4, 1) => true,
                (2013, 4, 30) => true,
                (2013, 5, 1) => true,
                (2013, 5, 8) => true,
                (2013, 5, 9) => true,
                (2013, 6, 5) => true,
                (2013, 6, 6) => true,
                (2013, 6, 21) => true,
                (2013, 7, _) => true,
                (2013, 11, 1) => true,
                (2013, 12, 24) => true,
                (2013, 12, 25) => true,
                (2013, 12, 26) => true,
                (2013, 12, 31) => true,

                _ => false,
            };

        
        private static TimeRange GetTimeRange(DateTime date) =>
            date.TimeOfDay switch
            {
                TimeSpan d when d >= new TimeSpan(6, 0, 0) && d < new TimeSpan(6, 30, 0) => TimeRange.Low,
                TimeSpan d when d >= new TimeSpan(6, 30, 0) && d < new TimeSpan(7, 0, 0) => TimeRange.Normal,
                TimeSpan d when d >= new TimeSpan(7, 0, 0) && d < new TimeSpan(8, 0, 0) => TimeRange.Heavy,

                TimeSpan d when d >= new TimeSpan(8, 0, 0) && d < new TimeSpan(8, 30, 0) => TimeRange.Normal,
                TimeSpan d when d >= new TimeSpan(8, 30, 0) && d < new TimeSpan(15, 0, 0) => TimeRange.Low,
                TimeSpan d when d >= new TimeSpan(15, 0, 0) && d < new TimeSpan(15, 30, 0) => TimeRange.Normal,

                TimeSpan d when d >= new TimeSpan(15, 30, 0) && d < new TimeSpan(17, 0, 0) => TimeRange.Heavy,
                TimeSpan d when d >= new TimeSpan(17, 0, 0) && d < new TimeSpan(18, 0, 0) => TimeRange.Normal,
                TimeSpan d when d >= new TimeSpan(18, 0, 0) && d < new TimeSpan(18, 30, 0) => TimeRange.Low,


                _ => TimeRange.free,
            };

    }
}
