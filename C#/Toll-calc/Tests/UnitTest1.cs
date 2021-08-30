using System;
using Toll_calc.Models;
using Toll_calc.Services;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {

        private readonly TollCalculator _calculator;
        public UnitTest1()
        {
            var holidayService = new HolidayService();
            _calculator = new TollCalculator(holidayService);
        }

        [Fact]
        public void Car_Three_Passes_Spread_Out_Weekday()
        {
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 9, 1,08,30,0),
                new DateTime(2021, 9, 1, 06,15,00),
                new DateTime(2021, 9, 1, 16,15,00),
            };
            var totalSum = _calculator.GetTollFeeForDay(new Car(), passTimes);
            Assert.Equal(34, totalSum);
        }

        [Fact]
        public void Toll_Free_Vehicle()
        {
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 9, 1,08,30,0),
                new DateTime(2021, 9, 1, 06,15,00),
                new DateTime(2021, 9, 1, 16,15,00),
            };
            var totalSum = _calculator.GetTollFeeForDay(new Diplomat(), passTimes);
            Assert.Equal(0, totalSum);
        }

        [Fact]
        public void Saturday()
        {
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 9, 4,08,30,0),
                new DateTime(2021, 9, 4, 06,15,00),
                new DateTime(2021, 9, 4, 16,15,00),
            };
            var totalSum = _calculator.GetTollFeeForDay(new Car(), passTimes);
            Assert.Equal(0, totalSum);
        }
        
        [Fact]
        public void Weekends_Should_Be_Free()
        {
            var saturday = new DateTime[]
            {
                new DateTime(2021, 9, 4,08,30,0),
                new DateTime(2021, 9, 4, 06,15,00),
                new DateTime(2021, 9, 4, 16,15,00),
            };
            var saturdaySum = _calculator.GetTollFeeForDay(new Car(), saturday);
            Assert.Equal(0, saturdaySum);

            var sunday = new DateTime[]
            {
                new DateTime(2021, 9, 5,08,30,0),
                new DateTime(2021, 9, 5, 06,15,00),
                new DateTime(2021, 9, 5, 16,15,00),
            };
            var sundaySum = _calculator.GetTollFeeForDay(new Car(), sunday);
            Assert.Equal(0, sundaySum);
        }

        [Fact]
        public void Holidays_Should_Be_Free()
        {
            //friday
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 12, 24,08,30,0),
                new DateTime(2021, 12, 24, 06,15,00),
                new DateTime(2021, 12, 24, 16,15,00),
            };
            var totalSum = _calculator.GetTollFeeForDay(new Car(), passTimes);
            Assert.Equal(0, totalSum);
        }

        [Fact]
        public void Multiple_Passes_Within_An_Hour_Should_Only_Count_Highest()
        {
            //friday
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 09, 1,06,55,0), // 13 - overwritten by line 3
                new DateTime(2021, 09, 1, 06,15,00), // 8 - overwritten by line 3
                new DateTime(2021, 09, 1, 7,13,00), // 18  - counted
                new DateTime(2021, 09, 1, 7,16,00), // 18  - counted

            };
            var totalSum = _calculator.GetTollFeeForDay(new Car(), passTimes);
            Assert.Equal(36, totalSum);
        }

        [Fact]
        public void Multiple_Passes_Within_An_Hour_Should_Only_Count_Highest2()
        {
            //friday
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 09, 1,06,55,0), // 13 - overwritten by line 3
                new DateTime(2021, 09, 1, 06,15,00), // 8 - overwritten by line 3
                new DateTime(2021, 09, 1, 7,13,00), // 18  - counted
                new DateTime(2021, 09, 1, 7,16,00), // 18  - counted
                new DateTime(2021, 09, 1, 8,12,00), // 13  - skipped since previous row is start of a new hour interval and has a  higher fee

            };
            var totalSum = _calculator.GetTollFeeForDay(new Car(), passTimes);
            Assert.Equal(36, totalSum);
        }


        [Fact]
        public void Maximum_Fee_Cannot_Exceed_60()
        {
            //friday
            var passTimes = new DateTime[]
            {
                //interval 1
                new DateTime(2021, 09, 1, 06,55,0), // 13 - overwritten by line 3
                new DateTime(2021, 09, 1, 06,15,00), // 8 - overwritten by line 3
                new DateTime(2021, 09, 1, 07,13,00), // 18  - counted

                //interval 2
                new DateTime(2021, 09, 1, 07,16,00), // 18  - counted
                new DateTime(2021, 09, 1, 08,12,00), // 13  - skipped since previous row is start of a new hour interval and has a  higher fee

                //interval 3
                new DateTime(2021, 09, 1, 11,12,00), // 8  - counted

                //interval 4
                new DateTime(2021, 09, 1, 15,12,00), // 13  - overwritten
                new DateTime(2021, 09, 1, 16,00,00), // 18  - counted

                //sum = 62
            };
            var totalSum = _calculator.GetTollFeeForDay(new Car(), passTimes);
            Assert.Equal(60, totalSum);
        }


        [Fact]
        public void NullVehicle_ThrowsArgumentException()
        {
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 09, 1, 06, 55, 0)
            };
            
            var ex = Assert.Throws<ArgumentException>(() => _calculator.GetTollFeeForDay(null, passTimes));
            Assert.Equal("Vehicle can not be null.", ex.Message);

        }

        [Fact]
        public void Pass_Times_With_Different_Dates_ThrowsArgumentException()
        {
            var passTimes = new DateTime[]
            {
                new DateTime(2021, 09, 1, 06, 55, 0),
                new DateTime(2021, 08, 1, 06, 55, 0)
            };

           var ex = Assert.Throws<ArgumentException>(() => _calculator.GetTollFeeForDay(new Car(), passTimes));
           Assert.Equal("All pass times must be from the same date.", ex.Message);
        }

        [Fact]
        public void Null_Or_Empty_Pass_Times_returns_0()
        {
            var passTimes = new DateTime[]
            {
            };

            var zeroLengthSum = _calculator.GetTollFeeForDay(new Car(), passTimes);
            Assert.Equal(0, zeroLengthSum);

            var nullTimesSum = _calculator.GetTollFeeForDay(new Car(), null);
            Assert.Equal(0, nullTimesSum);
        }
    }
}
