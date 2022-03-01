using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator;

namespace Tests
{
    [TestClass()]
    public class TollCalculatorTests
    {
        readonly TollCalculator calculator = new TollCalculator();

        [TestMethod]
        public void weekend_should_return_zero_fee()
        {
            var res = calculator.GetTollFee(new Car(), DateTime.Parse("3/5/2022"), new[] { new TimeSpan(6, 0, 0) });

            Assert.IsTrue(res == 0);
        }

        [TestMethod]
        public void toll_fee_vehicle_should_get_zero_fee()
        {
            var res = calculator.GetTollFee(new Motorbike(), DateTime.Parse("3/1/2022"), new[] { new TimeSpan(6, 0, 0) });

            Assert.IsTrue(res == 0);
        }

        [TestMethod]
        public void ordinary_vehicle_should__not_get_zero_fee()
        {
            var firstItemOfRange = FeeRangeCollection.Range[0];

            var res = calculator.GetTollFee(new Car(), DateTime.Parse("3/1/2022"), new[] { firstItemOfRange.From });

            Assert.IsTrue(res > 0);
            Assert.IsTrue(res == firstItemOfRange.Fee);
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