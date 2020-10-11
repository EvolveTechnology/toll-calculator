using Evolve.TollFeeCalculator.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Validators
{
    /// <summary>
    /// Validator Klass
    /// </summary>
    public class DateRequestValidator: AbstractValidator<VehicleAndDateRequest>
    {
        /// <summary>
        /// Contracture 
        /// </summary>
        public DateRequestValidator()
        {
            RuleFor(x => x.TollDates)
                .NotNull()
                .Must(x => x.FindAll(delegate (DateTime today) { return today.Day>= DateTime.Now.AddDays(1).Day; }).Count == 0);
               
        }      
    }
}
