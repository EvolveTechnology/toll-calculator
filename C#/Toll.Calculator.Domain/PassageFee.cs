namespace Toll.Calculator.Domain
{
    public class PassageFee
    {
        public TimeStamp StartTime { get; set; }
        public TimeStamp EndTime { get; set; }
        public decimal Fee { get; set; }
    }
}