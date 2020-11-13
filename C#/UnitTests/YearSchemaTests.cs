using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class YearSchemaTests
    {
        private YearSchema _sut;

        [TestInitialize]
        public void Initialize()
        {
            
            _sut = new YearSchema("testyear_2017.json");
        }

        [TestMethod]
        public void OnCallingCtor_readsModelAndYearIsSet()
        {
            var result = _sut.GetYear();
            result.Should().Be(2017);
        }

        [TestMethod]
        public void WhenDayIsFound_ReturnsTrue()
        {
            var result = _sut.IsAFreeDay(new DateTime(2017,3,28,0,0,0));
            result.Should().BeTrue();
        }


        [TestMethod]
        public void WhenDayIsFound_ReturnsFalse()
        {
            var result = _sut.IsAFreeDay(new DateTime(2017, 10, 2, 0, 0, 0));
            result.Should().BeFalse();
        }

        [TestMethod]
        public void WhenDayIsInFreeMonth_returnsTrue()
        {
            var result = _sut.IsAFreeDay(new DateTime(2017, 4, 22, 0, 0, 0));
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDayIsSaturday_returnsTrue()
        {
            var result = _sut.IsAFreeDay(new DateTime(2017, 2, 25, 0, 0, 0));
            result.Should().BeTrue();
        }

        [TestMethod]
        public void WhenDayIsSunday_returnsTrue()
        {
            var result = _sut.IsAFreeDay(new DateTime(2017, 2, 26, 0, 0, 0));
            result.Should().BeTrue();
        }


    }

}
