using System;
using System.Linq;
using TollFeeCalculator;

public partial class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param day   - day of passes
     * @param times   - times of all passes on one day
     * @return - the total toll fee for that day
     */
    public int GetTollFee(Vehicle vehicle, DateTime day, TimeSpan[] times)
    {
        if (day.IsWeekendOrHoliday() || vehicle.IsTollFreeVehicle()) return 0;

        var groups = times.GroupBy(x => (x - times.FirstOrDefault()).Ticks / TimeSpan.FromHours(1).Ticks).ToList(); // grouping per hour (based on the first item)

        var totalFee = 0;

        foreach (var timeGroupdItems in groups)
        {
            var maxFeeInThePeriod = timeGroupdItems.Max(x => x.GetFeeOfSpecificTime());

            totalFee += maxFeeInThePeriod;
        }

        return totalFee > 60 ? 60 : totalFee;
    }
}