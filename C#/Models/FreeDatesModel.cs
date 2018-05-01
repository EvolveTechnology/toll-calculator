using System.Collections.Generic;

namespace TollFeeCalculator.Models
{
    public class FreeDatesModel
    {
        public int Year { get; set; }
        public List<MonthDaysModel> Dates { get; set; }
    }
}
