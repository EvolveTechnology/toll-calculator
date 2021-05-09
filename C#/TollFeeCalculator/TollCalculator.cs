using System;
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
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = tollTariff.GetTollFee(date, vehicle);
            int tempFee = tollTariff.GetTollFee(intervalStart, vehicle);

            var minutes = (date-intervalStart).TotalMinutes;

            if (minutes <= 60)
            {
                if (totalFee > 0) totalFee -= tempFee;
                if (nextFee > tempFee) 
                {
                    intervalStart = date;
                    tempFee = nextFee;
                }
                totalFee += tempFee;
            }
            else
            {
                intervalStart = date;
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }
}