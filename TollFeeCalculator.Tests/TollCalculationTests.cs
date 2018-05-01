using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TollFeeCalculator.Tests
{
    [TestClass]
    public class TollCalculationTests
    {
        // Baseline test for calculating normal weekday
        [TestMethod]
        public void CorrectCalculationTestForACarForNormalWeekday()
        {
            var times = new List<DateTime>()
            {
                new DateTime(2018,6,4, 6,0,0),    
                new DateTime(2018,6,4, 6,45,0),    
                new DateTime(2018,6,4, 7,0,0),     
                new DateTime(2018,6,4, 8,0,0),     
                new DateTime(2018,6,4, 8,30,0),    
                new DateTime(2018,6,4, 13,0,0),    
                new DateTime(2018,6,4, 13,30,0),   
                new DateTime(2018,6,4, 15,15,0),   
                new DateTime(2018,6,4, 16,15,0),   
                new DateTime(2018,6,4, 17,0,0),    
                new DateTime(2018,6,4, 18,0,0),    
                new DateTime(2018,6,4, 18,30,0)    
            };

            var classUnderTest = new TollCalculator();

            Assert.AreEqual(8, classUnderTest.GetTollFee(times[0], new Car()));  
            Assert.AreEqual(13, classUnderTest.GetTollFee(times[1], new Car())); 
            Assert.AreEqual(18, classUnderTest.GetTollFee(times[2], new Car())); 
            Assert.AreEqual(13, classUnderTest.GetTollFee(times[3], new Car())); 
            Assert.AreEqual(8, classUnderTest.GetTollFee(times[4], new Car()));  
            Assert.AreEqual(0, classUnderTest.GetTollFee(times[5], new Car()));  
            Assert.AreEqual(8, classUnderTest.GetTollFee(times[6], new Car()));  
            Assert.AreEqual(13, classUnderTest.GetTollFee(times[7], new Car())); 
            Assert.AreEqual(18, classUnderTest.GetTollFee(times[8], new Car())); 
            Assert.AreEqual(13, classUnderTest.GetTollFee(times[9], new Car())); 
            Assert.AreEqual(8, classUnderTest.GetTollFee(times[10], new Car())); 
            Assert.AreEqual(0, classUnderTest.GetTollFee(times[11], new Car())); 

            Assert.AreEqual(47/*48*/, new TollCalculator().GetTollFee(new Car(), times.ToArray()));
        }

        [TestMethod]
        public void CorrectFreeDateForACar()
        {
            var classUnderTest = new TollCalculator();
            Assert.AreEqual(0, classUnderTest.GetTollFee(new DateTime(2013, 1, 1), new Car()));
        }

        [TestMethod]
        public void CorrectFreeMonthForACar()
        {
            var classUnderTest = new TollCalculator();
            Assert.AreEqual(0, classUnderTest.GetTollFee(new DateTime(2013, 7, 11), new Car()));
        }

        [TestMethod]
        public void CorrectFeeForFreeVehicle()
        {
            var classUnderTest = new TollCalculator();
            Assert.AreEqual(0, classUnderTest.GetTollFee(new DateTime(2013, 1, 1), new Motorbike()));
        }

        [TestMethod]
        public void MaxFeeForDay()
        {
            var times = new List<DateTime>()
            {
                new DateTime(2018,6,4, 6,0,0),    
                new DateTime(2018,6,4, 7,1,0),    
                new DateTime(2018,6,4, 8,2,0),    
                new DateTime(2018,6,4, 9,3,0),    
                new DateTime(2018,6,4, 10,4,0),   
                new DateTime(2018,6,4, 11,5,0),   
                new DateTime(2018,6,4, 13,30,0),  
                new DateTime(2018,6,4, 15,15,0),  
                new DateTime(2018,6,4, 16,17,0),  
                new DateTime(2018,6,4, 17,30,0),  
                new DateTime(2018,6,4, 18,0,0),   
            };

            var classUnderTest = new TollCalculator();
            Assert.AreEqual(60, classUnderTest.GetTollFee(new Car(), times.ToArray()));
        }

        [TestMethod]
        public void MaxFeeForTwoDays()
        {
            var times = new List<DateTime>()
            {
                new DateTime(2018,6,4, 6,0,0),     
                new DateTime(2018,6,4, 7,1,0),     
                new DateTime(2018,6,4, 8,2,0),     
                new DateTime(2018,6,4, 9,3,0),     
                new DateTime(2018,6,4, 10,4,0),    
                new DateTime(2018,6,4, 11,5,0),    
                new DateTime(2018,6,4, 13,30,0),   
                new DateTime(2018,6,4, 15,15,0),   
                new DateTime(2018,6,4, 16,17,0),   
                new DateTime(2018,6,4, 17,30,0),   
                new DateTime(2018,6,4, 18,0,0),
                new DateTime(2018,6,5, 6,0,0),
                new DateTime(2018,6,5, 7,1,0),
                new DateTime(2018,6,5, 8,2,0),
                new DateTime(2018,6,5, 9,3,0),
                new DateTime(2018,6,5, 10,4,0),
                new DateTime(2018,6,5, 11,5,0),
                new DateTime(2018,6,5, 13,30,0),
                new DateTime(2018,6,5, 15,15,0),
                new DateTime(2018,6,5, 16,17,0),
                new DateTime(2018,6,5, 17,30,0),
                new DateTime(2018,6,5, 18,0,0),
            };

            var classUnderTest = new TollCalculator();
            Assert.AreEqual(120, classUnderTest.GetTollFee(new Car(), times.ToArray()));
        }
    }
}
