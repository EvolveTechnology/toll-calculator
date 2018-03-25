using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Holidays;

namespace Toll_calculator.Policies {
    public class StandardDateTollPolicy : IDateTollPolicy {

        private IHolidayChecker holidayChecker;

        public StandardDateTollPolicy(IHolidayChecker holidayChecker) {
            this.holidayChecker = holidayChecker;
        }

        public bool IsTollable(DateTime date) {
            return !Utils.IsWeekend(date) && !holidayChecker.IsHoliday(date);
        }

    }
}
