using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Toll.Calculator.Domain;

namespace Toll.Calculator.DAL
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetTollFreeVehicles();
    }
}
