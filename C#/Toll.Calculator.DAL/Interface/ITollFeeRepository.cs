using System;
using System.Collections.Generic;
using Toll.Calculator.Domain;

namespace Toll.Calculator.DAL.Interface
{
    public interface ITollFeeRepository
    {
        PassageFee GetPassageFeeByTime(DateTime passageTime);
    }
}