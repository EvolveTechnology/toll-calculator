using System;
using Xunit;
using TollFeeCalculator;

namespace TollFeeCalculatorTest
{
    public class TollTariffTests
    {
        private readonly TollTariff tariff = new TollTariff();
        private readonly TollCalculator calculator;
        private readonly Vehicle car = new Car();

        public TollTariffTests()
        {
            calculator = new TollCalculator(tariff);
        }

        [Fact]
        public void NewIsTollFreeDateImplShouldGiveSameResultAsOldFor2013()
        {
            var year = 2013;
            for (var month = 1; month <=12; month++)
            {
                for (var day = 1; day <= DateTime.DaysInMonth(year,month); day++)
                {
                    var dt = new DateTime(year,month,day);
                    var expected = calculator.IsTollFreeDate(dt);
                    var result = tariff.IsTollFreeDate(dt);                
                    Assert.Equal(expected, result);
                }
            }
        }
    }
}
