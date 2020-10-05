using Evolve.TollFeeCalculator.Extensions;
using Evolve.TollFeeCalculator.Interfaces;
using Evolve.TollFeeCalculator.Models;
using Evolve.TollFeeCalculator.Validators;
using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolve.TollFeeCalculator.Services
{
    /// <summary>
    /// Service for Calculate cost Toll
    /// </summary>
    public class TollFeeCalculatorService : ITollFeeCalculatorService
    {

        /// <summary>
        /// Beräkna den totala avgiften för en dag för ett fordon
        /// </summary>
        /// <param name="vehicleTollAndDate"></param>
        /// <returns>total cost toll fee for one day</returns>       
        public async Task<int> GetTotalTollFeeForDateAsync(VehicleAndDateRequest vehicleTollAndDate)
        {
         
            var dateRequestValidator = new DateRequestValidator();
            ValidationResult results = dateRequestValidator.Validate(vehicleTollAndDate);

            if (!results.IsValid)
            {
                var errorMessage = string.Empty;
                foreach (var failure in results.Errors)
                {
                    errorMessage = string.Join(Environment.NewLine, failure.ErrorMessage);
                }
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
        /// Räkna vägtullar för en tid.
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

