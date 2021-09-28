using System;
using System.Collections.Generic;

namespace TollFeeCalculator.Toll
{
    public class Build : Builder
    {
        private List<DateTime> HolidayDates = new List<DateTime>();


        public override void BuildHollidays()
        {
            HolidayDates.Add(new DateTime(2021, 11, 01));
            HolidayDates.Add(new DateTime(2021, 12, 24));
            HolidayDates.Add(new DateTime(2021, 12, 25));
            HolidayDates.Add(new DateTime(2021, 12, 26));
            HolidayDates.Add(new DateTime(2021, 12, 27));
        }

        public  override List<DateTime>  GetResult()
        {
            return HolidayDates;
        }
    }
}
