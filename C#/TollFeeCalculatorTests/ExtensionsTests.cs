using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TollFeeCalculator.Tests
{
    [TestClass()]
    public class ExtensionsTests
    {
        [DataRow("fake_string", false)]
        [DataRow("Tractor", true)]
        [DataRow("Car", false)]
        [DataTestMethod]
        public void check_enum_instance(string str, bool result)
        {
            var res = str.IsTypeOfAnEnum<TollFreeVehicles>();

            Assert.IsTrue(res == result);
        }

        [DataRow("3/1/2022", false)]
        [DataRow("3/5/2022", true)] // saturday
        [DataRow("3/6/2022", true)] // sunday
        [DataTestMethod]
        public void check_weekend(string dateTimeStr, bool result)
        {
            var res = DateTime.Parse(dateTimeStr).IsWeekendOrHoliday();

            Assert.IsTrue(res == result);
        }

        [TestMethod]
        public void car_vehicle_should_not_be_toll_free()
        {
            var vehicle = new Car();

            var res = vehicle.IsTollFreeVehicle();

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void motorbike_vehicle_should_be_toll_free()
        {
            var vehicle = new Motorbike();

            var res = vehicle.IsTollFreeVehicle();

            Assert.IsTrue(res);
        }

        [DataRow(0)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataTestMethod]
        public void fee_of_an_item_should_be_equal_to_its_actual_value(int index)
        {
            var item = FeeRangeCollection.Range[index];

            var fee = item.From.GetFeeOfSpecificTime();

            Assert.IsTrue(fee == item.Fee);
        }
    }

    internal class Motorbike : Vehicle
    {
        public string GetVehicleType()
        {
            return "Motorbike";
        }
    }
}