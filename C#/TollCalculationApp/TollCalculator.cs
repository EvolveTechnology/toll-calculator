using System;
using System.Collections.Generic;
using TollFeeCalculator;
using Nager.Date;

public static class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     * 
     * Note that Nager.Date runs under MIT license!
     */

    public static int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        List<int> fees = new List<int>();
        foreach( DateTime pass in dates)
        {
            fees.Add(GetTollFee(pass, vehicle));
        }
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Ticks - intervalStart.Ticks;
            double minutes = diffInMillies / 10000000 / 60;

            if (minutes < 60.0)
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


    public static int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || vehicle.IsTollFree) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        else if (hour == 7 ) return 18;
        else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 8 && minute >= 30 && minute <= 59) return 8;
        else if (hour > 8 && hour <= 14 ) return 8;
        else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        else if (hour == 15 && minute >= 30 && minute <= 59) return 18;
        else if (hour == 16 ) return 18;
        else if (hour == 17 ) return 13;
        else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        else return 0;
    }

    private static bool IsTollFreeDate(DateTime date)
    {
        // Free on weekends
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) 
            return true;

        // Use NuGet package Nager.Date to check if it is a holiday!
        if (DateSystem.IsPublicHoliday(date, CountryCode.SE))
            return true;
            
        // Otehrwise return false
        return false;
    }

}