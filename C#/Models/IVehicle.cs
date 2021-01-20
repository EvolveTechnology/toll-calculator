using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollCalculatorApp.Models
{
    public interface IVehicle
    {
        VehiclesType VehicleType { get; }
    }
}