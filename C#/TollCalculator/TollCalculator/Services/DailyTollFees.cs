using System;
using System.Collections.Generic;
using TollFeeCalculator.Interfaces;

namespace TollFeeCalculator.Services
{
    public class DailyTollFees : IDailyTollFees
    {
        private IDictionary<TimeSpan, decimal> _dailyRates => new Dictionary<TimeSpan, decimal>
        {
            { new TimeSpan(06, 00, 0), 8m },
            { new TimeSpan(06, 30, 0), 13m },
            { new TimeSpan(07, 00, 0), 18m },
            { new TimeSpan(08, 00, 0), 13m },
            { new TimeSpan(08, 30, 0), 8m },
            { new TimeSpan(15, 00, 0), 13m },
            { new TimeSpan(15, 30, 0), 18m },
            { new TimeSpan(17, 00, 0), 13m },
            { new TimeSpan(18, 00, 0), 8m },
            { new TimeSpan(18, 30, 0), 0m }
        };

        public IDictionary<TimeSpan, decimal> GetRates()
        {
            return _dailyRates;
        }
    }
}
