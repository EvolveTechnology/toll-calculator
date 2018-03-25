using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Toll_calculator;

namespace Toll_calculator_test {

    [TestClass]
    public class FeePolicyTest {

        private StandardFeePolicy standardFeePolicy;

        [TestInitialize]
        public void testInit() {
            standardFeePolicy = new StandardFeePolicy();
        }

        [TestCleanup]
        public void testClean() {
            standardFeePolicy = null;
        }

        [TestMethod]
        public void TestStandardFeePolicy() {
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2018, 4, 12, 5, 22, 39)), 0);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2013, 2, 2, 6, 4, 9)), standardFeePolicy.LOW_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(1987, 7, 2, 6, 51, 49)), standardFeePolicy.MEDIUM_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(1, 1, 2, 7, 11, 0)), standardFeePolicy.HIGH_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2090, 1, 2, 8, 29, 59)), standardFeePolicy.MEDIUM_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2018, 9, 21, 11, 26, 1)), standardFeePolicy.LOW_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2018, 12, 25, 15, 0, 0)), standardFeePolicy.MEDIUM_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2018, 9, 18, 15, 30, 0)), standardFeePolicy.HIGH_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2018, 1, 1, 17, 59, 59)), standardFeePolicy.MEDIUM_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2019, 5, 25, 18, 11, 33)), standardFeePolicy.LOW_FEE);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2018, 7, 27, 19, 50, 5)), 0);
            Assert.AreEqual(standardFeePolicy.GetFee(new DateTime(2018, 7, 27, 0, 0, 0)), 0);
        }
    }
}
