using System;
using System.Linq;
using TollCalculator.Lib;
using Xunit;

namespace TollCalculator.Tests.ComponentTests {
    public class TollCalculatorTest
    {
        [Fact]
        public void CorrectFee()
        {
            var fee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                ParseDate("2020-12-24 06:20"), //No fee, holiday
                ParseDate("2020-02-05 06:22"), //(8)kr
                ParseDate("2020-02-05 06:33"), //13kr Day 05/02 only 13kr
                ParseDate("2020-02-02 06:24"), //No fee, sunday
                ParseDate("2020-02-06 12:25"), //Non rush hour, 8kr
                ParseDate("2020-02-06 07:30"), //18kr
            });
            
            //Total: 13 + 8 + 18 = 39kr
            
            Assert.Equal(47, fee);
            AssertFeeIsInsideDayLimit(fee);
        }
        [Fact]
        public void FeeIsInsideDayLimit()
        {
            var fee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                ParseDate("2020-02-05 00:20"), //8kr
                ParseDate("2020-02-05 01:20"), //8kr
                ParseDate("2020-02-05 02:20"), //8kr
                ParseDate("2020-02-05 03:20"), //8kr
                ParseDate("2020-02-05 04:20"), //8kr
                ParseDate("2020-02-05 05:20"), //8kr
                ParseDate("2020-02-05 10:20"), //8kr
                ParseDate("2020-02-05 11:20"), //8kr
            });
            
            Assert.Equal(60, fee);
        }
        
        [Fact]
        public void RushHourHasHighestFees()
        {
            var nonRushHourFee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                ParseDate("2020-02-05 01:00"), //8kr, non-rush hour
            });
            
            var rushHourFee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                ParseDate("2020-02-05 07:30"), //18kr, rish hour
            });
            
            Assert.True(nonRushHourFee < rushHourFee);
            
            AssertFeeIsInsideDayLimit(nonRushHourFee);
            AssertFeeIsInsideRangeLimits(nonRushHourFee);
            
            AssertFeeIsInsideDayLimit(rushHourFee);
            AssertFeeIsInsideRangeLimits(rushHourFee);
        }
        
        [Fact]
        public void VehicleOnlyChargedOnceAnHour()
        {
            var fee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                ParseDate("2020-02-05 06:20"), //8kr
                ParseDate("2020-02-05 06:22"), //8kr
                ParseDate("2020-02-05 06:23"), //8kr
                ParseDate("2020-02-05 06:24"), //8kr
                ParseDate("2020-02-05 06:25"), //8kr
            });
            
            Assert.Equal(8, fee);
            AssertFeeIsInsideDayLimit(fee);
            AssertFeeIsInsideRangeLimits(fee);
        }
        
        [Fact]
        public void HighestFeeInSameHourApplies()
        {
            var fee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                ParseDate("2020-02-05 06:29"), //8kr
                ParseDate("2020-02-05 06:31"), //13kr
                ParseDate("2020-02-05 07:01"), //18kr
            });
            
            Assert.Equal(18, fee);
            AssertFeeIsInsideDayLimit(fee);
            AssertFeeIsInsideRangeLimits(fee);
        }
        
        [Theory]
        [InlineData(VehicleType.Motorbike)]
        [InlineData(VehicleType.Tractor)]
        [InlineData(VehicleType.Emergency)]
        [InlineData(VehicleType.Diplomat)]
        [InlineData(VehicleType.Foreign)]
        [InlineData(VehicleType.Military)]
        public void SpecificVehicleTypesAreFree(VehicleType vehicleType)
        {
            var testDateStrings = new string[]
            {
                "2020-01-20 02:00",
                "2020-01-21 05:00",
                "2020-01-22 08:00",
                "2020-01-23 12:00",
                "2020-01-24 14:00",
                "2020-01-25 18:00",
                "2020-01-26 22:30",
            };

            var testDateTimes = testDateStrings.Select(ParseDate).ToArray();
            var actualFee = Lib.TollCalculator.GetTollFee(vehicleType, testDateTimes);

            Assert.Equal(0, actualFee);
        }
        
        [Theory]
        [InlineData(VehicleType.Car, "2020-01-27 15:40")] //Saturday
        [InlineData(VehicleType.Diplomat, "2020-02-02 14:40")] //Sunday 
        public void WeekendsAreFree(VehicleType vehicleType, string dateTimeString)
        {
            var dateTime = ParseDate(dateTimeString);
            var actualFee = Lib.TollCalculator.GetTollFee(vehicleType, new [] { dateTime });
            
            Assert.Equal(0, actualFee);
        }
        
        [Theory]
        [InlineData(VehicleType.Car, "2019-01-01 14:40")]
        [InlineData(VehicleType.Diplomat, "2019-03-28 15:40")]
        [InlineData(VehicleType.Emergency, "2019-04-01 16:40")]
        [InlineData(VehicleType.Foreign, "2019-11-01 17:40")]
        [InlineData(VehicleType.Military, "2020-12-31 18:40")]
        public void HolidaysAreFree(VehicleType vehicleType, string dateTimeString)
        {
            var dateTime = ParseDate(dateTimeString);
            var actualFee = Lib.TollCalculator.GetTollFee(vehicleType, new [] { dateTime });
            
            Assert.Equal(0, actualFee);
        }

        private void AssertFeeIsInsideDayLimit(int fee) {
            Assert.InRange(fee, 0, 60);
        }
        
        private void AssertFeeIsInsideRangeLimits(int fee) {
		    Assert.InRange(fee, 8, 18);
        }

        private static DateTime ParseDate(string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}