using System.Collections.Generic;
using System.Threading.Tasks;
using Toll.Calculator.Domain;

namespace Toll.Calculator.DAL.Interface
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetTollFreeVehicles();
    }
}