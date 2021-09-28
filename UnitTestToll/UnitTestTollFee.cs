using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestToll
{
    [TestClass]
    public class UnitTestTollFee
    {
        [TestMethod]
        public void Test_IsFeeFree()
        {
            // build holidays 
            TollFeeCalculator.Toll.Builder builder = new TollFeeCalculator.Toll.Build();
            builder.BuildHollidays();
            TollFeeCalculator.Toll.Motorbike motorbike = new TollFeeCalculator.Toll.Motorbike();
            TollFeeCalculator.Toll.TollCalculator calculatorMotorBike = new TollFeeCalculator.Toll.TollCalculator(motorbike, 12, 00, 13, 00, DateTime.Now, builder.GetResult());
            Assert.IsTrue(calculatorMotorBike.IsFeeFree(DateTime.Now));
        }


        [TestMethod]
        public void Test_NotFeeFree()
        {
            // build holidays 
            TollFeeCalculator.Toll.Builder builder = new TollFeeCalculator.Toll.Build();
            builder.BuildHollidays();
            TollFeeCalculator.Toll.Tesla tesla = new TollFeeCalculator.Toll.Tesla();
            TollFeeCalculator.Toll.TollCalculator calculatorMotorBike = new TollFeeCalculator.Toll.TollCalculator(tesla, 12, 00, 13, 00, DateTime.Now, builder.GetResult());
            Assert.IsTrue(calculatorMotorBike.IsFeeFree(DateTime.Now));
        }
    }
}
