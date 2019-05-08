using System;

namespace TollFeeCalculator.TollFeeTime
{
    public interface ITollFeeTimeService
    {
        FeeTime GetFeeTime(TimeSpan date);
        bool IsTollFreeDate(DateTime date);
    }
}