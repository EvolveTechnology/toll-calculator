using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator;

public class TollCalculator
{
    private readonly ITollTariff tollTariff;
    public TollCalculator(ITollTariff tariff)
    {
        tollTariff = tariff;
    }

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    public int GetTollFee(Vehicle vehicle, DateTime[] dates)
    {
        var passages = dates.Where(date => tollTariff.GetTollFee(date,vehicle) > 0);
        return Math.Min(60, CalculateOptimalTollFee(vehicle, passages));
    }

    private int CalculateOptimalTollFee(Vehicle vehicle, IEnumerable<DateTime> passages)
    {
        var totalFee = 0;
        foreach (var passage in passages)
        {
            var tempFee = 
                tollTariff.GetTollFee(passage, vehicle) + 
                CalculateOptimalTollFee(vehicle, passages.Where(pass => Math.Abs((passage-pass).TotalHours) > 1));
            totalFee = Math.Max(totalFee, tempFee);
            if (totalFee >= 60) return 60;
        }
        return totalFee;
    }
}