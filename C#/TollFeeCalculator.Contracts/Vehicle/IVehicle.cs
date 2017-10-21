using System.Collections.Generic;
namespace TollFeeCalculator.Contracts.Vehicle
{
    public interface IVehicle
    {
		/// <summary>
		/// Type of the vechicle
		/// </summary>
		VehicleType.VehicleType VehicleType { get; set; }
		/// <summary>
		/// Get the string representation of the vechicle type
		/// </summary>
		string GetVehicleType();
    }
}