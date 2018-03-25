using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toll_calculator;
using Toll_calculator.Holidays;
using Toll_calculator.Vehicles;
using Toll_calculator.Policies;
using System.Linq;

namespace Toll_calculator_test {

    [TestClass]
    public class TollCalculatorTest {

        private ITollCalculator tollCalculator;

        [TestInitialize]
        public void testInit() {
            IHolidayChecker holidayChecker = new Sweden2018HolidayChecker();
            IDateTollPolicy dateTollPolicy = new StandardDateTollPolicy(holidayChecker);
            IFeePolicy feePolicy = new StandardFeePolicy();
            IVehicleTollPolicy vehicleTollPolicy = new StandardVehicleTollPolicy();
            tollCalculator = new SimpleTollCalculator(dateTollPolicy, feePolicy, vehicleTollPolicy);
        }

        [TestCleanup]
        public void testClean() {
            tollCalculator = null;
        }

        [TestMethod]
        public void TestVehicles() {
            DateTime[] times = new DateTime[1] {
                new DateTime(2018, 3, 26, 7, 30, 0)
            };

            IVehicle car = new Car();
            Assert.AreEqual(tollCalculator.GetTollFee(car, times), 18);

            IVehicle tractor = new Tractor();
            Assert.AreEqual(tollCalculator.GetTollFee(tractor, times), 0);

            IVehicle foreign = new ForeignVehicle();
            Assert.AreEqual(tollCalculator.GetTollFee(foreign, times), 0);

            IVehicle military = new MilitaryVehicle();
            Assert.AreEqual(tollCalculator.GetTollFee(military, times), 0);
        }

        [TestMethod]
        public void TestMaxAmount() {
            DateTime[] times = new DateTime[9] {
                new DateTime(2018, 3, 26, 6, 30, 0),
                new DateTime(2018, 3, 26, 7, 30, 0),
                new DateTime(2018, 3, 26, 8, 30, 0),
                new DateTime(2018, 3, 26, 9, 30, 0),
                new DateTime(2018, 3, 26, 10, 30, 0),
                new DateTime(2018, 3, 26, 11, 30, 0),
                new DateTime(2018, 3, 26, 12, 30, 0),
                new DateTime(2018, 3, 26, 13, 30, 0),
                new DateTime(2018, 3, 26, 14, 30, 0)
            };

            IVehicle car = new Car();
            Assert.AreEqual(tollCalculator.GetTollFee(car, times), 60);
        }

        [TestMethod]
        public void TestTollFreeDays() {
            DateTime[] times = new DateTime[4] {
                new DateTime(2018, 3, 24, 7, 30, 0),
                new DateTime(2018, 3, 25, 7, 30, 0),
                new DateTime(2018, 6, 6, 7, 30, 0),
                new DateTime(2018, 5, 10, 7, 30, 0)
            };

            IVehicle car = new Car();
            Assert.AreEqual(tollCalculator.GetTollFee(car, times), 0);
        }

        [TestMethod]
        public void TestMinTollFrequency() {
            IVehicle car = new Car();
            DateTime[] times = new DateTime[2] {
                new DateTime(2018, 3, 26, 7, 30, 0),
                new DateTime(2018, 3, 26, 7, 31, 0)
            };
            Assert.AreEqual(tollCalculator.GetTollFee(car, times), 18);
        }

        [TestMethod]
        public void TestTwoDays() {
            IVehicle car = new Car();

            DateTime[] dayOne = new DateTime[3] {
                new DateTime(2018, 3, 26, 6, 30, 0),
                new DateTime(2018, 3, 26, 7, 30, 0),
                new DateTime(2018, 3, 26, 14, 30, 0)
            };
            Assert.AreEqual(tollCalculator.GetTollFee(car, dayOne), 39);

            DateTime[] dayTwo = new DateTime[2] {
                new DateTime(2018, 3, 27, 7, 30, 0),
                new DateTime(2018, 3, 27, 15, 10, 20)
            };
            Assert.AreEqual(tollCalculator.GetTollFee(car, dayTwo), 31);

            DateTime[] doubleDays = dayOne.Concat(dayTwo).ToArray();
            Assert.AreEqual(tollCalculator.GetTollFee(car, doubleDays), 70);
        }

        [TestMethod]
        public void TestBadOrder() {
            IVehicle car = new Car();
            DateTime[] doubleDays = new DateTime[6] {
                new DateTime(2018, 3, 27, 15, 10, 20),
                new DateTime(2018, 3, 26, 14, 30, 0),
                new DateTime(2018, 3, 26, 7, 31, 0),
                new DateTime(2018, 3, 26, 7, 30, 0),
                new DateTime(2018, 3, 27, 7, 30, 0),
                new DateTime(2018, 3, 26, 6, 30, 0)
            };
            Assert.AreEqual(tollCalculator.GetTollFee(car, doubleDays), 70);
        }
    }
}
