using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator.TollFeeTime
{
    public class TollFeeTimeService : ITollFeeTimeService
    {
        private List<FeeTime> _feeTimes;

        public TollFeeTimeService()
        {
            _feeTimes = new List<FeeTime> {
                new FeeTime(new TimeSpan(6,0,0),new TimeSpan(6,29,0),8),
                new FeeTime(new TimeSpan(6,30,0),new TimeSpan(6,59,0),13),
                new FeeTime(new TimeSpan(7,0,0),new TimeSpan(7,59,0),18),
                new FeeTime(new TimeSpan(8,0,0),new TimeSpan(8,29,0),13),
                new FeeTime(new TimeSpan(8,30,0),new TimeSpan(14,59,0),8),
                new FeeTime(new TimeSpan(15,0,0),new TimeSpan(15,29,0),13),
                new FeeTime(new TimeSpan(15,30,0),new TimeSpan(16,59,0),18),
                new FeeTime(new TimeSpan(17,0,0),new TimeSpan(17,59,0),13),
                new FeeTime(new TimeSpan(18,0,0),new TimeSpan(18,29,0),8),
            };
        }

        public FeeTime GetFeeTime(TimeSpan date)
        {
            return _feeTimes.FirstOrDefault(x => x.Start <= date && x.End >= date) ?? new FeeTime(new TimeSpan(), new TimeSpan(), 0);
        }

        public bool IsTollFreeDate(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                return true;

            if (year != 2013)
                return false;

            return month == 1 && day == 1 ||
                   month == 3 && (day == 28 || day == 29) ||
                   month == 4 && (day == 1 || day == 30) ||
                   month == 5 && (day == 1 || day == 8 || day == 9) ||
                   month == 6 && (day == 5 || day == 6 || day == 21) ||
                   month == 7 ||
                   month == 11 && day == 1 ||
                   month == 12 && (day == 24 || day == 25 || day == 26 || day == 31);
        }
    }
}

