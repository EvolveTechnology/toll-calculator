using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator {
    public class Utils {

        public static int TimeBetweenTimestampsInMinutes (DateTime startTime, DateTime endTime) {
            TimeSpan duration = endTime.Subtract(startTime);
            return (int)duration.TotalMinutes;
        }

        public static bool IsWeekend(DateTime date) {
            return (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday);
        }

    }
}
