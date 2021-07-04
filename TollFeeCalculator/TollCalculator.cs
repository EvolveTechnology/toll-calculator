using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Models;

// Note that since this is an assignment for a potential job, I have written more elaborate comments than usual in my code.
// This is just a way to document my thought process for the reviewers since I can't communicate with them any other way. 
public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
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
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle is null)
            return false; // Throw ArgumentNullException?

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
                return false; // Throw ArgumentOutOfRangeException?
        }
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle))
            return 0;

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
                
        // (Ordinary Sundays not included)
        var movingHolidays = GetMovingHolidays(date.Year);

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
        var easterDay = CalculateEasterDay(year);
        var midsummerDay = CalculateMidsummerDay(year);
        var allSaintsDay = CalculateAllSaintsDay(year);
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

    private DateTime CalculateEasterDay(int year)
    {
        // Gauss Easter formula, code copied and adapted from here: https://www.geeksforgeeks.org/how-to-calculate-the-easter-date-for-a-given-year-using-gauss-algorithm/

        float A, B, C, P, Q,
            M, N, D, E;

        // All calculations done
        // on the basis of
        // Gauss Easter Algorithm
        A = year % 19;
        B = year % 4;
        C = year % 7;
        P = (float)(year / 100);
        Q = (float)(
            (13 + 8 * P) / 25);
        M = (15 - Q + P - P / 4) % 30;
        N = (4 + P - P / 4) % 7;
        D = (19 * A + M) % 30;
        E = (2 * B + 4 * C + 6 * D + N) % 7;
        int days = (int)(22 + D + E);

        // A corner case,
        // when D is 29
        if ((D == 29) && (E == 6))
        {
            return new DateTime(year, 4, 19);
        }

        // Another corner case,
        // when D is 28
        else if ((D == 28) && (E == 6))
        {
            return new DateTime(year, 4, 18);
        }
        else
        {

            // If days > 31, move to April
            // April = 4th Month
            if (days > 31)
            {
                Console.Write(year + "-04-" +
                             (days - 31));
                return new DateTime(year, 4, days - 31);
            }
            // Otherwise, stay on March
            // March = 3rd Month
            else
            {
                return new DateTime(year, 3, days);
            }
        }
    }

    private DateTime CalculateMidsummerDay(int year)
    {
        var potentialDates = new List<DateTime>()
        {
            new DateTime(year, 6, 20),
            new DateTime(year, 6, 21),
            new DateTime(year, 6, 22),
            new DateTime(year, 6, 23),
            new DateTime(year, 6, 24),
            new DateTime(year, 6, 25),
            new DateTime(year, 6, 26),
        };

        foreach (var date in potentialDates)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return date;
        }

        // Logically unreachable code since one of the potential dates must be a Saturday, but the compiler doesn't know that.
        return potentialDates.First();
    }

    private DateTime CalculateAllSaintsDay(int year)
    {
        var potentialDates = new List<DateTime>()
        {
            new DateTime(year, 10, 31),
            new DateTime(year, 11, 1),
            new DateTime(year, 11, 2),
            new DateTime(year, 11, 3),
            new DateTime(year, 11, 4),
            new DateTime(year, 11, 5),
            new DateTime(year, 11, 6),
        };

        foreach (var date in potentialDates)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
                return date;
        }

        // Logically unreachable code since one of the potential dates must be a Saturday, but the compiler doesn't know that.
        return potentialDates.First();
    }
}