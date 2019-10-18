using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
    public class FreeDatesRepository
    {
        public IEnumerable<DateTime> GetFeeFreeDays()
        {
            return new []
            {
                new DateTime(2013, 1, 1),
                new DateTime(2013, 3, 28),
                new DateTime(2013, 3, 29),
                new DateTime(2013, 4, 1),
                new DateTime(2013, 4, 30),
                new DateTime(2013, 5, 1),
                new DateTime(2013, 5, 8),
                new DateTime(2013, 5, 9),
                new DateTime(2013, 6, 5),
                new DateTime(2013, 6, 6),
                new DateTime(2013, 6, 21),
                new DateTime(2013, 11, 1),
                new DateTime(2013, 12, 24),
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2013, 12, 31)
            };
        }

        public IEnumerable<DateTime> GetFeeFreeMonths()
        {
            return new []
            {
                new DateTime(2013, 7, 1)
            };
        }
    }
}
