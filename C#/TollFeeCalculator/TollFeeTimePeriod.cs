namespace TollFeeCalculator
{
    /// <summary>
    /// Represents a daily time period with a specific toll fee.
    /// </summary>
    public class TollFeeTimePeriod
    {
        private int _startHour;
        private int _startMinute;
        private int _endHour;
        private int _endMinute;

        public int TollFee { get; private set; }

        public TollFeeTimePeriod(int startHour, int startMinute, int endHour, int endMinute, int tollFee)
        {
            _startHour = startHour;
            _startMinute = startMinute;
            _endHour = endHour;
            _endMinute = endMinute;

            TollFee = tollFee;
        }

        public bool Contains(int hour, int minute)
        {
            if (hour >= _startHour && hour <= _endHour 
                && minute >= _startMinute && minute <= _endMinute)
                return true;

            return false;
        }
    }
}
