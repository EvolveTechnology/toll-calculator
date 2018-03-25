using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator.Holidays {

    /**
     * A holiday checker for Swedish holidays specific for 2018.
     */
    public class Sweden2018HolidayChecker : SwedenHolidayChecker {

        private readonly DateTime[] holidays = new DateTime[7] {
                new DateTime(2018, 3, 30),
                new DateTime(2018, 4, 1),
                new DateTime(2018, 4, 2),
                new DateTime(2018, 5, 10),
                new DateTime(2018, 5, 20),
                new DateTime(2018, 6, 23),
                new DateTime(2018, 11, 3)
            };

        public override bool IsHoliday(DateTime date) {
            foreach (DateTime holiday in holidays) {
                if (date.Equals(holiday)) {
                    return true;
                }
            }
            return base.IsHoliday(date);
        }

    }
}
