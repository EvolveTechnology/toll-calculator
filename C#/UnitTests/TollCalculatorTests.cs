using FakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class TollCalculatorTests 
    {
        private ISchemas fakeSchemas;
        private IYearSchema fakeYear;
        private IVehicleFactory fakeFactory;
        private IVehicle fakeVehicle;
        private TollCalculator _sut;

        [TestInitialize]
        public void Initialize()
        {
            fakeFactory = new Fake<IVehicleFactory>().FakedObject;
            fakeSchemas = new Fake<ISchemas>().FakedObject;
            fakeYear = new Fake<IYearSchema>().FakedObject;
            fakeVehicle = new Fake<IVehicle>().FakedObject;
            _sut = new TollCalculator(fakeSchemas,fakeFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void WhenNoDatesArePassed_throwError()
        {
            _sut.GetTollFee(VehicleType.Car, new DateTime[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateRangeException))]
        public void WhenNoDatesFromDifferentDaysArePassed_throwError()
        {
            _sut.GetTollFee(VehicleType.Car, new DateTime[] { new DateTime(1,1,1,1,1,1), new DateTime(2,2,2,2,2,2)});
        }


        [TestMethod]
        public void WhenYearMatchRegisteredYears_CheckForFreeDays()
        {
            A.CallTo(() => fakeSchemas.GetSchemaForYear(A<int>.Ignored)).Returns(fakeYear);
            A.CallTo(() => fakeYear.IsAFreeDay(A<DateTime>.Ignored)).Returns(true);
           
            var result = _sut.GetTollFee(VehicleType.Car, new DateTime[] { new DateTime(1, 1, 1, 1, 1, 1), new DateTime(1, 1, 1, 2, 2, 2) });
            result.Should().Be(0);

            A.CallTo(() => fakeSchemas.GetSchemaForYear(A<int>.Ignored)).MustHaveHappened();
            A.CallTo(() => fakeYear.IsAFreeDay(A<DateTime>.Ignored)).MustHaveHappened();
            A.CallTo(() => fakeFactory.GetVehicle(A<VehicleType>._)).MustNotHaveHappened();
        }

        [TestMethod]
        public void WhenDatesAreValidReturnsNewVehicleFromfactory()
        {
            A.CallTo(() => fakeSchemas.GetSchemaForYear(A<int>.Ignored)).Returns(fakeYear);
            A.CallTo(() => fakeYear.IsAFreeDay(A<DateTime>.Ignored)).Returns(false);
            A.CallTo(() => fakeFactory.GetVehicle(A<VehicleType>.Ignored)).Returns(fakeVehicle);
            var result =  _sut.GetTollFee(VehicleType.Car, new DateTime[] { new DateTime(1, 1, 1, 1, 1, 1), new DateTime(1, 1, 1, 2, 2, 2) });

            A.CallTo(() => fakeSchemas.GetSchemaForYear(A<int>.Ignored)).MustHaveHappened();
            A.CallTo(() => fakeYear.IsAFreeDay(A<DateTime>.Ignored)).MustHaveHappened();
            A.CallTo(() => fakeFactory.GetVehicle(A<VehicleType>.Ignored)).MustHaveHappened();

        }
    }
}
