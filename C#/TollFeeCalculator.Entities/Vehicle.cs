using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Contracts.Vehicle;
using TollFeeCalculator.Contracts.VehicleType;

namespace TollFeeCalculator.Entities
{
	public class Vehicle: IVehicle
	{
		public VehicleType VehicleType { get; set; }

		public string GetVehicleType()
		{
			return VehicleType.ToString();
		}

		public bool IsVehicleTollFree(VehicleType vehicleType)
		{
			return TollFreeVehicleTypes.Contains(vehicleType);
		}

		public IEnumerable<VehicleType> TollFreeVehicleTypes { get; set; }
	}
}
