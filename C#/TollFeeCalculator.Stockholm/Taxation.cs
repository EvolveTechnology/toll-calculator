using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Contracts.TaxationTimespan;
using TollFeeCalculator.Contracts.Vehicle;
using TollFeeCalculator.Contracts.VehicleType;
using TollFeeCalculator.Sweden;

namespace TollFeeCalculator.Stockholm
{
	public class Taxation : Sweden.Taxation
	{
		private readonly Calendar _calendar = new Calendar();
		public new bool IsVehicleTollFree(VehicleType vehicleType)
		{
			return base.IsVehicleTollFree(vehicleType);
		}

		public new IEnumerable<VehicleType> TollFreeVehicleTypes => base.TollFreeVehicleTypes;

		public new IEnumerable<TaxationTimespan> TaxationTimespans { get; } = new List<TaxationTimespan>
		{
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(6,30,0),
				TimespanEnd = new TimeSpan(7,0,0),
				TimespanFee = 15
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(7,0,0),
				TimespanEnd = new TimeSpan(7,30,0),
				TimespanFee = 25
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(7,30,0),
				TimespanEnd = new TimeSpan(8,30,0),
				TimespanFee = 35
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(8,30,0),
				TimespanEnd = new TimeSpan(9,0,0),
				TimespanFee = 25
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(9,0,0),
				TimespanEnd = new TimeSpan(9,30,0),
				TimespanFee = 15
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(9,30,0),
				TimespanEnd = new TimeSpan(15,0,0),
				TimespanFee = 11
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(15,0,0),
				TimespanEnd = new TimeSpan(15,30,0),
				TimespanFee = 15
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(15,30,0),
				TimespanEnd = new TimeSpan(16,0,0),
				TimespanFee = 25
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(16,0,0),
				TimespanEnd = new TimeSpan(17,30,0),
				TimespanFee = 35
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(17,30,0),
				TimespanEnd = new TimeSpan(18,0,0),
				TimespanFee = 25
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(18,0,0),
				TimespanEnd = new TimeSpan(18,30,0),
				TimespanFee = 15
			}
		};
		public new int TimeSpanFee(DateTime dateTime)
		{
			if (_calendar.IsDateTollFree(dateTime.Date)) return 0;
			var timespan = dateTime.TimeOfDay;
			var fee = from taxation in TaxationTimespans
					  where taxation.TimespanStart <= timespan && taxation.TimespanEnd > timespan
					  select taxation.TimespanFee;
			var enumerable = fee as int[] ?? fee.ToArray();
			return enumerable.Any() ? enumerable.SingleOrDefault() : 0;
		}

		public new int MaxDailyFee { get; } = 105;
		public new int SingleChargeRuleMinutes { get; } = -1;
		public new int FeeForPassages(IVehicle vehicle, IEnumerable<DateTime> passages)
		{
			// If toll free vehicle return 0
			if (IsVehicleTollFree(vehicle.VehicleType)) return 0;
			var dateTimes = passages as DateTime[] ?? passages.ToArray();
			var passagesSorted = dateTimes.OrderBy(time => time).ToList();
			if(passagesSorted.First().Date!=passagesSorted.Last().Date)
			{
				throw new ArgumentException("Passages should be of the same day");
			}
			var totalFee = dateTimes.OrderBy(time => time).Sum(TimeSpanFee);
			return totalFee > MaxDailyFee ? MaxDailyFee : totalFee;
		}
	}
}
