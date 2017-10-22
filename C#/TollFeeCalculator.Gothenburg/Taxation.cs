﻿using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Contracts.TaxationTimespan;
using TollFeeCalculator.Contracts.Vehicle;
using TollFeeCalculator.Contracts.VehicleType;
using TollFeeCalculator.Sweden;

namespace TollFeeCalculator.Gothenburg
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
				TimespanStart = new TimeSpan(6,0,0),
				TimespanEnd = new TimeSpan(6,30,0),
				TimespanFee = 9.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(6,30,0),
				TimespanEnd = new TimeSpan(7,0,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(7,0,0),
				TimespanEnd = new TimeSpan(8,0,0),
				TimespanFee = 22.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(8,0,0),
				TimespanEnd = new TimeSpan(8,30,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(8,30,0),
				TimespanEnd = new TimeSpan(15,0,0),
				TimespanFee = 9.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(15,00,0),
				TimespanEnd = new TimeSpan(15,30,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(15,30,0),
				TimespanEnd = new TimeSpan(17,0,0),
				TimespanFee = 22.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(17,0,0),
				TimespanEnd = new TimeSpan(18,0,0),
				TimespanFee = 16.0f
			},
			new TaxationTimespan
			{
				TimespanStart = new TimeSpan(18,0,0),
				TimespanEnd = new TimeSpan(18,30,0),
				TimespanFee = 9.0f
			}
		};
		public new float TimeSpanFee(DateTime dateTime)
		{
			if (_calendar.IsDateTollFree(dateTime.Date)) return 0.0f;
			var timespan = dateTime.TimeOfDay;
			var fee = from taxation in TaxationTimespans
					  where taxation.TimespanStart <= timespan && taxation.TimespanEnd > timespan
					  select taxation.TimespanFee;
			var enumerable = fee as float[] ?? fee.ToArray();
			return enumerable.Any() ? enumerable.SingleOrDefault() : 0.0f;
		}

		public new float MaxDailyFee { get; } = 60.0f;
		public new int SingleChargeRuleMinutes { get; } = 60;
		public new float FeeForPassages(IVehicle vehicle, IEnumerable<DateTime> passages)
		{
			if (IsVehicleTollFree(vehicle.VehicleType)) return 0.0f;
			var firstPassageWithinXMinutes = DateTime.MinValue;
			var previousPassageFee = 0.0f;
			var firstPassage = true;
			var totalFee = 0.0f;
			var maxOfPassagesWithinXMinutes = 0.0f;
			var dateTimes = passages as DateTime[] ?? passages.ToArray();
			var lastPassage = dateTimes.Last();
			foreach (var passage in dateTimes.OrderBy(time => time))
			{
				var fee = TimeSpanFee(passage);
				if (firstPassage)
				{
					//totalFee = fee;
					firstPassageWithinXMinutes = passage;
					maxOfPassagesWithinXMinutes = fee;
				}
				else
				{
					if ((passage - firstPassageWithinXMinutes).TotalMinutes <= SingleChargeRuleMinutes)
					{
						maxOfPassagesWithinXMinutes = Math.Max(fee, previousPassageFee);
					}
					else
					{
						totalFee += maxOfPassagesWithinXMinutes;
						firstPassageWithinXMinutes = passage;
						maxOfPassagesWithinXMinutes = fee;
					}
				}
				if (passage == lastPassage) totalFee += maxOfPassagesWithinXMinutes;
				previousPassageFee = fee;
				firstPassage = false;
			}
			return totalFee > MaxDailyFee ? MaxDailyFee : totalFee;
		}
	}
}
