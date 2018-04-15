using Nager.Date;
using System;
using System.Collections.Generic;
using TollFeeCalculator;

public class TollCalculator
{
    private List<TollFeeTimePeriod> _timePeriods;

    public TollCalculator()
    {
        _timePeriods = new List<TollFeeTimePeriod>();
        CreateTollFeeTimePeriods();
    }

    /// <summary>
    /// Calculate the total toll fee for one day.
    /// </summary>
    /// <param name="vehicle">The Vehicle</param>
    /// <param name="dates">Date and time of all passes on one day</param>
    /// <returns>The total toll fee for that day</returns>
    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
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

    /// <summary>
    /// Get the toll fee for a specific date and vehicle.
    /// </summary>
    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int fee = 0;

        foreach (TollFeeTimePeriod period in _timePeriods)
        {
            if (period.SpansOver(date.Hour, date.Minute))
            {
                fee = period.TollFee;
                break;
            }
        }

        return fee;
    }

    private void CreateTollFeeTimePeriods()
    {
        _timePeriods.Add(new TollFeeTimePeriod(6, 0, 6, 29, 8)); // 06:00 - 06:29, 8 kr
        _timePeriods.Add(new TollFeeTimePeriod(6, 30, 6, 59, 13)); // 06:30 - 06:59, 13 kr
        _timePeriods.Add(new TollFeeTimePeriod(7, 0, 7, 59, 18)); // 07:00 - 07:59, 18 kr
        _timePeriods.Add(new TollFeeTimePeriod(8, 0, 8, 29, 13)); // 08:00 - 08:29, 13 kr
        _timePeriods.Add(new TollFeeTimePeriod(8, 30, 14, 59, 8)); // 08:30 - 14:59, 8 kr
        _timePeriods.Add(new TollFeeTimePeriod(15, 0, 15, 29, 13)); // 15:00 - 15:29, 13 kr
        _timePeriods.Add(new TollFeeTimePeriod(15, 30, 16, 59, 18)); // 15:30 - 16:59, 18 kr
        _timePeriods.Add(new TollFeeTimePeriod(17, 0, 17, 59, 13)); // 17:00 - 17:59, 13 kr
        _timePeriods.Add(new TollFeeTimePeriod(18, 0, 18, 29, 8)); // 18:00 - 18:29, 8 kr
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;

        return vehicle.IsTollFree();
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        if (date.DayOfWeek == DayOfWeek.Saturday 
         || date.DayOfWeek == DayOfWeek.Sunday 
         || DateSystem.IsPublicHoliday(date, CountryCode.SE)
         || date.Month == 7) // july is free
        {
            return true;
        }

        return false;
    }
}