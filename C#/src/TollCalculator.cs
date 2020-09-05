using System;
using System.Linq;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        private const int MaximumTollFee = 60;
        private readonly TollFee _tollFee;

        public TollCalculator(TollFee tollFee)
        {
            _tollFee = tollFee;
        }

        /**
        * Calculate the total toll fee for one day
        *
        * @param vehicle - the vehicle
        * @param dates   - date and time of all passes on one day
        * @return - the total toll fee for that day
        */

        public int GetTollFee(IVehicle vehicle, DateTime[] dates)
        {
            if (vehicle.IsTollFree())
            {
                return 0;
            }

            var totalFee = 0;
            var datesLeft = dates.ToList();
            while (datesLeft.Any())
            {
                var initialDate = datesLeft.First();
                var datesWithinRange = datesLeft.Where(date => date >= initialDate && date < initialDate.AddHours(1));
                datesLeft = datesLeft.Except(datesWithinRange).ToList();

                var highestTollFeeInRange = datesWithinRange
                    .Select(date => _tollFee.GetFeeForDate(date))
                    .Max();
                
                totalFee += highestTollFeeInRange;
            }

            return totalFee > MaximumTollFee
                ? MaximumTollFee
                : totalFee;
        }
    }
}