using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Contracts.Taxation;
using TollFeeCalculator.Contracts.TaxationTimespan;
using TollFeeCalculator.Contracts.VehicleType;

namespace TollFeeCalculator.Gothenburg
{
	public class Taxation : ITaxation
	{
		public bool IsVehicleTollFree(VehicleType vehicleType)
		{
			throw new NotImplementedException("Implemented in country specific class.");
		}

		public IEnumerable<VehicleType> TollFreeVehicleTypes { get; } = null;
		public IEnumerable<TaxationTimespan> TaxationTimespans { get; } = new List<TaxationTimespan>
		{
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(6,0,0),
				TimespanEnd = new TimeSpan(6,29,0),
				TimespanFee = 9.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(6,30,0),
				TimespanEnd = new TimeSpan(6,59,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(7,0,0),
				TimespanEnd = new TimeSpan(7,59,0),
				TimespanFee = 22.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(8,0,0),
				TimespanEnd = new TimeSpan(8,29,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(8,30,0),
				TimespanEnd = new TimeSpan(14,59,0),
				TimespanFee = 9.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(15,00,0),
				TimespanEnd = new TimeSpan(15,29,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(15,30,0),
				TimespanEnd = new TimeSpan(16,59,0),
				TimespanFee = 22.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(17,0,0),
				TimespanEnd = new TimeSpan(17,59,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(18,0,0),
				TimespanEnd = new TimeSpan(18,29,0),
				TimespanFee = 9.0f
			}
		};
		public float TimeSpanFee(DateTime dateTime)
		{
			var timespan = dateTime.TimeOfDay;
			var fee = from taxation in TaxationTimespans
					  where taxation.TimespanStart <= timespan && taxation.TimespanEnd >= timespan
					  select taxation.TimespanFee;
			var enumerable = fee as float[] ?? fee.ToArray();
			return enumerable.Any() ? enumerable.SingleOrDefault() : 0.0f;
		}

		public float MaxDailyFee { get; } = 60.0f;
		public int SingleChargeRuleMinutes { get; } = 60;
		public float FeeForPassages(IEnumerable<DateTime> passages)
		{
			var previousPassageFee = 0.0f;
			var previousPassage = DateTime.MinValue;
			var firstPassage = true;
			var totalFee = 0.0f;
			foreach (var passage in passages.OrderBy(time => time))
			{
				var fee = TimeSpanFee(passage);
				if (firstPassage)
				{
					totalFee = fee;
				}
				else
				{
					if ((passage - previousPassage).Minutes <= SingleChargeRuleMinutes)
					{
						totalFee += Math.Max(fee, previousPassageFee);
					}
					else
					{
						totalFee += fee;
					}
				}

				previousPassage = passage;
				previousPassageFee = fee;
				firstPassage = false;
			}
			return totalFee > MaxDailyFee ? MaxDailyFee : totalFee;
		}
	}
}
