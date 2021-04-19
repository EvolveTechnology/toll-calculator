using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;
using TollFeeCalculator.Vehicles;
using Xunit;

namespace TollFeeCalculator.Test
{
    public class TollCalculatorTests
    {
        [Theory]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "00:00", "05:59", 0)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "06:00", "06:29", 8)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "06:30", "07:00", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "07:00", "07:59", 18)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "08:00", "08:29", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "08:30", "14:59", 8)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "15:00", "15:29", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "15:30", "16:59", 18)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "17:00", "17:59", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "18:00", "18:29", 8)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "18:30", "23:59", 0)]
        public void GetTollFee_PassagesEveryMinuteByACarOnAWorkingDay_CorrectFee(DateTime passage, int expectedFee)
        {
            var fee = new TollCalculator().GetTollFee(passage, new Car());
            fee.ShouldBe(expectedFee);
        }
        
        [Theory]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "00:00", "05:59", 0)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "06:00", "06:29", 8)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "06:30", "07:00", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "07:00", "07:59", 18)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "08:00", "08:29", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "08:30", "14:59", 8)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "15:00", "15:29", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "15:30", "16:59", 18)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "17:00", "17:59", 13)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "18:00", "18:29", 8)]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2000-01-03", "18:30", "23:59", 0)]
        public void GetTollFee_PassagesEveryMinuteByATruckOnAWorkingDay_CorrectFee(DateTime passage, int expectedFee)
        {
            var fee = new TollCalculator().GetTollFee(passage, new Truck());
            fee.ShouldBe(expectedFee);
        }
        
        
        
        [Theory]
        [MemberData(nameof(ExpectedFeesForPassageRange), VehicleType.Diplomat, "2002-01-04", "00:00", "23:59", 0)]
        [MemberData(nameof(ExpectedFeesForPassageRange), VehicleType.Emergency, "2002-01-04", "00:00", "23:59", 0)]
        [MemberData(nameof(ExpectedFeesForPassageRange), VehicleType.Foreign, "2002-01-04", "00:00", "23:59", 0)]
        [MemberData(nameof(ExpectedFeesForPassageRange), VehicleType.Military, "2002-01-04", "00:00", "23:59", 0)]
        [MemberData(nameof(ExpectedFeesForPassageRange), VehicleType.Motorbike, "2002-01-04", "00:00", "23:59", 0)]
        [MemberData(nameof(ExpectedFeesForPassageRange), VehicleType.Tractor, "2002-01-04", "00:00", "23:59", 0)]
        public void GetTollFee_PassagesEveryMinuteByANoFeeVehicleOnAWorkingDay_NoFee(IVehicle vehicle, DateTime passage, int expectedFee)
        {
            var actual = new TollCalculator().GetTollFee(passage, vehicle);
            actual.ShouldBe(expectedFee);
        }
       
        [Theory]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2002-01-01", "00:00", "23:59", 0)]
        public void GetTollFee_PassagesEveryMinuteByACarOnAHoliday_NoFee(DateTime passage, int expectedFee)
        {
            var actual = new TollCalculator().GetTollFee(passage, new Car());
            actual.ShouldBe(expectedFee);
        }
        
        [Theory]
        [MemberData(nameof(ExpectedFeesForPassageRange), "2002-01-01", "00:00", "23:59", 0)]
        public void GetTollFee_PassagesEveryMinuteByATruckOnAHoliday_NoFee(DateTime passage, int expectedFee)
        {
            var actual = new TollCalculator().GetTollFee(passage, new Truck());
            actual.ShouldBe(expectedFee);
        }
        
        public static IEnumerable<object[]>ExpectedFeesForPassageRange(string date, string fromTime, string toTime, int fee)
        {
            var currentTime = DateTime.Parse($"{date}T{fromTime}");
            var to = DateTime.Parse($"{date}T{toTime}");

            do
            {
                yield return new object[] {currentTime, fee};
                currentTime = currentTime.AddMinutes(1);
            } while (currentTime < to);
        }
        
        public static IEnumerable<object[]>ExpectedFeesForPassageRange(VehicleType type, string date, string fromTime, string toTime, int fee)
        {
            var currentTime = DateTime.Parse($"{date}T{fromTime}");
            var to = DateTime.Parse($"{date}T{toTime}");

            do
            {
                yield return new object[] {VehicleFactory.Create(type), currentTime, fee};
                currentTime = currentTime.AddMinutes(1);
            } while (currentTime < to);
        }
        
        [Theory]
        [InlineData(VehicleType.Car)]
        [InlineData(VehicleType.Truck)]
        public void GetTollFeePerDay_NonTollFreeVehicleZeroPassages_ZeroFee(VehicleType type)
        {
            var vehicle = VehicleFactory.Create(type);
            var fee = new TollCalculator().GetADaysTotalTollFee(vehicle, new DateTime[] { });
            fee.ShouldBe(0);
        }
        
        [Theory]
        [InlineData(VehicleType.Car, "2002-01-05T07:00")]
        [InlineData(VehicleType.Car, "2002-01-06T07:00")]
        [InlineData(VehicleType.Truck, "2002-01-05T07:00")]
        [InlineData(VehicleType.Truck, "2002-01-06T07:00")]
        public void GetTollFee_PassageByNonFreeFeeVehiclesDuringSaturdayAndSunday_ZeroFee(VehicleType type, string passage)
        {
            var vehicle = VehicleFactory.Create(type);
            var passageDateTime = DateTime.Parse(passage);
            
            var fee = new TollCalculator().GetADaysTotalTollFee(vehicle, new[] {passageDateTime});
            fee.ShouldBe(0);

            passageDateTime.DayOfWeek.ShouldBeOneOf(DayOfWeek.Saturday, DayOfWeek.Sunday);
        } 
        
        
        [Theory]
        [InlineData(8, new[]{"06:00"})]
        [InlineData(8, new[]{"06:00", "06:20"})]
        [InlineData(13, new[]{"06:00", "06:30"})]
        [InlineData(26, new[]{"06:00", "07:00"})]
        [InlineData(18, new[]{"06:30", "07:00"})]
        [InlineData(31, new[]{"06:30", "07:30"})]
        [InlineData(39, new[]{"06:30", "07:30", "08:30"})]
        [InlineData(39, new[]{"06:30", "07:30", "08:30", "08:35", "08:40"})]
        [InlineData(39, new[]{"08:40", "08:35", "08:30", "07:30", "06:30"})]
        [InlineData(47, new[]{"06:30", "07:30", "08:30", "09:30"})]
        [InlineData(52, new[]{"06:30", "07:30", "08:30", "15:00"})]
        [InlineData(57, new[]{"06:30", "07:30", "08:30", "15:30"})]
        [InlineData(60, new[]{"06:30", "07:30", "08:30", "15:30", "16:30"})]
        [InlineData(60, new[]{"16:30", "08:30", "07:30", "06:30", "15:30"})]
        [InlineData(52, new[]{"06:30", "07:30", "08:30", "17:00"})]
        [InlineData(47, new[]{"06:30", "07:30", "08:30", "18:00"})]
        [InlineData(47, new[]{"06:30", "07:30", "08:30", "18:29"})]
        [InlineData(39, new[]{"06:30", "07:30", "08:30", "18:30"})]
        public void GetTollFeePerDay_NonTollFreeVehicleZeroPassages_ExpectedFee(int expectedFee, string[] passages)
        {
            var sut = new TollCalculator(); 
            
            sut.GetADaysTotalTollFee(
                    vehicle: new Car(),
                    passages: passages.Select(x=>DateTime.Parse($"2002-01-04T{x}")).ToArray())
                .ShouldBe(expectedFee);
            sut.GetADaysTotalTollFee(
                    vehicle: new Truck(),
                    passages: passages.Select(x=>DateTime.Parse($"2002-01-04T{x}")).ToArray())
                .ShouldBe(expectedFee);
        }
        
        
        [Fact]
        public void GetTollFeePerDay_PassagesOnDifferentDays_ThrowArgumentException()
        {
            var sut = new TollCalculator();

            Should.Throw<PassagesShouldBeOnDifferentDaysException>(() =>
                sut.GetADaysTotalTollFee(
                    new Car(),
                    new[]
                    {
                        new DateTime(2002, 1, 1),
                        new DateTime(2002, 1, 2),
                    })
            );
        }
        
        [Theory]
        [InlineData(24, new[]{"06:00"})]
        [InlineData(24, new[]{"06:00", "06:20"})]
        [InlineData(39, new[]{"06:00", "06:30"})]
        [InlineData(78, new[]{"06:00", "07:00"})]
        [InlineData(54, new[]{"06:30", "07:00"})]
        [InlineData(93, new[]{"06:30", "07:30"})]
        [InlineData(117, new[]{"06:30", "07:30", "08:30"})]
        [InlineData(117, new[]{"06:30", "07:30", "08:30", "08:35", "08:40"})]
        [InlineData(141, new[]{"06:30", "07:30", "08:30", "09:30"})]
        [InlineData(156, new[]{"06:30", "07:30", "08:30", "15:00"})]
        [InlineData(171, new[]{"06:30", "07:30", "08:30", "15:30"})]
        [InlineData(180, new[]{"06:30", "07:30", "08:30", "15:30", "16:30"})]
        [InlineData(156, new[]{"06:30", "07:30", "08:30", "17:00"})]
        [InlineData(141, new[]{"06:30", "07:30", "08:30", "18:00"})]
        [InlineData(141, new[]{"06:30", "07:30", "08:30", "18:29"})]
        [InlineData(117, new[]{"06:30", "07:30", "08:30", "18:30"})]
        public void GetTollFee_CarPassagesOnThreeDifferentWorkingDays_ExpectedFee(int expectedFee, string[] passages)
        {
            var passagesSpreadOnThreeDays = passages
                .Select(x => DateTime.Parse($"2000-01-04T{x}"))
                .Concat(passages.Select(x => DateTime.Parse($"2000-01-05T{x}")))
                .Concat(passages.Select(x => DateTime.Parse($"2000-01-07T{x}")))
                .ToArray();

            var sut = new TollCalculator(); 
            
            sut.GetTollFee(new Car(), passagesSpreadOnThreeDays).ShouldBe(expectedFee);
        }
        
        [Theory]
        [InlineData(24, new[]{"06:00"})]
        [InlineData(24, new[]{"06:00", "06:20"})]
        [InlineData(39, new[]{"06:00", "06:30"})]
        [InlineData(78, new[]{"06:00", "07:00"})]
        [InlineData(54, new[]{"06:30", "07:00"})]
        [InlineData(93, new[]{"06:30", "07:30"})]
        [InlineData(117, new[]{"06:30", "07:30", "08:30"})]
        [InlineData(117, new[]{"06:30", "07:30", "08:30", "08:35", "08:40"})]
        [InlineData(141, new[]{"06:30", "07:30", "08:30", "09:30"})]
        [InlineData(156, new[]{"06:30", "07:30", "08:30", "15:00"})]
        [InlineData(171, new[]{"06:30", "07:30", "08:30", "15:30"})]
        [InlineData(180, new[]{"06:30", "07:30", "08:30", "15:30", "16:30"})]
        [InlineData(156, new[]{"06:30", "07:30", "08:30", "17:00"})]
        [InlineData(141, new[]{"06:30", "07:30", "08:30", "18:00"})]
        [InlineData(141, new[]{"06:30", "07:30", "08:30", "18:29"})]
        [InlineData(117, new[]{"06:30", "07:30", "08:30", "18:30"})]
        public void GetTollFee_TruckPassagesOnThreeDifferentWorkingDays_ExpectedFee(int expectedFee, string[] passages)
        {
            var passagesSpreadOnThreeDays = passages
                .Select(x => DateTime.Parse($"2000-01-04T{x}"))
                .Concat(passages.Select(x => DateTime.Parse($"2000-01-05T{x}")))
                .Concat(passages.Select(x => DateTime.Parse($"2000-01-07T{x}")))
                .ToArray();

            var sut = new TollCalculator(); 
            
            sut.GetTollFee(new Truck(),passagesSpreadOnThreeDays).ShouldBe(expectedFee);
        }
    }
}
