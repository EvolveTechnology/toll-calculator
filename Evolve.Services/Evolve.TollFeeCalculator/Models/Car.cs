using Evolve.TollFeeCalculator.Enums;
using Evolve.TollFeeCalculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    ///  klass för bil som Vehicle type
    /// </summary>
    public class Car : IVehicle
    {
        /// <summary>
        /// Vehicle RegNo
        /// </summary>
        public string RegNo { get; set; }
        /// <summary>
        /// Vehicle type
        /// </summary>
        /// <returns></returns>
        VehicleType IVehicle.GetVehicleType()
        {
            return VehicleType.CAR;
        }
    }
}
