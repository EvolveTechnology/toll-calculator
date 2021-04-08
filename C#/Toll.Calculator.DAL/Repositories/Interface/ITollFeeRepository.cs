using System;
using System.Threading.Tasks;

namespace Toll.Calculator.DAL.Repositories.Interface
{
    public interface ITollFeeRepository
    {
        Task<decimal> GetPassageFeeByTimeAsync(DateTime passageTime);

        Task<bool> IsTollFreeDateAsync(DateTime passageTime);

        Task<decimal> GetMaximumDailyFeeAsync();

        Task<TimeSpan> GetPassageLeewayInterval();
    }
}