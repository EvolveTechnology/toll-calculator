using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator.Interfaces
{
    // Define daily toll fee structure
    public interface IDailyTollFees
    {
        // Get the daily fee structure as a collection of <StartTime, fee> values.
        IDictionary<TimeSpan, decimal> GetRates();

    }
}
