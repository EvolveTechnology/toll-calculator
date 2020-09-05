using System;
using PublicHoliday;
using TollFeeCalculator;

public class TollCalculator
{
    private readonly SwedenPublicHoliday swedenPublicHoliday = new SwedenPublicHoliday();

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        int i = 0;
        DateTime intervalStart = dates[i];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = (long)(date - intervalStart).TotalMilliseconds;
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

            intervalStart = dates[i++];
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || vehicle.IsTollFree()) return 0;

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

    private Boolean IsTollFreeDate(DateTime date)
    {
        return
            date.DayOfWeek == DayOfWeek.Saturday ||
            date.DayOfWeek == DayOfWeek.Sunday ||
            swedenPublicHoliday.IsPublicHoliday(date);
    }
}