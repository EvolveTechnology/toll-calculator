using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using TollCalculator.Contracts.Rules;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator
{
    /// <summary>
    /// Toll fee calculator for congestion taxes.
    /// </summary>
    public class TollFeeCalculator
    {
        /// <summary>
        /// Creates toll fee calculator.
        /// </summary>
        /// <param name="tollFeeRulesRepository">Toll fee rules repository to use.</param>
        /// <param name="logger">Optional logger.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="tollFeeRulesRepository"/> is <b>null</b>.
        /// </exception>
        public TollFeeCalculator(ITollFeeRulesRepository tollFeeRulesRepository, ILogger logger)
        {
            TollFeeRulesRepository = tollFeeRulesRepository ??
                throw new ArgumentNullException(nameof(tollFeeRulesRepository));

            Logger = logger ?? NullLogger.Instance;
        }

        private ILogger Logger { get; } 

        private ITollFeeRulesRepository TollFeeRulesRepository { get; }

        /// <summary>
        /// Calculates toll fees for a specific vehicle for a number of passages through toll stations.
        /// </summary>
        /// <param name="vehicle">Vehicle to calculate tolls for.</param>
        /// <param name="passages">All passages to calculate toll for.</param>
        /// <returns>Calculation of toll fee and taxable amount in total and for each day.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="vehicle"/> is <b>null</b>.
        /// </exception>
        public TollFeeResult CalculateTollFee(Vehicle vehicle, IEnumerable<DateTime> passages)
        {
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));

            var passagesPerDay = (passages ?? Enumerable.Empty<DateTime>())
                .GroupBy(d => d.Date);

            if (!passagesPerDay.Any())
            {
                Logger.LogWarning($"No passages for vehicle:  {vehicle.RegistrationIdentifier} ({vehicle.VehicleType}).");
                return new TollFeeResult(vehicle, Enumerable.Empty<DailyTollFee>());
            }

            Logger.LogDebug($"Calculate toll fees for vehicle: {vehicle.RegistrationIdentifier} ({vehicle.VehicleType}) " +
                $"between {passagesPerDay.Min(p => p.Key).Date:d} and {passagesPerDay.Max(p => p.Key).Date:d}.");

            IDictionary<DateTime, ITollFeeRules> tollFeeRules =
                passagesPerDay.ToDictionary(p => p.Key, p => TollFeeRulesRepository.GetTollFeeRulesForDate(p.Key));

            var result = new List<DailyTollFee>();

            passagesPerDay = RemoveDaysWithNoToll(vehicle, passagesPerDay, tollFeeRules, result);

            result.AddRange(
                CalculateDailyTollFees(vehicle, passagesPerDay.SelectMany(p => p)));

            return new TollFeeResult(vehicle, result.OrderBy(daily => daily.Date));
        }

        private IEnumerable<DailyTollFee> CalculateDailyTollFees(Vehicle vehicle, IEnumerable<DateTime> passages)
        {
            if (!(passages ?? Enumerable.Empty<DateTime>()).Any())
            {
                yield break;
            }

            ITollFeeRules tollFeeRules = null;
            DateTime date = DateTime.MinValue.Date;
            int dailyPassages = 0;
            decimal dailyTotal = 0;
            decimal dailyTaxable = 0;
            (DateTime time, decimal fee) lastSingleCharge = (DateTime.MinValue, 0);

            foreach (DateTime passage in passages.OrderBy(p => p))
            {
                // Are we switching to new date?
                if (passage.Date != date && date != DateTime.MinValue.Date)
                {
                    // Then create report for previous date
                    yield return CreateDailyResult();
                }

                date = passage.Date;
                tollFeeRules = TollFeeRulesRepository.GetTollFeeRulesForDate(date);
                var fee = FindTollFeeForPassageTime(passage.TimeOfDay, tollFeeRules);
                dailyTotal += fee;

                // Ignore zero fees (they are not counted as passages)
                if (fee > 0)
                {
                    dailyPassages++;

                    // Get single charge time to use (-1 means no single charge)
                    int singleChargeMinutes = tollFeeRules.NumberOfMinutesForSingleChargeRule ?? -1;

                    // New single charge period started
                    if ((passage - lastSingleCharge.time).TotalMinutes >= singleChargeMinutes)
                    {
                        dailyTaxable += fee;
                        lastSingleCharge = (passage, fee);
                    }
                    // Still within last single charge period
                    else
                    {
                        dailyTaxable += Math.Max(0, fee - lastSingleCharge.fee);
                        lastSingleCharge = (lastSingleCharge.time, Math.Max(lastSingleCharge.fee, fee));
                    }
                }
            }

            // Create report for final date
            yield return CreateDailyResult();

            // C#7 local function to create daily result (captures variables in outer scope)
            DailyTollFee CreateDailyResult()
            {
                DailyTollFee result = new DailyTollFee(date, dailyPassages, dailyTotal,
                    Math.Min(dailyTaxable, tollFeeRules.DailyMaximumTollFee ?? decimal.MaxValue));

                dailyPassages = 0;
                dailyTotal = 0;
                dailyTaxable = 0;

                return result;
            }
        }

        private IEnumerable<IGrouping<DateTime, DateTime>> RemoveDaysWithNoToll(
            Vehicle vehicle,
            IEnumerable<IGrouping<DateTime, DateTime>> passagesPerDay,
            IDictionary<DateTime, ITollFeeRules> tollFeeRules,
            List<DailyTollFee> result)
        {
            var daysWithNoFee = passagesPerDay
                .Where(day => IsTollFree(day.Key, tollFeeRules[day.Key], vehicle, day))
                .ToDictionary(day => day.Key, day => NoDailyFee(day.Key, day.Count()));

            result.AddRange(daysWithNoFee.Values);

            return passagesPerDay.Where(d => !daysWithNoFee.ContainsKey(d.Key));
        }

        /// <summary>
        /// Calculates toll fees for a specific vehicle for a number of passages on a single day.
        /// </summary>
        /// <param name="date">Date to calculate toll fees for.</param>
        /// <param name="vehicle">Vehicle to calculate tolls for.</param>
        /// <param name="passages">All passages to calculate toll for.</param>
        /// <returns>Calculation of toll fee and taxable amount for specific day.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="vehicle"/> is <b>null</b>.
        /// </exception>
        public DailyTollFee CalculateDailyTollFee(DateTime date, Vehicle vehicle, IEnumerable<DateTime> passages)
        {
            if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));

            ITollFeeRules tollFeeRules = TollFeeRulesRepository.GetTollFeeRulesForDate(date);

            IOrderedEnumerable<DateTime> passagesForDate = GetSortedPassagesForDate(date, passages);

            if (IsTollFree(date, tollFeeRules, vehicle, passagesForDate))
            {
                return NoDailyFee(date, passagesForDate.Count());
            }

            return CalculateDailyTollFees(vehicle, passagesForDate).First();
        }

        private bool IsTollFree(DateTime date, ITollFeeRules tollFeeRules, Vehicle vehicle, IEnumerable<DateTime> passagesForDay)
        {
            if (!passagesForDay.Any())
            {
                Logger.LogInformation($"No passages for {date:d}.");
                return true;
            }

            if (tollFeeRules.IsTollFreeDate)
            {
                Logger.LogDebug($"{date:d} is toll free.");
                return true;
            }

            if (tollFeeRules.TollFreeVehicleTypes.Contains(vehicle.VehicleType))
            {
                Logger.LogDebug($"Toll fee is not applicable for {vehicle.VehicleType}.");
                return true;
            }

            if (tollFeeRules.AreNonDomesticVehiclesTollFree &&
                !tollFeeRules.DomesticIso3166CountryCodes.Contains(vehicle.Iso3166Alpha2CountryCode))
            {
                Logger.LogDebug("Toll fee is not applicable for non-domestic vehicles.");
                return true;
            }

            return false;
        }

        private static IOrderedEnumerable<DateTime> GetSortedPassagesForDate(DateTime date, IEnumerable<DateTime> passages)
        {
            var passagesForDay = passages
                .Where(passage => passage.Date == date.Date)
                .OrderBy(passage => passage);

            return passagesForDay;
        }

        private static decimal FindTollFeeForPassageTime(TimeSpan passageTime, ITollFeeRules tollFeeRules)
        {
            var result = tollFeeRules
                .TollFeeOrderedByStartTime
                .LastOrDefault(kvp => kvp.Key <= passageTime)
                .Value;

            return result;
        }

        private static DailyTollFee NoDailyFee(DateTime date, int passages)
        {
            return new DailyTollFee(date, passages, 0, 0);
        }
    }
}
