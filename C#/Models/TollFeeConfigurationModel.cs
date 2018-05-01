using System.Collections.Generic;
using TollFeeCalculator.Enums;

namespace TollFeeCalculator.Models
{
    public class TollFeeConfigurationModel
    {
        public List<FreeDatesModel> FreeDates { get; set; }

        public List<VehicleTypeEnum> FreeVehicles { get; set; }

        public List<TimeDateFeeModel> FeeTimes { get; set; }
    }
}
