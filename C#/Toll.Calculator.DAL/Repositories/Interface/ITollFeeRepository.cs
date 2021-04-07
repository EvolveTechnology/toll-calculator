using System;
using System.Threading.Tasks;

namespace Toll.Calculator.DAL.Repositories.Interface
{
    public interface ITollFeeRepository
    {
        Task<decimal> GetPassageFeeByTime(DateTime passageTime);

        Task<bool> IsTollFreeDate(DateTime passageTime);
    }
}