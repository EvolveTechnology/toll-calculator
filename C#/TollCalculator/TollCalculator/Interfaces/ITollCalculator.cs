using System;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator.Interfaces
{
    public interface ITollCalculator
    {
        decimal GetDailyTollFee(IVehicle vehicle, DateTime[] dates);
    }
}
