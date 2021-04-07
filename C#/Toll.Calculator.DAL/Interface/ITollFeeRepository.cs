using System;
using System.Collections.Generic;
using Toll.Calculator.Domain;

namespace Toll.Calculator.DAL.Interface
{
    public interface ITollFeeRepository
    {
        decimal GetPassageFeeByTime(DateTime passageTime);

        bool IsTollFreeDate(DateTime passageTime);
    }
}