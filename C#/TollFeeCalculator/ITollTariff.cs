using System;
using TollFeeCalculator;

public interface ITollTariff
{
    int GetTollFee(DateTime date, Vehicle vehicle);
    Boolean IsTollFreeDate(DateTime date);
}