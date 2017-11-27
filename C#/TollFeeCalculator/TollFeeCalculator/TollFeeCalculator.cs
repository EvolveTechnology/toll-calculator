using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.HolidayLookup;
using TollFeeCalculator.Vehicle;

namespace TollFeeCalculator
{
    /// <summary>
    /// Implements a toll fee calculator for Gothenburg, Sweden.
    /// </summary>
    public class TollFeeCalculator : ITollFeeCalculator
    {
        private readonly IHolidayLookup holidayLookup;

        public TollFeeCalculator(IHolidayLookup holidayLookup)
        {
            this.holidayLookup = holidayLookup ?? throw new ArgumentNullException(nameof(holidayLookup));
        }

        /// <inheritdoc />
        public int GetDailyTollFee(IVehicle vehicle, DateTime[] passages)
        {
            if (vehicle.IsTollFree()) return 0;
            const int maxDailyTollFee = 60;
            return Math.Min(
                maxDailyTollFee,
                GetChargeablePassages(passages.Where(passage => !IsTollFreeDate(passage)).ToArray())
                    .Select(CalculateTollFeeForPassage)
                    .Sum()
            );
        }

        /// <summary>
        /// Returns true if a passage on a certain date is not subject for a toll fee.
        /// </summary>
        internal bool IsTollFreeDate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday 
                    || date.DayOfWeek == DayOfWeek.Sunday 
                    || holidayLookup.IsPublicHoliday(date);
        }

        /// <summary>
        /// Calculates a toll fee for a passage using rules for the city of Gothenburg.
        /// Normally these rules should be fed from a configuration system.
        /// </summary>
        internal int CalculateTollFeeForPassage(DateTime passage)
        {
            var hour = passage.Hour;
            var minute = passage.Minute;

            if (hour == 6 && minute >= 0 && minute <= 29) return 8;
            if (hour == 6 && minute >= 30 && minute <= 59) return 13;
            if (hour == 7 && minute >= 0 && minute <= 59) return 18;
            if (hour == 8 && minute >= 0 && minute <= 29) return 13;
            if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8;
            if (hour == 15 && minute >= 0 && minute <= 29) return 13;
            if (hour == 15 && minute >= 0 || hour == 16 && minute <= 59) return 18;
            if (hour == 17 && minute >= 0 && minute <= 59) return 13;
            if (hour == 18 && minute >= 0 && minute <= 29) return 8;
            return 0;
        }

        /// <summary>
        /// Returns a sorted list of passages such that no passage occurs within one hour of any other passage.
        /// A vehicle should only be charged once an hour. The very first passage is considered as chargeable (greedy strategy).
        /// NOTE: this implementation does not take monetary optimization into account.
        /// E.g. consider these passages during a day:  [08:40 8kr], [09:00 18kr], [17:00 8kr].
        /// The algorithm below will pick passages 1 and 3 as chargeable with a total fee of 16kr, whereas passages 2 and 3 will give the highest fee of 26kr.
        /// Both combinations are valid as each passage is at least 1 hour apart from one another.
        /// </summary>
        /// <param name="passages">All passages</param>
        internal List<DateTime> GetChargeablePassages(DateTime[] passages)
        {
            if (passages == null || !passages.Any()) return new List<DateTime>(); // no chargeable passages available
            var chargeablePassages = new List<DateTime>();
            passages = passages.OrderBy(time => time).ToArray(); // sort passages by date and time in increasing order
            chargeablePassages.Add(passages[0]);
            for (var i = 1; i < passages.Length; i++)
            {
                var passage = passages[i];
                if (DiffInMinutes(chargeablePassages.Last(), passage) >= 60)
                {
                    chargeablePassages.Add(passage);
                }
            }
            return chargeablePassages;
        }

        private static int DiffInMinutes(DateTime startDateTime, DateTime endDateTime)
        {
            return (int) endDateTime.Subtract(startDateTime).TotalMinutes;
        }
    }
}
