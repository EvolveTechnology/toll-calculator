using NUnit.Framework;
using System;
using System.Collections.Generic;
using TollFeeCalculator.Models;

namespace TollFeeCalculator.Tests
{
    [TestFixture]
    public class TollCalculatorTests
    {
        private TollCalculator _tollCalculator;
        private static DateTime _goodFridayDate = new DateTime(2021, 4, 2, 8, 0, 0); // Långfredag
        private static DateTime _tollFreeDateButNotTollFreeTime = new DateTime(2021, 1, 1, 8, 0, 0);
        private static DateTime _notTollFreeDateAndNotTollFreeTime = new DateTime(2021, 1, 4, 7, 0, 0);
        private static DateTime _notTollFreeDateButTollFreeTime = new DateTime(2021, 1, 4, 20, 0, 0);
        private static IVehicle _tollFreeVehicle = new Motorbike();
        private static IVehicle _notTollFreeVehicle = new Car();

        [SetUp]
        public void Setup()
        {
            _tollCalculator = new TollCalculator();
        }

        private static IEnumerable<TestCaseData> TollFreeVehicleTypesTestData
        {
            get
            {
                yield return new TestCaseData(_tollFreeVehicle, true);
                yield return new TestCaseData(_notTollFreeVehicle, false);
            }
        }

        private static IEnumerable<TestCaseData> TollFreeDatesTestData
        {
            get
            {
                yield return new TestCaseData(_tollFreeDateButNotTollFreeTime, true);
                yield return new TestCaseData(_notTollFreeDateAndNotTollFreeTime, false);
            }
        }

        private static IEnumerable<TestCaseData> DatesAtDifferentRushHours
        {
            get
            {
                yield return new TestCaseData(_notTollFreeDateAndNotTollFreeTime, 18); // HighRush
                yield return new TestCaseData(_notTollFreeDateAndNotTollFreeTime.AddHours(2), 8); // LowRush
                yield return new TestCaseData(_notTollFreeDateAndNotTollFreeTime.AddHours(8), 13); // MediumRush
                yield return new TestCaseData(_notTollFreeDateAndNotTollFreeTime.AddHours(12), 0); // NoRush
            }
        }

        [Test]
        [TestCaseSource(nameof(TollFreeVehicleTypesTestData))]
        public void Total_Fee_Is_Zero_For_TollFree_Vehicles(IVehicle vehicle, bool expectedResult)
        {
            var dates = new DateTime[] { _notTollFreeDateAndNotTollFreeTime };

            var totalFee = _tollCalculator.GetTotalTollFeeForDay(vehicle, dates);
            var feeIsZero = totalFee == 0;

            Assert.AreEqual(feeIsZero, expectedResult);
        }

        [Test]
        [TestCaseSource(nameof(TollFreeDatesTestData))]
        public void Total_Fee_Is_Zero_For_TollFree_Dates(DateTime date, bool expectedResult)
        {
            var vehicle = _notTollFreeVehicle;
            var dates = new DateTime[] { date };

            var totalFee = _tollCalculator.GetTotalTollFeeForDay(vehicle, dates);
            var feeIsZero = totalFee == 0;

            Assert.AreEqual(feeIsZero, expectedResult);
        }

        [Test]
        public void Total_Fee_Is_Zero_For_GoodFriday_Date()
        {
            var vehicle = _notTollFreeVehicle;
            var dates = new DateTime[]
            {
                _goodFridayDate,
            };

            var totalFee = _tollCalculator.GetTotalTollFeeForDay(vehicle, dates);

            Assert.AreEqual(0, totalFee);
        }

        [Test]
        public void Maximum_Fee_Is_Sixty_For_One_Day()
        {
            var vehicle = _notTollFreeVehicle;
            var dates = new DateTime[]
            {
                _notTollFreeDateAndNotTollFreeTime,              // 07:00 HighRush    18 SEK
                _notTollFreeDateAndNotTollFreeTime.AddHours(2),  // 09:00 LowRush      8 SEK
                _notTollFreeDateAndNotTollFreeTime.AddHours(4),  // 11:00 LowRush      8 SEK
                _notTollFreeDateAndNotTollFreeTime.AddHours(6),  // 13:00 LowRush      8 SEK
                _notTollFreeDateAndNotTollFreeTime.AddHours(8),  // 15:00 MediumRush  13 SEK
                _notTollFreeDateAndNotTollFreeTime.AddHours(10), // 17:00 MediumRush  13 SEK
            };                                                   //            Total: 68 SEK

            var totalFee = _tollCalculator.GetTotalTollFeeForDay(vehicle, dates);

            Assert.AreEqual(60, totalFee);
        }

        [Test]
        [TestCaseSource(nameof(DatesAtDifferentRushHours))]
        public void Fees_Will_Differ_Depending_On_Time_Of_Day(DateTime date, int expectedResult)
        {
            var vehicle = _notTollFreeVehicle;
            var dates = new DateTime[] { date };

            var totalFee = _tollCalculator.GetTotalTollFeeForDay(vehicle, dates);

            Assert.AreEqual(expectedResult, totalFee);
        }

        [Test]
        public void Only_Highest_Fee_Applies_For_Multiple_Fees_Within_Same_Hour()
        {
            var vehicle = _notTollFreeVehicle;
            var dates = new DateTime[]
            {
                _notTollFreeDateAndNotTollFreeTime.AddMinutes(45), // 07:45 HighRush   18 SEK
                _notTollFreeDateAndNotTollFreeTime.AddMinutes(75), // 08:15 MediumRush 13 SEK
            };

            var totalFee = _tollCalculator.GetTotalTollFeeForDay(vehicle, dates);

            Assert.AreEqual(18, totalFee);
        }

        [Test]
        public void Large_Set_Of_Fees_Returns_Correct_Total_Fee()
        {
            var vehicle = _notTollFreeVehicle;
            var dates = new DateTime[]
            {
                _notTollFreeDateAndNotTollFreeTime,                       // 07:00 HighRush   18 SEK
                _notTollFreeDateAndNotTollFreeTime.AddMinutes(15),        // 07:15 HighRush    0 SEK (Too soon after previous fee) 
                _notTollFreeDateAndNotTollFreeTime.AddMinutes(75),        // 08:15 MediumRush 13 SEK
                _notTollFreeDateAndNotTollFreeTime.AddHours(6),           // 13:00 LowRush     8 SEK
                _notTollFreeDateAndNotTollFreeTime.AddHours(8),           // 15:00 MediumRush  0 SEK (Next fee is higher and within an hour)
                _notTollFreeDateAndNotTollFreeTime.AddMinutes(8*60 + 45), // 15:45 HighRush   18 SEK
                _notTollFreeDateButTollFreeTime,                          // 20:00 NoRush      0 SEK
                _notTollFreeDateButTollFreeTime.AddHours(2),              // 22:00 NoRush      0 SEK
            };                                                            // Total:           57 SEK

            var totalFee = _tollCalculator.GetTotalTollFeeForDay(vehicle, dates);

            Assert.AreEqual(57, totalFee);
        }
    }
}
