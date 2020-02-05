using System;

namespace TollCalculator.Lib.Models
{
    public class RushHourFee
    {
        public TimeOfDay From { get; set; }
        public TimeOfDay To { get; set; }
        public int Fee { get; set; }

        public RushHourFee(TimeOfDay from, TimeOfDay to, int fee)
        {
            From = from;
            To = to;
            Fee = fee;
        }

        public bool IsInsideRange(DateTime dateTime)
        {
            var fromDate = From.ToDateTime(dateTime);
            var toDate = To.ToDateTime(dateTime);

            return fromDate <= dateTime && dateTime <= toDate;
        }
    }
}