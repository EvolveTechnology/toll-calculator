using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Linq;

using TollFeeCalculator;
using TollFeeCalculator.Vehicles;

//TODO: get logger to log errors
public class TollCalculator
{
    /// Calculate the total toll fee for one day
    /// 
    /// @param vehicle - the vehicle
    /// @param dates   - date and time of all passes on one day
    /// @return - the total toll fee for that day
    public int GetTollFee(
        Vehicle vehicle, 
        DateTime[] dates,
        bool isNotDebugMode = true)
    {
        if (!dates.Any())
            return 0;

        DateTime intervalStart = dates.First();

        if (isNotDebugMode && dates.Any(d => d.Year != intervalStart.Year
            || d.Month != intervalStart.Month
            || d.Day != intervalStart.Day))
        {
            throw new ApplicationException("Only dates of the same day is allowed");
        }

        int totalFee = 0;

        foreach (DateTime date in dates)
        {
            if (totalFee >= 60)
                return 60;

            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);

            TimeSpan timeDiff = date - intervalStart;
            var elapsedMinutesBetweenFees = timeDiff.TotalMilliseconds/1000/60;

            if (elapsedMinutesBetweenFees <= 60)
            {
                if (totalFee > 0) 
                    totalFee -= tempFee;

                if (nextFee >= tempFee) 
                    tempFee = nextFee;

                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }

        return totalFee;
    }

    //TODO: datetime is always in range, no need for all these checks?
    public int GetTollFee(DateTime date, Vehicle vehicle)
    {
        if (IsTollFreeDate(date) 
            || (vehicle?.IsNotTollable ?? false) ) 
        {
            return 0;
        }

        // DateTime cannot be larger than 23 hours or 59 minutes, no need to check.
        // cannot be negative eighter
        int hour = date.Hour;
        int minute = date.Minute;

        if (hour == 6 && minute <= 29) 
            return 8;
        else if (hour == 6 && minute >= 30) 
            return 13;
        else if (hour == 7) 
            return 18;
        else if (hour == 8 && minute <= 29) 
            return 13;
        else if (hour >= 8 && hour <= 14 && minute >= 30 ) 
            return 8;
        else if (hour == 15 && minute <= 29) 
            return 13;
        else if (hour == 15 || hour == 16) 
            return 18;
        else if (hour == 17) 
            return 13;
        else if (hour == 18 && minute <= 29) 
            return 8;
        else 
            return 0;
    }

    //TODO: Get all dates for the whole year and save to file/db. 
    //      Read and cache at start to increate performance, request calls are expensive
    //TODO: Needs futhur testing, we assume all "helgdagar" is toll free
    //      Toll free dates: https://transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Betalning/dagar-da-trangselskatt-inte-tas-ut/
    private Boolean IsTollFreeDate(DateTime date)
    {
        var day = date.Day;
        var month = date.Month;
        if ((month == 6 && day == 5) ||
            month == 12 && (day == 25 || day == 26 || day == 31) ||
            month == 7 )
        {
            return true;
        }

        WebClient client = null;
        try 
        {
            client = new WebClient();
            var json = client.DownloadString(this.GetEndpoint(date));

            if (json.Contains(",\"helgdag"))
            {
                return true;
            }
            // Current know edge case is "kristi himmelsfärd", but all days before
            // a "helgdag" should be toll free, see https://transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Betalning/dagar-da-trangselskatt-inte-tas-ut/
            else if (json.Contains("arbetsfri helgdag"))
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return true; // We can always pay back
        }
        finally
        {
            client.Dispose();
        }

        return false;
    }

    //TODO: read engpoint form some kind of config instead
    private string GetEndpoint(DateTime date)
        => "https://api.dryg.net/dagar/v2.1/" 
            + date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

    
}