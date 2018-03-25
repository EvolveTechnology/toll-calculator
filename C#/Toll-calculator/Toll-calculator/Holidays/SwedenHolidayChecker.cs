using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator.Holidays {

    /**
     * A holiday checker for fixed data holidays in Sweden such
     * as New Year's Day and Christmas Day.
     */
    public abstract class SwedenHolidayChecker : IHolidayChecker {

        /**
         * The year parameter of these dates does not matter since
         * these are holidays at fixed dates for every year.
         */
        private readonly DateTime[] holidays = new DateTime[6] {
                new DateTime(1, 1, 1),
                new DateTime(1, 1, 6),
                new DateTime(1, 5, 1),
                new DateTime(1, 6, 6),
                new DateTime(1, 12, 25),
                new DateTime(1, 12, 26)
            };

        public virtual bool IsHoliday(DateTime date) {
            foreach(DateTime holiday in holidays) {
                if(date.Month == holiday.Month && date.Day == holiday.Day) {
                    return true;
                }
            }
            return false;
        }
    }

}
