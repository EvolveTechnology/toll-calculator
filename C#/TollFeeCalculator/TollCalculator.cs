using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        /// <summary>
        /// Calculates the total fee for a vehicles passages
        /// </summary>
        /// <param name="vehicle">The vehicle</param>
        /// <param name="passages">Timestamps the vehicles passed through the tolls.</param>
        /// <returns>Total fee</returns>
        public int GetTollFee(IVehicle vehicle, DateTime[] passages)
        {
            return passages
                .GroupBy(x => x.Date)
                .Sum(x => GetADaysTotalTollFee(vehicle, x.ToArray()));
        }

        /// <summary>
        /// Calculate the total toll fee for one day
        /// </summary>
        /// <param name="vehicle">The vehicle</param>
        /// <param name="passages">Timestamps the vehicles passed through the tolls.</param>
        /// <returns>Total fee</returns>
        /// <exception cref="ArgumentException">Throws argument exception if passages timestamps are on different days.</exception>
        public int GetADaysTotalTollFee(IVehicle vehicle, DateTime[] passages)
        {
            if (passages.Length == 0)
            {
                return 0;
            }
            
            if (passages.Select(x => x.Date).Distinct().Count() > 1)
            {
                throw new PassagesShouldBeOnDifferentDaysException();
            }
            
            IEnumerable<List<DateTime>> GroupPassagesByHour()
            {
                var sortedPassages = passages.OrderBy(x => x).ToList();
                var hourStart = sortedPassages[0];
                var passagesDuringAnHour = new List<DateTime> {hourStart};
                foreach (var passage in sortedPassages.Skip(1))
                {
                    if (passage.Subtract(hourStart) < TimeSpan.FromHours(1))
                    {
                        passagesDuringAnHour.Add(passage);
                    }
                    else
                    {
                        yield return passagesDuringAnHour;
                        hourStart = passage;
                        passagesDuringAnHour = new List<DateTime> {passage};
                    }
                }

                yield return passagesDuringAnHour;
            }

            var fee = GroupPassagesByHour()
                .Sum(x => x.Max(d => GetTollFee(d, vehicle)));

            return Math.Min(fee, 60);
        }
        
        /// <summary>
        /// Calculates the passage fee for a vehicle
        /// </summary>
        /// <param name="passage">The passage time stamp</param>
        /// <param name="vehicle">The vehicle</param>
        /// <returns>Toll fee</returns>
        public int GetTollFee(DateTime passage, IVehicle vehicle)
        {
            if (IsTollFreeDate(passage) || vehicle.IsTollFree()) return 0;

            bool IsBefore(string time) => 
                passage < passage.Date.Add(TimeSpan.Parse(time));

            if (IsBefore("06:00")) return 0;
            if (IsBefore("06:30")) return 8;
            if (IsBefore("07:00")) return 13;
            if (IsBefore("08:00")) return 18;
            if (IsBefore("08:30")) return 13;
            if (IsBefore("15:00")) return 8;
            if (IsBefore("15:30")) return 13;
            if (IsBefore("17:00")) return 18;
            if (IsBefore("18:00")) return 13;
            if (IsBefore("18:30")) return 8;

            return 0;
        }

        private Boolean IsTollFreeDate(DateTime date)
        {
            if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            {
                return true;
            }
            return new PublicHoliday.SwedenPublicHoliday().IsPublicHoliday(date);
        }
    }
}