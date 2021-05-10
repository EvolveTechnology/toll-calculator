using System;

namespace TollFeeCalculator
{
    public interface ITollTariff
    {
        int TollIntervalInMinutes { get; }
        int MaxFeePerDay { get; }
        int GetTollFee(DateTime date, Vehicle vehicle);
    }
}