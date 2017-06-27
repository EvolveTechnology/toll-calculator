using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
//https://github.com/tinohager/Nager.Date
using Nager.Date;
using TollFeeCalculator;

public class TollCalculator
{

    /**
     * An array of all fee periods
    */

    //Still has too much duplication, could be optimised if I sit and think about it more.
    private List<TollFeePeriod> FeePeriods = new List<TollFeePeriod>( new TollFeePeriod[]
    {
        new TollFeePeriod(new TimeSpan(6,0,0), new TimeSpan(6,30,0), 8),
        new TollFeePeriod(new TimeSpan(6,30,0), new TimeSpan(7,0,0), 13),
        new TollFeePeriod(new TimeSpan(7,0,0), new TimeSpan(8,0,0), 18),
        new TollFeePeriod(new TimeSpan(8,0,0), new TimeSpan(8,30,0), 13),
        new TollFeePeriod(new TimeSpan(8,30,0), new TimeSpan(15,0,0), 8),
        new TollFeePeriod(new TimeSpan(15,0,0), new TimeSpan(15,30,0), 13),
        new TollFeePeriod(new TimeSpan(15,30,0), new TimeSpan(17,0,0), 18),
        new TollFeePeriod(new TimeSpan(17,0,0), new TimeSpan(18,0,0), 13),
        new TollFeePeriod(new TimeSpan(18,0,0), new TimeSpan(18,30,0), 13)
    });

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
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

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

    /**
    * Proxy method to IsFeeFree on Vehicles
    * @param vehicle The vehicle to be checked for fee exemptions
    * @return If the vehicle is exempt from fees.
    */
    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        return vehicle.IsFeeFree();
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {

        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
        //If no period exists, IE it is after the last billable period of the day, it will catch and return 0.
        try
        {
            var Period = FeePeriods.First(period => period.IsWithinPeriod(date));
            return Period.TollFee;
        }
        catch (InvalidOperationException)
        {
            return 0;
        }
    }

    /**
    * Determines if a given date is Toll Free
    * Depends on library from https://github.com/tinohager/Nager.Date
    * @param date The Date to check
    * @return true if the Date should result in no toll fees
    */
    private Boolean IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            return true;
        }
        return DateSystem.IsPublicHoliday(date, CountryCode.SE);
    }
}
