using System.Collections.Generic;

namespace TollFeeCalculator.TollFeeTime
{
    public interface ITollFeeTimeRepository
    {
        List<FeeTime> GetAllTollFeeTimes();
    }
}