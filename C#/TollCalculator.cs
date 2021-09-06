using System;
using System.Globalization;
using TollFeeCalculator;
using System.Collections.Generic;

public class TollCalculator
{

    const int MAX_DAY_FEE = 60;
    const int FEE_INTERVAL_MINUTES = 60;

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalDayFee = 0;
        int intervalFee = 0;
        List<DateTime> datesSorted = new List<DateTime>(dates);
        datesSorted.Sort();
        foreach (DateTime date in datesSorted)
        {
            int nextFee = GetTollFee(date, vehicle);

            int diffInMinutes = Convert.ToInt32((date - intervalStart).TotalMinutes);

            if (diffInMinutes <= FEE_INTERVAL_MINUTES)
            {
                if (nextFee >= intervalFee)
                {
                    intervalFee = nextFee;
                }
            }
            else
            {
                totalDayFee += intervalFee;
                if (totalDayFee > MAX_DAY_FEE)
                {
                    return MAX_DAY_FEE;
                }
                intervalStart = date;
                intervalFee = nextFee;
            }
        }

        totalDayFee += intervalFee;
        if (totalDayFee > MAX_DAY_FEE)
        {
            return MAX_DAY_FEE;
        }

        return totalDayFee;
    }

    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) || vehicle.IsTollFree())
        {
            return 0;
        }

        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute <= 29) return 8;
        else if (hour == 6) return 13;
        else if (hour == 7) return 18;
        else if (hour == 8 && minute <= 29) return 13;
        else if (hour >= 8 && hour <= 14) return 8;
        else if (hour == 15 && minute <= 29) return 13;
        else if (hour == 15 || hour == 16) return 18;
        else if (hour == 17) return 13;
        else if (hour == 18 && minute <= 29) return 8;
        else return 0;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && (day == 1 || day == 5 || day == 6) ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }
}