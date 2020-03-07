using System;

namespace TollFeeCalculator.Interfaces
{
    public interface ITollFreeDates
    {
        bool IsTollFreeDate(DateTime date);
    }
}
