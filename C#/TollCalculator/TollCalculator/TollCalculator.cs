using System;
using System.Globalization;
using System.Linq;
using TollFeeCalculator;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Services;

public class TollCalculator : ITollCalculator
{
    private readonly decimal _maxPerDay;
    private const double _chargingIntervalInMinutes = 60;
    private readonly ITollFreeVehicles _tollFreeVehicles;
    private readonly ITollFreeDates _tollFreeDates;
    private readonly IDailyTollFees _dailyTollFees;

    public TollCalculator()
    {
        _tollFreeVehicles = new TollFreeVehicles();
        _tollFreeDates = new TollFreeDates();
        _dailyTollFees = new DailyTollFees();
    }

    public TollCalculator(ITollFreeVehicles tollFreeVehicles, ITollFreeDates tollFreeDates, IDailyTollFees dailyTollFees, decimal? maxPerDay)
    {
        _maxPerDay = maxPerDay ?? 60M;
        _tollFreeVehicles = tollFreeVehicles ?? new TollFreeVehicles();
        _tollFreeDates = tollFreeDates ?? new TollFreeDates();
        _dailyTollFees = dailyTollFees ?? new DailyTollFees();
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
        if (vehicle == null) throw new ArgumentException($"No Vehicle type provided for {nameof(vehicle)}");
        if (dates.Length == 0) throw new ArgumentException(nameof(dates));

        if (_tollFreeVehicles.IsTollFreeVehicle((vehicle))) return 0M;

        DateTime intervalStart = dates[0];
        DateTime previousDate = dates[0];

        decimal totalFee = 0;
        decimal totalFeePerDay = 0;
   
        foreach (DateTime date in dates)
        {
            if (previousDate.Date.CompareTo(date.Date) < 0)
            {
                totalFee += totalFeePerDay;
                totalFeePerDay = 0;
                previousDate = date;
            }

            decimal nextFee = GetTollFee(date, vehicle);
            decimal tempFee = GetTollFee(intervalStart, vehicle);

            TimeSpan span = date - intervalStart;
            double minutes = span.TotalMinutes;

            if (minutes <= _chargingIntervalInMinutes)
            {
                // TODO is there a bug here?
                if (totalFeePerDay > 0) totalFeePerDay -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFeePerDay += tempFee;
            }
            else
            {
                totalFeePerDay += nextFee;
                intervalStart = date;
            }

            if (totalFeePerDay > _maxPerDay) totalFeePerDay = _maxPerDay;
        }

        return totalFee + totalFeePerDay;
    }


    public decimal GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (_tollFreeDates.IsTollFreeDate(date) || _tollFreeVehicles.IsTollFreeVehicle(vehicle)) return 0;

        return _dailyTollFees.GetRates()
            .LastOrDefault(x => x.Key <= date.TimeOfDay).Value;

    }

   
}