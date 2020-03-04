using System;
using System.Globalization;
using System.Linq;
using TollFeeCalculator;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Services;

public class TollCalculator : ITollCalculator
{
    private readonly ITollFreeVehicles _tollFreeVehicles;
    private readonly ITollFreeDates _tollFreeDates;
    private readonly IDailyTollFees _dailyTollFees;

    public TollCalculator()
    {
        _tollFreeVehicles = new TollFreeVehicles();
    }

    public TollCalculator(ITollFreeVehicles tollFreeVehicles, ITollFreeDates tollFreeDates, IDailyTollFees dailyTollFees)
    {
        _tollFreeVehicles = tollFreeVehicles;
        _tollFreeDates = tollFreeDates;
        _dailyTollFees = dailyTollFees;
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public decimal GetDailyTollFee(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        decimal totalFee = 0;
        foreach (DateTime date in dates)
        {
            decimal nextFee = GetTollFee(date, vehicle);
            decimal tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies/1000/60;

            if (minutes <= 60)
            {
                // TODO is there a bug here?
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


    public decimal GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (_tollFreeDates.IsTollFreeDate(date) || _tollFreeVehicles.IsTollFreeVehicle(vehicle)) return 0;

        return _dailyTollFees.GetRates()
            .LastOrDefault(x => x.Key <= date.TimeOfDay).Value;

    }

   
}