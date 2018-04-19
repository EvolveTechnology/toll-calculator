using System;

namespace TollFeeCalculator
{
    public interface IDateService
    {
        bool IsTollFreeDate(DateTime date);
    }
}
