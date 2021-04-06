using System;
using System.Collections.Generic;
using Toll.Calculator.Domain;

namespace Toll.Calculator.WebAPI.ApiModels
{
    public class TotalFeeRequestModel
    {
        public Vehicle VehicleType { get; set; }
        public List<DateTime> PassageDates { get; set; }
    }
}