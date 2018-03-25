using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toll_calculator;
using Toll_calculator.Vehicles;

namespace Toll_calculator_test {

    [TestClass]
    public class VehicleTollPolicyTest {

        private StandardVehicleTollPolicy standardVehicleTollPolicy;

        [TestInitialize]
        public void testInit() {
            standardVehicleTollPolicy = new StandardVehicleTollPolicy();
        }

        [TestCleanup]
        public void testClean() {
            standardVehicleTollPolicy = null;
        }

        [TestMethod]
        public void TestStandardVehiclePolicy() {
            IVehicle car = new Car();
            Assert.IsTrue(car.IsTollable(standardVehicleTollPolicy));

            IVehicle motorbike = new Motorbike();
            Assert.IsFalse(motorbike.IsTollable(standardVehicleTollPolicy));

            IVehicle tractor = new Tractor();
            Assert.IsFalse(tractor.IsTollable(standardVehicleTollPolicy));

            IVehicle emergency = new EmergencyVehicle();
            Assert.IsFalse(emergency.IsTollable(standardVehicleTollPolicy));

            IVehicle foreign = new ForeignVehicle();
            Assert.IsFalse(foreign.IsTollable(standardVehicleTollPolicy));

            IVehicle military = new MilitaryVehicle();
            Assert.IsFalse(military.IsTollable(standardVehicleTollPolicy));

            IVehicle diplomatic = new DiplomaticVehicle();
            Assert.IsFalse(diplomatic.IsTollable(standardVehicleTollPolicy));
        }

    }
}
