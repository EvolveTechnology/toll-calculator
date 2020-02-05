using System.Linq;
using TollCalculator.Lib;
using TollCalculator.Lib.Models;
using TollCalculator.Tests.Utils;
using Xunit;

namespace TollCalculator.Tests.ComponentTests {
    public class TollCalculatorTest
    {
        [Fact]
        public void CorrectFee()
        {
            var fee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                DateUtils.ParseDateAndTime("2020-12-24 06:20"), //No fee, holiday
                DateUtils.ParseDateAndTime("2020-02-05 06:22"), //(8)kr
                DateUtils.ParseDateAndTime("2020-02-05 06:33"), //13kr, Same hour as above, only 13kr this day
                DateUtils.ParseDateAndTime("2020-02-02 06:24"), //No fee, sunday
                DateUtils.ParseDateAndTime("2020-02-06 12:25"), //8kr, Not rush hour
                DateUtils.ParseDateAndTime("2020-02-06 07:30"), //18kr, Rush hour
            });
            
            //Total: 13 + 8 + 18 = 39kr
            
            Assert.Equal(39, fee);
            AssertFeeIsInsideDayLimit(fee);
        }
        [Fact]
        public void FeeIsInsideDayLimit()
        {
            var fee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                DateUtils.ParseDateAndTime("2020-02-05 00:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 01:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 02:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 03:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 04:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 05:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 10:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 11:20"), //8kr
            });
            
            Assert.Equal(TollRules.DailyFeeMax, fee);
        }
        
        [Fact]
        public void RushHourHasHighestFees()
        {
            var nonRushHourFee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                DateUtils.ParseDateAndTime("2020-02-05 01:00"), //8kr, non-rush hour
            });
            
            var rushHourFee = Lib.TollCalculator.GetTollFee(VehicleType.Car, new []
            {
                DateUtils.ParseDateAndTime("2020-02-05 07:30"), //18kr, rush hour
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
                DateUtils.ParseDateAndTime("2020-02-05 06:20"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 06:22"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 06:23"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 06:24"), //8kr
                DateUtils.ParseDateAndTime("2020-02-05 06:25"), //8kr
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
                DateUtils.ParseDateAndTime("2020-02-05 15:00"), //13kr
                DateUtils.ParseDateAndTime("2020-02-05 15:30"), //18kr
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
            var testDateStrings = new []
            {
                "2020-01-20 02:00",
                "2020-01-21 05:00",
                "2020-01-22 08:00",
                "2020-01-23 12:00",
                "2020-01-24 14:00",
                "2020-01-25 18:00",
                "2020-01-26 22:30",
            };

            var testDateTimes = testDateStrings.Select(DateUtils.ParseDateAndTime).ToArray();
            var actualFee = Lib.TollCalculator.GetTollFee(vehicleType, testDateTimes);

            Assert.Equal(0, actualFee);
        }
        
        [Theory]
        [InlineData(VehicleType.Car, "2020-01-25 15:40")] //Saturday
        [InlineData(VehicleType.Car, "2020-02-02 14:40")] //Sunday 
        public void WeekendsAreFree(VehicleType vehicleType, string dateTimeString)
        {
            var dateTime = DateUtils.ParseDateAndTime(dateTimeString);
            var actualFee = Lib.TollCalculator.GetTollFee(vehicleType, new [] { dateTime });
            
            Assert.Equal(0, actualFee);
        }
        
        [Theory]
        [InlineData(VehicleType.Car, "2019-01-01 14:40")]
        [InlineData(VehicleType.Car, "2019-04-30 15:40")]
        [InlineData(VehicleType.Car, "2019-05-01 16:40")]
        [InlineData(VehicleType.Car, "2019-06-06 17:40")]
        [InlineData(VehicleType.Car, "2020-12-31 18:40")]
        public void HolidaysAreFree(VehicleType vehicleType, string dateTimeString)
        {
            var dateTime = DateUtils.ParseDateAndTime(dateTimeString);
            var actualFee = Lib.TollCalculator.GetTollFee(vehicleType, new [] { dateTime });
            
            Assert.Equal(0, actualFee);
        }

        private void AssertFeeIsInsideDayLimit(int fee) {
            Assert.InRange(fee, 0, TollRules.DailyFeeMax);
        }
        
        private void AssertFeeIsInsideRangeLimits(int fee) {
		    Assert.InRange(fee, TollRules.FeeMin, TollRules.FeeMax);
        }
    }
}