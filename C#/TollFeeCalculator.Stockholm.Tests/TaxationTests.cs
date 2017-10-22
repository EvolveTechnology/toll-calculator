﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TollFeeCalculator.Contracts.VehicleType;
using TollFeeCalculator.Entities;

namespace TollFeeCalculator.Stockholm.Tests
{
	[TestClass]
	public class TaxationTests
	{
		private Taxation _taxation;
		[TestInitialize]
		public void TestInitialize()
		{
			ConfigurationManager.AppSettings["DateCheckAPIBaseAddress"] = "https://api.dryg.net";
			ConfigurationManager.AppSettings["DateCheckAPIPathAndQuery"] = "/dagar/v1/?datum=";
			_taxation = new Taxation();
		}

		[TestMethod]
		public void MilitaryVehicleIsTollFree()
		{
			var militaryVehicle = new Vehicle
			{
				VehicleType = VehicleType.Military
			};

			Assert.AreEqual(0.0f, _taxation.FeeForPassages(militaryVehicle, new List<DateTime>()));
		}

		[TestMethod]
		public void MaxTollIs105SEK()
		{
			var vehicle = new Vehicle { VehicleType = VehicleType.Car };
			var passages = new List<DateTime>
			{
				new DateTime(2017,10,20,6,0,0),
				new DateTime(2017,10,20,7,1,0),
				new DateTime(2017,10,20,8,2,0),
				new DateTime(2017,10,20,9,3,0),
				new DateTime(2017,10,20,10,4,0),
				new DateTime(2017,10,20,12,5,0),
				new DateTime(2017,10,20,13,5,0),
				new DateTime(2017,10,20,14,5,0),
				new DateTime(2017,10,20,15,5,0)
			};

			Assert.AreEqual(105.0f, _taxation.FeeForPassages(vehicle, passages));
		}

		[TestMethod]
		public void NoSingleCharge()
		{
			var vehicle = new Vehicle { VehicleType = VehicleType.Car };
			var passages = new List<DateTime>
			{
				new DateTime(2017,10,20,7,0,0),
				new DateTime(2017,10,20,7,10,0),
				new DateTime(2017,10,20,7,15,0)
			};

			Assert.AreEqual(75.0f, _taxation.FeeForPassages(vehicle, passages));
		}
	}
}
