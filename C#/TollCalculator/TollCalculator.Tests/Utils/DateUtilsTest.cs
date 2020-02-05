using Xunit;

namespace TollCalculator.Tests.Utils
{
    public class DateUtilsTest
    {
        [Fact]
        public void DateAndTimeIsParsedCorrectly()
        {
            var result = DateUtils.ParseDateAndTime("2020-02-05 14:20");
            
            Assert.Equal(2020, result.Year);
            Assert.Equal(2, result.Month);
            Assert.Equal(5, result.Day);
            
            Assert.Equal(14, result.Hour);
            Assert.Equal(20, result.Minute);
        }
    }
}