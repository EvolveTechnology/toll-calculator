using Evolve.TollFeeCalculator.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.TollFeeCalculator.Interfaces
{
    /// <summary>
    /// interface for Calculate cost Toll
    /// </summary>
    public interface ITollFeeCalculatorService
    {
        /// <summary>
        /// Calculate the total toll fee for one day for a vehicle
        /// </summary>
        /// <param name="vehicleTollAndDateDto"></param>      
        /// <returns></returns>
        Task<int> GetTotalTollFeeForDateAsync(VehicleAndDateRequest vehicleTollAndDateDto);
    }
}
