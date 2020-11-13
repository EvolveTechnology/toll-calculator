using System;
using System.Collections.Generic;

namespace IntegrationTests
{
    public static class DateFeeder
    {
        public static List<DateTime> GetPassesWithTollTimes()
        {
            return new List<DateTime>()
            {
                new DateTime(2013,2,1,7,15,23),
                new DateTime(2013,2,1,8,15,23),
                new DateTime(2013,2,1,16,15,23),
                new DateTime(2013,2,1,17,15,23)
            };
        }

        public static List<DateTime> GetPassesWithTollTimesOnTollFreeDay()
        {
            return new List<DateTime>()
            {
                new DateTime(2013,5,1,7,15,23),
                new DateTime(2013,5,1,8,15,23),
                new DateTime(2013,5,1,16,15,23),
                new DateTime(2013,5,1,17,15,23)
            };
        }

        public static List<DateTime> GetPassesWithTollTimesOnDifferebtDays()
        {
            return new List<DateTime>()
            {
                new DateTime(2013,5,1,7,15,23),
                new DateTime(2013,5,1,8,15,23),
                new DateTime(2013,5,2,16,15,23),
                new DateTime(2013,5,1,17,15,23)
            };
        }
    }
}
