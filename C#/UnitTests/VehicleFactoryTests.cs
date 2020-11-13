using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class VehicleFactoryTests
    {
        private VehicleFactory _sut;

        [TestInitialize]
        public void Initialize()
        {
            
            _sut = new VehicleFactory();
            
        }

        [TestMethod]
        public void WhenRegisteredVehicleIsnotFound_returnsDefaultVehicle()
        {
            IVehicle result =_sut.GetVehicle(VehicleType.Emergency);
            result.Should().BeOfType(typeof(Vehicle));
            result.GetVehicleType().Should().Be(VehicleType.Unknown);
        }

        [TestMethod]
        public void WhenRegisteredVehicleIsFound_returnsCorrectVehicle()
        {
            var fakeAggregator = new Fake<ITollFeeAggregator>().FakedObject;
            _sut.RegisterVehicle(new Vehicle(fakeAggregator, VehicleType.Motorbike));
            _sut.RegisterVehicle(new Vehicle(fakeAggregator, VehicleType.Emergency));
            IVehicle result = _sut.GetVehicle(VehicleType.Emergency);
            result.Should().BeOfType(typeof(Vehicle));
            result.GetVehicleType().Should().Be(VehicleType.Emergency);
        }
    }
}
