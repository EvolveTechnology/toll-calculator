using System;
using System.Collections.Generic;

namespace TollFeeCalculator.Toll
{
    public abstract class Builder
    {
        public abstract void BuildHollidays();
        public abstract List<DateTime> GetResult();
    }
}
