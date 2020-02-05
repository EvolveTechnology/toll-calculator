using System;
using System.Collections.Generic;
using System.Linq;

namespace TollCalculator.Lib
{ 
    public static class TollCalculator
    {

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */

        public static int GetTollFee(VehicleType vehicleType, DateTime[] dates)
        {
            DateTime intervalStart = dates[0];
            int totalFee = 0;
            foreach (DateTime date in dates)
            {
                int nextFee = GetTollFee(date, vehicleType);
                int tempFee = GetTollFee(intervalStart, vehicleType);

                long diffInMillies = date.Millisecond - intervalStart.Millisecond;
                long minutes = diffInMillies/1000/60;

                if (minutes <= 60)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            if (totalFee > 60) totalFee = 60;
            return totalFee;
        }

        private static bool IsTollFreeVehicle(VehicleType vehicleType)
        {
            return vehicleType != VehicleType.Car;
        }

        private static int GetTollFee(DateTime date, VehicleType vehicleType)
        {
            if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicleType)) return 0;

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

        private static Boolean IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            return IsPublicHoliday(date);
        }

        private static bool IsPublicHoliday(DateTime date)
        {
            var staticPublicHolidays = new []
            {
                new YearlyDate(1, 1), //Nyårsdagen
                new YearlyDate(1, 5), //Trettondagsafton
                new YearlyDate(1, 6), //Trettondedag jul
                new YearlyDate(4, 30), //Valborgsmässoafton
                new YearlyDate(5, 1), //Första maj
                new YearlyDate(6, 6), //Sveriges nationaldag
                new YearlyDate(12, 24), //Julafton
                new YearlyDate(12, 25), //Juldagen
                new YearlyDate(12, 26), //Annandag jul
                new YearlyDate(12, 31), //Nyårsafton
            };
            
            /*
             * NOTE (this is a TODO, that should normally go to a separate issue in the issue-tracker and not in the code):
             * There is an obvious mistake here, and this method is lacking dynamic public holidays,
             * e.g. Pingstdagen (sjunge söndagen efter påsk)
             *
             * To fix this either:
             * 1) Implement logic that can calculate the dynamic holidays for a year based on the formulas
             * available for Swedish public holidays (e.g. https://sv.wikipedia.org/wiki/Helgdagar_i_Sverige)
             * 2) Start using a third-party library that solves this, e.g. https://www.nuget.org/packages/Nager.Date/,
             * However, such a library should be vetted in the sense if the license complies with ours and if we can trust
             * the code or not.
             *
             * Once dynamic holdays are added to the code, the unit tests should also reflect test cases with dynamic holidays.
             */

            return staticPublicHolidays.Any(holiday => holiday.IsEqualToDate(date));
        }
    }
}