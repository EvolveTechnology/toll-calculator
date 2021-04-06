using System.Collections.Generic;
using Toll.Calculator.Domain;

namespace Toll.Calculator.DAL.Interface
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetTollFreeVehicles();
    }
}
