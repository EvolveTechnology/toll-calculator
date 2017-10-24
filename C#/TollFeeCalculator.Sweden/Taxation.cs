using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Contracts.Taxation;
using TollFeeCalculator.Contracts.TaxationTimespan;
using TollFeeCalculator.Contracts.Vehicle;
using TollFeeCalculator.Contracts.VehicleType;

namespace TollFeeCalculator.Sweden
{
	/// <summary>
	/// Implementation specific for Sweden
	/// </summary>
	public class Taxation : ITaxation
	{
		public bool IsVehicleTollFree(VehicleType vehicleType)
		{
			return TollFreeVehicleTypes.Contains(vehicleType);
		}

		public IEnumerable<VehicleType> TollFreeVehicleTypes { get; } = new List<VehicleType>
		{
			VehicleType.Bus,
			VehicleType.Diplomat,
			VehicleType.EmergencyVehicle,
			VehicleType.Military,
			VehicleType.Motorbike
		};

		public IEnumerable<TaxationTimespan> TaxationTimespans { get; } = null;
		public int TimeSpanFee(DateTime dateTime)
		{
			throw new NotImplementedException("Implemented in city specific class.");
		}

		public int MaxDailyFee { get; } = 0;
		public int SingleChargeRuleMinutes { get; } = -1;
		public int FeeForPassages(IVehicle vehicle, IEnumerable<DateTime> passages)
		{
			throw new NotImplementedException("Implemented in city specific class.");
		}
	}
}
