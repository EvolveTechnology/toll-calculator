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
        if(IsTollFreeVehicle(vehicle))
        {
            return 0;
        }

        //Ordered dates with fee-less times removed, since those will be ignored for fee calculation.
        //From Specification: A vehicle should only be charged once an hour
        //Important to distinguish this from vehicle only being CHECKED once an hour.
        var OrderedDates = dates.
                            OrderBy(e => e).
                            Where(date => !IsTollFreeDate(date) && GetTollFee(date, vehicle) != 0 );
        
        if(OrderedDates.Count() < 1)
        {
            return 0;
        }

        //Tuple documentation: DateTime is previous date, int is accumulated toll fees, using the oldest date possible as seed value so all provided dates will be later.   
        //Aggregate all fees into a sum, ensuring only one billing at most per hour.
        return  OrderedDates.Aggregate(
            new Tuple<DateTime, int>(new DateTime(1, 1, 1), 0),
            (acc, curr) =>
                (curr < (acc.Item1 + new TimeSpan(1, 0, 0))) 
                ? new Tuple<DateTime, int>(curr, acc.Item2)
                : new Tuple<DateTime, int>(curr, acc.Item2 + GetTollFee(curr, vehicle))
            ).Item2;
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
