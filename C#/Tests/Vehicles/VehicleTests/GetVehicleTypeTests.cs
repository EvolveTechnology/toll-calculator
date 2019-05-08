using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator.Vehicles;

namespace TollCalculatorTests
{
    public class VehiclesTests
    {
        private static IEnumerable<TestCaseData> Vehicles 
        {
            get
            {
                yield return new TestCaseData(new Motorbike(), "Motorbike");
                yield return new TestCaseData(new Tractor(), "Tractor");
                yield return new TestCaseData(new Emergency(), "Emergency");
                yield return new TestCaseData(new Diplomat(), "Diplomat");
                yield return new TestCaseData(new Foreign(), "Foreign");
                yield return new TestCaseData(new Military(), "Military");
                yield return new TestCaseData(new Car(), "Car");
            }
        }

        [TestCaseSource(nameof(Vehicles))]
        public void It_shall_return_VehicleType(IVehicle vehicle, string expected)
        {
            var result = vehicle.GetVehicleType();
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
