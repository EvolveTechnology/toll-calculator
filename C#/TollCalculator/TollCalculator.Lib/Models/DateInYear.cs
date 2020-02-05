namespace TollCalculator.Lib.Models
{
    public class YearlyDate
    {
        public int Day { get; set; }
        public int Month { get; set; }

        public YearlyDate(int month, int day)
        {
            Month = month;
            Day = day;
        }

        public bool IsEqualToDate(int month, int day)
        {
            return day == Day && month == Month;
        }
    }
}