using System;
using System.Collections.Generic;
using System.Linq;
using Toll.Calculator.Domain;
using Toll.Calculator.Infrastructure.CustomExceptions;

namespace Toll.Calculator.WebAPI.ApiModels
{
    public class TotalFeeRequestModel
    {
        public Vehicle VehicleType { get; set; }
        public string PassageDates { get; set; }

        public List<DateTime> GetPassageDateTimes()
        {
            var stringDates = PassageDates.Split(";");

            try
            {
                return stringDates.Select(stringDate => DateTime.Parse(stringDate)).ToList();
            }
            catch (Exception)
            {
                throw new DateFormatException();
            }
        }
    }
}