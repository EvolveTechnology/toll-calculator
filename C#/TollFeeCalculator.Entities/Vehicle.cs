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
	}
}
