using System.Collections.Generic;
using System.Threading.Tasks;
using Toll.Calculator.Domain;

namespace Toll.Calculator.DAL.Repositories.Interface
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetTollFreeVehiclesAsync();
    }
}