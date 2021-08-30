using System;
using System.Linq;
using Toll_calc.Extensions;
using Toll_calc.Models;
using Toll_calc.Services;

public class TollCalculator
{
    private readonly IHolidayService _holidayService;

    public TollCalculator(IHolidayService holidayService)
    {
        _holidayService = holidayService;
    }

    /**
    * Calculate the total toll fee for one day
    *
    * @param vehicle - the vehicle
    * @param passTimes   - date and time of all passes on one day
    * @return - the total toll fee for that day
    */
    public int GetTollFeeForDay(Vehicle vehicle, DateTime[] passTimes)
    {
        if (null == vehicle)
            throw new ArgumentException("Vehicle can not be null.");
        if (null == passTimes || passTimes.Length == 0)
            return 0;
        if (passTimes.Any(p => p.Date != passTimes[0].Date))
            throw new ArgumentException("All pass times must be from the same date.");

        if (vehicle.IsTollFree() || IsWeekend(passTimes[0]) || _holidayService.IsHoliday(passTimes[0])) return 0;

        var totalFee = 0;
        var orderedPassTimes = passTimes.OrderBy(p => p.TimeOfDay).ToList();
        var intervalStart = orderedPassTimes[0];

        for (var i = 0; i < orderedPassTimes.Count; i++)
        {
            if (orderedPassTimes[i] < intervalStart)
                continue;
            intervalStart = orderedPassTimes[i];
            var endTime = intervalStart.AddHours(1);

            //select the highest fee in interval
            totalFee += (from date in orderedPassTimes
                         where date >= intervalStart && date < endTime
                         select GetTollFee(date))
                .Max();

            intervalStart = endTime;
        }
        return Math.Min(totalFee, 60);
    }

    private bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    private int GetTollFee(DateTime passTime)
    {
        if (passTime.Hour < 6) return (int)Fees.Free;
        if (passTime.FallsBetween(new TimeSpan(6, 0, 0), new TimeSpan(6, 30, 0))) return (int)Fees.Low;
        if (passTime.FallsBetween(new TimeSpan(6, 30, 0), new TimeSpan(7, 0, 0))) return (int)Fees.Medium;
        if (passTime.FallsBetween(new TimeSpan(7, 0, 0), new TimeSpan(8, 0, 0))) return (int)Fees.High;
        if (passTime.FallsBetween(new TimeSpan(8, 0, 0), new TimeSpan(8, 30, 0))) return (int)Fees.Medium;
        //I've assumed the previous condition (where xx:00 - xx:29 in this was free) was incorrect (//else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;)
        //I've changed it to Low fee for the entire span.
        if (passTime.FallsBetween(new TimeSpan(8, 30, 0), new TimeSpan(15, 0, 0))) return (int)Fees.Low;
        if (passTime.FallsBetween(new TimeSpan(15, 0, 0), new TimeSpan(15, 30, 0))) return (int)Fees.Medium;
        if (passTime.FallsBetween(new TimeSpan(15, 30, 0), new TimeSpan(17, 0, 0))) return (int)Fees.High;
        if (passTime.FallsBetween(new TimeSpan(17, 0, 0), new TimeSpan(18, 0, 0))) return (int)Fees.Medium;
        if (passTime.FallsBetween(new TimeSpan(18, 0, 0), new TimeSpan(18, 30, 0))) return (int)Fees.Low;
        return (int)Fees.Free;
    }

    [Obsolete]
    public int GetTollFee_Old(DateTime date, Vehicle vehicle)
    {
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


    private enum Fees
    {
        Free = 0,
        Low = 8,
        Medium = 13,
        High = 18
    }
}