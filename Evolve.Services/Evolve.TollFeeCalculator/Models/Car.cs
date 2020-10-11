using Evolve.TollFeeCalculator.Enums;
using Evolve.TollFeeCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    ///  Vehicle class of type Car
    /// </summary>
    public class Car : IVehicle
    {               
        VehicleType IVehicle.GetVehicleType()
        {
            return VehicleType.CAR;
        }
    }
}
