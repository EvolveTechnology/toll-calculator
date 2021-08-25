using System;
using System.Globalization;
using TollFeeCalculator;

public class TollCalculator
{
    private IHolidayService HolidayService { get; }

    public TollCalculator(IHolidayService holidayService)
    {
        this.HolidayService = holidayService ?? throw new ArgumentNullException(nameof(holidayService));
    }

    /// <summary>
    ///     Calculates the total toll fee for one day
    /// </summary>
    /// <param name="vehicle">The vehicle</param>
    /// <param name="dates">Date and time of all passes on one day</param>
    /// <returns>The total toll fee for that day</returns>
    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        if (dates is null || !dates.Any())
            return 0;

        dates = Array.Sort(dates); // In case they were not sorted already

        DateTime intervalStart = dates[0];
        int totalFee = 0;
        int intervalFee = 0;

        foreach (DateTime date in dates)
        {
            int fee = GetTollFee(date, vehicle);

            if (date > intervalStart.AddHours(1)) // New interval
            {
                intervalStart = date;
                intervalFee = 0;
            }

            totalFee += Math.Max(0, fee - intervalFee); // Charge only what has not yet been charged this hour

            if (totalFee >= 60)
                return 60;

            intervalFee = Math.Max(fee, intervalFee);
        }

        return totalFee;
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour <= 5) return 0;
        if (hour == 6 && minute <= 29) return 8;
        if (hour == 6) return 13;
        if (hour == 7) return 18;
        if (hour == 8 && minute <= 29) return 13;
        if (hour <= 14) return minute <= 29 ? 0 : 8;
        if (hour == 15 && minute <= 29) return 13;
        if (hour <= 16) return 18;
        if (hour == 17) return 13;
        if (hour == 18 && minute <= 29) return 8;
        return 0;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        var vehicleType = vehicle.GetVehicleType();
        return TollFreeVehicles.TryParse(vehicleType, out _);
    }

    private bool IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        var holidays = this.HolidayService.GetHolidays(date.Year);

        if (holidays.Any(holiday => holiday.Date == date.Date))
            return true;

        return false;
    }

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }

    public interface IHolidayService
    {
        public List<DateTime> GetHolidays(int year); // Maybe get from an API
    }
}