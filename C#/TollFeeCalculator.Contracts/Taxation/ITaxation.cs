using System;
using System.Collections.Generic;

namespace TollFeeCalculator.Contracts.Taxation
{
	public interface ITaxation
	{
		/// <summary>
		/// Determines whether the vechicle is 
		/// of the toll free type
		/// </summary>
		/// <returns>true or false</returns>
		bool IsVehicleTollFree(VehicleType.VehicleType vehicleType);
		/// <summary>
		/// List of vehicle types that are toll free
		/// </summary>
		IEnumerable<VehicleType.VehicleType> TollFreeVehicleTypes { get; }

		/// <summary>
		/// Taxation timespans defining the fee of
		/// different timespans
		/// </summary>
		IEnumerable<TaxationTimespan.TaxationTimespan> TaxationTimespans { get; }

		/// <summary>
		/// Calculates the fee of the passage for
		/// given timestamp
		/// </summary>
		/// <param name="dateTime">The timestamp to check</param>
		/// <returns>The calculated fee</returns>
		int TimeSpanFee(DateTime dateTime);
		/// <summary>
		/// Maximum daity fee for passages
		/// </summary>
		int MaxDailyFee { get; }
		/// <summary>
		/// Number of minutes within those the most expensive
		/// fee is applied
		/// </summary>
		/// <remarks>-1 means no single charge rule</remarks>
		int SingleChargeRuleMinutes { get; }

		/// <summary>
		/// Calculates the total fee for the given
		/// list of passages
		/// </summary>
		/// <param name="vehicle">The vehicle object</param>
		/// <param name="passages">List of passages</param>
		/// <returns>Total fee for passages</returns>
		int FeeForPassages(Vehicle.IVehicle vehicle, IEnumerable<DateTime> passages);
	}
}
