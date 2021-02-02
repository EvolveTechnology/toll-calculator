using System;

namespace TollFeeCalculator.Models.ViewModels
{
    public class TollFeeViewModel
    {
        public string VehicleId { get; set; }

        public string VehicleType { get; set; }

        public DateTime Date { get; set; }
    }
}
