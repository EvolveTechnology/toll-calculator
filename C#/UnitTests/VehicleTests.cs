using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using toll_calculator;

namespace UnitTests
{
    [TestClass]
    public class VehicleTests
    {
        private ITollFeeAggregator fakeAggregator;
        private Vehicle _sut;

        [TestInitialize]
        public void Initialize()
        {
            fakeAggregator = new Fake<ITollFeeAggregator>().FakedObject;
            _sut = new Vehicle(fakeAggregator,VehicleType.Emergency);
        }

        [TestMethod]
        public void GetTotalFee_returnsValueFromAggregator()
        {
            _sut.GetTotalFee(new System.DateTime[] { });
            A.CallTo(() => fakeAggregator.GetTotalToll(A<List<DateTime>>.Ignored)).MustHaveHappenedOnceExactly();
        }
    }
}
