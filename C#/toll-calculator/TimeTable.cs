using System;
using System.Collections.Generic;
using System.Linq;

namespace toll_calculator
{
    public class TimeTable : ITimeTable
    {
        private List<TollZone> timeZones;

        public TimeTable(IConfiguration configuration)
        {
            timeZones = configuration.TollZones();
        }

        public int GetFeeAtTimeStamp(DateTime time)
        {
            var result = timeZones.FirstOrDefault(x => x.IsValidZone(time));
            return result != null ? result.Fee : 0;
        }
    }

    public interface ITimeTable
    {
        public int GetFeeAtTimeStamp(DateTime time);
    }
}
