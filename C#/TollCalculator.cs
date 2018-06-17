using System;
using System.Collections.Generic;
using System.Linq;

public enum VehicleType
{
    Default = 0,

    // Vehicle types below are toll free
    Motorbike = 1,
    Tractor = 2,
    Emergency = 3,
    Diplomat = 4,
    Foreign = 5,
    Military = 6
}

public class TollCalculator
{

    private readonly Dictionary<Tuple<TimeSpan,TimeSpan>, int> Fees;

    public TollCalculator()
    {
        this.Fees = new Dictionary<Tuple<TimeSpan, TimeSpan>, int>
        {
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(0, 0, 0), new TimeSpan(5, 59, 59)), 0 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 59)), 8 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 59)), 13 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 59)), 18 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 59)), 13 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 59)), 8 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 59)), 13 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 59)), 18 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(17, 0, 0), new TimeSpan(17, 59, 59)), 13 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(18, 0, 0), new TimeSpan(18, 29, 59)), 8 },
            { new Tuple<TimeSpan, TimeSpan>(new TimeSpan(18, 30, 0), new TimeSpan(23, 59, 59)), 0 }
        };
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param VehicleType - the vehicle, use the enum type provided above
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    public int GetTollFee(VehicleType vehicle, DateTime[] timestamps)
    {
        Array.Sort(timestamps);

        if (IsTollFreeDate(timestamps[0]) || IsTollFreeVehicle(vehicle)) return 0;

        DateTime previousTimestamp = timestamps[0];
        int totalFee = 0;

        foreach (DateTime timestamp in timestamps)
        {
            if ((timestamp - timestamps[0]).Days >= 1)
            {
                // Should not happen since it should be timestamps from one single day, but if someone
                // uses this incorrectly, this is a simple safety measure.
                throw new Exception("All timestamps should be on the same day.");
            }
            else if (timestamp == timestamps[0] || (timestamp - previousTimestamp).Hours >= 1)
            {
                totalFee += TollFee(timestamp.Hour, timestamp.Minute);
            }
            else
            {
                // Already payed once this hour.
                // May be the case that you payed a lower fee than would be required now, 
                // but I feel generous today.
            }
            previousTimestamp = timestamp;
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }

    private int TollFee(int hour, int minute)
    {

        TimeSpan t = new TimeSpan(hour, minute, 0);

        IEnumerable<int> output = from row in this.Fees
                     where  t >= row.Key.Item1 &&
                            t <= row.Key.Item2
                     select row.Value;

        return output.FirstOrDefault();

    }

    private bool IsTollFreeVehicle(VehicleType vehicle)
    {
        if (vehicle == 0) return false;
        // If not 0, then it must be a vehicle type that is toll free
        return true;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
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