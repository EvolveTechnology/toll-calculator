using NUnit.Framework;
using TollFeeCalculator.Utils;

namespace TollFeeCalculator.Tests
{
    [TestFixture]
    public class CalendarUtilsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(2019, ExpectedResult = "2019-04-21")]
        [TestCase(2020, ExpectedResult = "2020-04-12")]
        [TestCase(2021, ExpectedResult = "2021-04-04")]
        [TestCase(2022, ExpectedResult = "2022-04-17")]
        [TestCase(2023, ExpectedResult = "2023-04-09")]
        [TestCase(2024, ExpectedResult = "2024-03-31")]
        [TestCase(2025, ExpectedResult = "2025-04-20")]
        [TestCase(2026, ExpectedResult = "2026-04-05")]
        [TestCase(2027, ExpectedResult = "2027-03-28")]
        [TestCase(2028, ExpectedResult = "2028-04-16")]
        public string CalculateEasterDay_Returns_Correct_Date(int year)
        {
            var easterDayDate = CalendarUtils.CalculateEasterDay(year);

            return easterDayDate.ToString("yyyy-MM-dd");
        }

        [Test]
        [TestCase(2019, ExpectedResult = "2019-06-22")]
        [TestCase(2020, ExpectedResult = "2020-06-20")]
        [TestCase(2021, ExpectedResult = "2021-06-26")]
        [TestCase(2022, ExpectedResult = "2022-06-25")]
        [TestCase(2023, ExpectedResult = "2023-06-24")]
        [TestCase(2024, ExpectedResult = "2024-06-22")]
        [TestCase(2025, ExpectedResult = "2025-06-21")]
        [TestCase(2026, ExpectedResult = "2026-06-20")]
        [TestCase(2027, ExpectedResult = "2027-06-26")]
        [TestCase(2028, ExpectedResult = "2028-06-24")]
        public string CalculateMidsummerDay_Returns_Correct_Date(int year)
        {
            var midsummerDayDate = CalendarUtils.CalculateMidsummerDay(year);

            return midsummerDayDate.ToString("yyyy-MM-dd");
        }

        [Test]
        [TestCase(2019, ExpectedResult = "2019-11-02")]
        [TestCase(2020, ExpectedResult = "2020-10-31")]
        [TestCase(2021, ExpectedResult = "2021-11-06")]
        [TestCase(2022, ExpectedResult = "2022-11-05")]
        [TestCase(2023, ExpectedResult = "2023-11-04")]
        [TestCase(2024, ExpectedResult = "2024-11-02")]
        [TestCase(2025, ExpectedResult = "2025-11-01")]
        [TestCase(2026, ExpectedResult = "2026-10-31")]
        [TestCase(2027, ExpectedResult = "2027-11-06")]
        [TestCase(2028, ExpectedResult = "2028-11-04")]
        public string CalculateAllSaintsDay_Returns_Correct_Date(int year)
        {
            var allSaintsDayDate = CalendarUtils.CalculateAllSaintsDay(year);

            return allSaintsDayDate.ToString("yyyy-MM-dd");
        }
    }
}