using Evolve.TollFeeCalculator.Extensions;
using Evolve.TollFeeCalculator.Interfaces;
using Evolve.TollFeeCalculator.Models;
using Evolve.TollFeeCalculator.Validators;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Evolve.TollFeeCalculator.Services
{
    /// <summary>
    /// Service for Calculate cost Toll
    /// </summary>
    public class TollFeeCalculatorService : ITollFeeCalculatorService
    {
        IValidator<VehicleAndDateRequest> _valadator;
        ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valadator"></param>
        public TollFeeCalculatorService(IValidator<VehicleAndDateRequest> valadator, ILogger<TollFeeCalculatorService> logger)
        {
            _valadator = valadator;
            _logger = logger;
        }
        /// <summary>
        /// calculat the total charge for one day for a vehicle
        /// </summary>
        /// <param name="vehicleTollAndDate"></param>
        /// <returns>total cost toll fee for one day</returns>       
        public async Task<int> GetTotalTollFeeForDateAsync(VehicleAndDateRequest vehicleTollAndDate)
        {
            _logger.LogInformation("GetTotalTollFeeForDateAsync");
            var results= _valadator.Validate(vehicleTollAndDate);

             if (!results.IsValid)
             {
                 var errorMessage = string.Empty;
                 foreach (var failure in results.Errors)
                 {
                     errorMessage = string.Join(Environment.NewLine, failure.ErrorMessage);
                 }
                _logger.LogDebug(errorMessage);
                throw new Exception(errorMessage);
             }

            var intervalStart = vehicleTollAndDate.TollDates[0];
            var totalFee = 0;

            foreach (DateTime date in vehicleTollAndDate.TollDates)
            {
                int nextFee = await GetTollFeeAsync(date, vehicleTollAndDate.Vehicle);
                int tempFee = await GetTollFeeAsync(intervalStart, vehicleTollAndDate.Vehicle);
                TimeSpan span = date - intervalStart;
                var diffInMillies = (long)span.TotalMilliseconds;
                var minutes = diffInMillies / 1000 / 60;

                if (minutes <= Globals.AppConfiguration.CostParameters.MaxDiffInMinutes)
                {
                    if (totalFee > 0) totalFee -= tempFee;
                    if (nextFee >= tempFee) tempFee = nextFee;
                    totalFee += tempFee;
                }
                else
                {
                    totalFee += nextFee;
                }
            }
            var MaxtotalCost = Globals.AppConfiguration.CostParameters.MaxtotalCost;
            if (totalFee > MaxtotalCost) totalFee = MaxtotalCost;
            return await Task.FromResult(totalFee);
        }
        /// <summary>
        /// Calculat tolls for a specific time
        /// </summary>
        /// <param name="date"></param>
        /// <param name="vehicle"></param>
        /// <returns></returns>
        private async Task<int> GetTollFeeAsync(DateTime date, IVehicle vehicle)
        {
            if (await vehicle.IsTollFreeVehicleAsync() || await date.IsTollFreeDateAsync()) return await new ValueTask<int>(0);
            var costTime = new CostTime(date.Hour, date.Minute);
            return await costTime.GetAmountOfTimeAsync();
        }

    }
}

