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

        /// <summary>
        /// Returns true if the current time period spans over the given hour and minute.
        /// </summary>
        public bool SpansOver(int hour, int minute)
        {
            if (hour > _startHour && hour < _endHour)
                return true;
            else if (hour == _startHour && minute >= _startMinute && minute <= _endMinute)
                return true;
            else if (hour == _endHour && minute <= _endMinute)
                return true;

            return false;
        }
    }
}
