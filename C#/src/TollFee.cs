using System;
using PublicHoliday;

namespace TollFeeCalculator
{
    public class TollFee
    {
        private const int HighTollFee = 18;
        private const int MidTollFee = 13;
        private const int LowTollFee = 8;
        private readonly SwedenPublicHoliday _swedenPublicHoliday = new SwedenPublicHoliday();

        public int GetFeeForDate(DateTime date)
        {
            if (IsTollFreeDate(date))
            {
                return 0;
            }

            return (date.TimeOfDay) switch
            {
                var time when time.IsInRange(6, 0, 6, 29) => LowTollFee,
                var time when time.IsInRange(6, 30, 6, 59) => MidTollFee,
                var time when time.IsInRange(7, 0, 7, 59) => HighTollFee,
                var time when time.IsInRange(8, 0, 8, 29) => MidTollFee,
                var time when time.IsInRange(8, 30, 14, 59) => LowTollFee,
                var time when time.IsInRange(15, 0, 15, 29) => MidTollFee,
                var time when time.IsInRange(15, 30, 16, 59) => HighTollFee,
                var time when time.IsInRange(17, 0, 17, 59) => MidTollFee,
                var time when time.IsInRange(18, 0, 18, 29) => LowTollFee,
                _ => 0
            };
        }

        private bool IsTollFreeDate(DateTime date)
        {
            return
                date.DayOfWeek == DayOfWeek.Saturday ||
                date.DayOfWeek == DayOfWeek.Sunday ||
                _swedenPublicHoliday.IsPublicHoliday(date);
        }
    }
}
