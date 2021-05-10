using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{
    public class TollCalculator
    {
        private readonly ITollTariff tollTariff;
        public TollCalculator(ITollTariff tariff)
        {
            tollTariff = tariff;
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
            if (dates.Any(dt => dates.First().Date != dt.Date)) throw new ArgumentException("Passages at different dates");
            return CalculateOptimalTollFee(vehicle, dates.Where(d => tollTariff.GetTollFee(d, vehicle) > 0).ToList()); //.ToList());
            return CalculateOptimalTollFee(vehicle, Filter(dates, vehicle).ToList());
        }

        private IEnumerable<DateTime> Filter(IEnumerable<DateTime> dates, IVehicle vehicle, double resolution = 5.0)
        {
            var date = dates.First().Date;
            for (var minute = 0.0; minute < 24 * 60; minute += resolution)
            {
                var dt = date.AddMinutes(minute);
                var passages = dates.Where(p => Math.Abs((p - dt).TotalMinutes) < resolution / 2 && tollTariff.GetTollFee(p, vehicle) > 0);
                if (!passages.Any()) continue;

                //return passage with highest toll fee
                yield return passages.Aggregate((max, x) => tollTariff.GetTollFee(x, vehicle) > tollTariff.GetTollFee(max, vehicle) ? x : max);
            }
        }

        private int CalculateOptimalTollFee(IVehicle vehicle, IEnumerable<DateTime> passages)
        {
            var totalFee = 0;
            foreach (var passage in passages)
            {
                var filteredPassages = passages.Where(pass => Math.Abs((passage - pass).TotalMinutes) > tollTariff.TollIntervalInMinutes);
                var tempFee =
                    tollTariff.GetTollFee(passage, vehicle) +
                    CalculateOptimalTollFee(vehicle, filteredPassages);
                totalFee = Math.Max(totalFee, tempFee);
                if (totalFee >= tollTariff.MaxFeePerDay) return tollTariff.MaxFeePerDay;
            }
            return totalFee;
        }
    }
}