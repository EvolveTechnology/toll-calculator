using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Models;
using TollFeeCalculator.Utils;

// Note that since this is an assignment for a potential job, I have written more elaborate comments than usual in my code.
// This is just a way to document my thought process for the reviewers since I can't communicate with them any other way. 

// Also note that Exceptions thrown by this code are intended to be caught, logged and handled within the code of the calling system.

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTotalTollFeeForDay(IVehicle vehicle, DateTime[] dates)
    {
        if (dates is null || dates.Length == 0)
            return 0;

        var dayOfYear = dates.First().DayOfYear;
        
        if (dates.Any(date => date.DayOfYear != dayOfYear))
            throw new ArgumentException("Not all dates are from the same day!");
        
        dates.OrderBy(date => date.TimeOfDay);

        int totalFee = 0;
        DateTime intervalStart = dates[0];
                
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            long diffInMillies = date.Millisecond - intervalStart.Millisecond;
            long minutes = diffInMillies / 1000 / 60;

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

        return Math.Min(totalFee, 60);
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle is null)
            throw new ArgumentNullException($"Parameter '{nameof(vehicle)}' is null.");

        var vehicleType = vehicle.GetVehicleType();

        switch (vehicleType)
        {
            case VehicleType.Motorbike:
            case VehicleType.Tractor:
            case VehicleType.Emergency:
            case VehicleType.Diplomat:
            case VehicleType.Foreign:
            case VehicleType.Military:
                return true;
            case VehicleType.Car:
                return false;
            default:
                throw new ArgumentOutOfRangeException($"Encountered unknown vehicle type '{vehicleType}'.");
        }
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            return 0;

        var rushHourType = GetRushHourType(date);

        switch(rushHourType)
        {
            case RushHourType.LowRush:
                return 8;
            case RushHourType.MediumRush:
                return 13;
            case RushHourType.HighRush:
                return 18;
            default: 
                return 0;
        }
    }

    private RushHourType GetRushHourType(DateTime date)
    {        
        if (date.IsBetweenTimes("06:00", "06:30")) return RushHourType.LowRush;
        if (date.IsBetweenTimes("06:30", "07:00")) return RushHourType.MediumRush;
        if (date.IsBetweenTimes("07:00", "08:00")) return RushHourType.HighRush;
        if (date.IsBetweenTimes("08:00", "08:30")) return RushHourType.MediumRush;
        if (date.IsBetweenTimes("08:30", "15:00")) return RushHourType.LowRush;
        if (date.IsBetweenTimes("15:00", "15:30")) return RushHourType.MediumRush;
        if (date.IsBetweenTimes("15:30", "17:00")) return RushHourType.HighRush;
        if (date.IsBetweenTimes("17:00", "18:00")) return RushHourType.MediumRush;
        if (date.IsBetweenTimes("18:00", "18:30")) return RushHourType.LowRush;

        return RushHourType.NoRush;

        // Logic translated from these conditions:

        //if (hour == 6 && minute >= 0 && minute <= 29) return 8;
        //else if (hour == 6 && minute >= 30 && minute <= 59) return 13;
        //else if (hour == 7 && minute >= 0 && minute <= 59) return 18;
        //else if (hour == 8 && minute >= 0 && minute <= 29) return 13;
        //else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
        //else if (hour == 15 && minute >= 0 && minute <= 29) return 13;
        //else if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
        //else if (hour == 17 && minute >= 0 && minute <= 59) return 13;
        //else if (hour == 18 && minute >= 0 && minute <= 29) return 8;
        //else return 0;

        // Note that one of the conditions doesn't really make sense:

        //else if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;

        // This condition doesn't include times like e.g. 10:00 since the minute is less than 30. 
        // But it makes no sense to make the first 30 minutes of these hours free, so I assume that this is just an error in the condition.
    }

    private bool IsTollFreeDate(DateTime date)
    {
        if (IsWeekend(date) || IsHoliday(date))
            return true;

        return false;
    }

    private bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday
            || date.DayOfWeek == DayOfWeek.Sunday;
    }

    private bool IsHoliday(DateTime date)
    {
        // This one is quite tricky to determine, since not all holidays have static dates. There are also different holidays in different countries. 
        // Since the fees in the assignment intro are denoted in SEK, I will assume that it is the Swedish holidays that are of interest for this app.

        // I see 4 alternatives for implementing this logic: 

        // 1. Hardcode the holiday dates for all the years the app is expected to be in use. Should be easy enough to google the dates.
        //    Obviously a flawed option, since the app cannot be used longer than the hardcoded dates allow for. 
        //    Also, holiday dates may change due to political decisions during the lifetime of the app.

        // 2. Make the holiday dates configurable, by adding a config file or a setting saved in a database or registry. 
        //    This makes the system very flexible, but the dates need to be maintained and updated yearly by hand. 

        // 3. Lookup the holiday dates using some external third-party logic. There is probably some public API or .NET library out there that already does this calculation anyway.
        //    Drawbacks are: Dependency on third-party services or libraries. Overhead for implementing the API client/downloading the library, risk that the API is down.

        // 4. Implement local methods for calculating the date of each of the moving holidays depending on the year. 
        //    This requires researching the rules that determine the date of each moving holiday, to get every calculation exactly right. 
        //    This approach is still vulnerable to political decisions that may change any of those rules during the lifetime of the app.

        // Which of these approaches I would choose in a real world scenario would depend on the circumstances I guess... 
        // Most likely I would implement my own methods in most cases. Maybe even with an option to override calculations with configured values.

        // For the purpose of this assignment I'll implement methods based on the info here: http://hogtider.se/helgdagar-i-sverige/
        // I'll include the three "helgdagsaftnar" (Midsommarafton, Julafton, Nyårsafton) in my definition of holidays. 
        // Note that while "Påskafton" is not a "helgdagsafton" it is always a Saturday, so it will be toll free in this app due to the weekend condition. Same with "Pingstafton".
        // If the year is a "skottår" there will be an additional day. Skottdagen is not a holiday (unless it's a Sunday).

        // All Sundays are holidays. 
        if (date.DayOfWeek == DayOfWeek.Sunday) 
            return true;

        var staticHolidays = new List<(int Month, int Day)>()
        {
            (1, 1),
            (1, 6),
            (5, 1),
            (6, 6),
            (12, 24),
            (12, 25),
            (12, 26),
            (12, 31),
        };
                
        var movingHolidays = GetMovingHolidays(date.Year); // (Ordinary Sundays not included)

        var holidays = new List<(int Month, int Day)>();
        holidays.AddRange(staticHolidays);
        holidays.AddRange(movingHolidays);

        var tollFeeDate = (date.Month, date.Day);

        if (holidays.Any(holiday => holiday.Month == tollFeeDate.Month && holiday.Day == tollFeeDate.Day))
            return true;

        return false;
    }

    private List<(int Month, int Day)> GetMovingHolidays(int year)
    {
        var easterDay = CalendarUtils.CalculateEasterDay(year);
        var midsummerDay = CalendarUtils.CalculateMidsummerDay(year);
        var allSaintsDay = CalendarUtils.CalculateAllSaintsDay(year);
        var ascensionDay = easterDay.AddDays(39); // "Kristi Himmelsfärdsdag", sixth Thursday after Easter Day.
        var pentecostDay = easterDay.AddDays(49); // "Pingstdagen", 7 weeks after Easter Day.

        var movingHolidayDates = new List<DateTime>()
        {
            easterDay.AddDays(-2),
            easterDay,
            easterDay.AddDays(1),
            ascensionDay,
            pentecostDay,
            midsummerDay.AddDays(-1),
            midsummerDay,
            allSaintsDay,
        };

        var movingHolidays = new List<(int Month, int Day)>();

        foreach (var date in movingHolidayDates)
        {
            movingHolidays.Add((date.Month, date.Day));
        }

        return movingHolidays;
    }
}