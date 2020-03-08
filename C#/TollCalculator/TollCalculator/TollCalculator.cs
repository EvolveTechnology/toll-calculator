using System;
using System.IO;
using System.Linq;
using TollFeeCalculator;
using TollFeeCalculator.Interfaces;
using TollFeeCalculator.Services;

public class TollCalculator : ITollCalculator
{
    private const double ChargingIntervalInMinutes = 60;
    private readonly decimal _maxPerDay;
    private readonly ITollFreeVehicles _tollFreeVehicles;
    private readonly ITollFreeDates _tollFreeDates;
    private readonly IDailyTollFees _dailyTollFees;


    public TollCalculator()
    {
        _maxPerDay = 60m;
        _tollFreeVehicles = new TollFreeVehicles();
        _tollFreeDates = new TollFreeDates();
        _dailyTollFees = new DailyTollFees();
    }

    // Inject any service to alter the default behaviour of the toll calculator.
    // To use the default service , pass null.
    public TollCalculator(ITollFreeVehicles tollFreeVehicles,
        ITollFreeDates tollFreeDates,
        IDailyTollFees dailyTollFees,
        decimal? maxPerDay)
    {
        _maxPerDay = maxPerDay ?? 60m;
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
        if (vehicle == null) throw new ArgumentNullException($"No Vehicle type provided for {nameof(vehicle)}");

        if (dates != null && dates.GroupBy(x => x.Date).Count() > 1)
            throw new ArgumentException(
                $"Includes date pass values of two or more days. Only date passes within one day is allowed");

        // assumption :  when dates parameter is null the fee is zero.
        if ((dates == null || dates.Length == 0) || _tollFreeVehicles.IsTollFreeVehicle((vehicle))) return 0M;

        DateTime intervalStart = dates[0];

        decimal totalFeePerDay = 0;

        foreach (DateTime date in dates)
        {
            decimal nextFee = GetTollFee(date, vehicle);
            decimal tempFee = GetTollFee(intervalStart, vehicle);

            TimeSpan span = date - intervalStart;
            double minutes = span.TotalMinutes;

            if (minutes <= ChargingIntervalInMinutes)
            {
                if (totalFeePerDay > 0) totalFeePerDay -= tempFee;
                if (nextFee >= tempFee) tempFee = nextFee;
                totalFeePerDay += tempFee;
            }
            else
            {
                totalFeePerDay += nextFee;
                // setting the interval start to current date since one hour has passed
                intervalStart = date;
            }

            if (totalFeePerDay > _maxPerDay) totalFeePerDay = _maxPerDay;
        }

        return totalFeePerDay;
    }

    private decimal GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (_tollFreeDates.IsTollFreeDate(date) || _tollFreeVehicles.IsTollFreeVehicle(vehicle)) return 0;

        return _dailyTollFees.GetRates()
            .LastOrDefault(x => x.Key <= date.TimeOfDay).Value;

    }
    
}