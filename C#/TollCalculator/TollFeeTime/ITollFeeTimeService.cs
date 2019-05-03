using System.Collections.Generic;

namespace TollFeeCalculator.TollFeeTime
{
    public interface ITollFeeTimeService
    {
        List<FeeTime> GetTollFeeTimes();
    }
}