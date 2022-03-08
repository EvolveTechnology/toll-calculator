using System;

namespace Evolve.TollCalculator.API.Models
{
    public class TollCalculateRequestModel
    {
        public string Vehicle { get; set; }
        public DateTime[] TollDates { get; set; }
    }
}
