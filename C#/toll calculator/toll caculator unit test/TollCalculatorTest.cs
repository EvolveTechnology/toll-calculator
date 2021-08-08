using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using toll_calculator_logic;

namespace toll_caculator_unit_test
{
    [TestClass]
    public class TollCalculatorTest
    {
        [TestMethod]
        public void Car_SiglePass_Nine_thirty_2013_5_13_Monday_Fee_8()
        {
            //Arrang
            var car = new Car();
            var datetime = new DateTime(2013, 5, 13, 9, 30, 27);

            var tollCalculator = new TollCalculator();

            //Act
            var fee = tollCalculator.GetTollFee(datetime, car);

            //Assert
            Assert.AreEqual(8, fee);
        }

        [TestMethod]
        public void Motorbike_SiglePass_Nine_thirty_2013_5_13_Monday_Fee_0()
        {
            //Arrang
            var motorbike = new Motorbike();
            var datetime = new DateTime(2013, 5, 13, 9, 30, 27);

            var tollCalculator = new TollCalculator();

            //Act
            var fee = tollCalculator.GetTollFee(datetime, motorbike);

            //Assert
            Assert.AreEqual(0, fee);
        }


        [TestMethod]
        public void Car_SiglePass_Nine_thirty_2013_5_12_Sunday_Fee_0()
        {
            //Arrang
            var car = new Car();
            var datetime = new DateTime(2013, 5, 12, 9, 30, 27);

            var tollCalculator = new TollCalculator();

            //Act
            var fee = tollCalculator.GetTollFee(datetime, car);

            //Assert
            Assert.AreEqual(0, fee);
        }


        [TestMethod]
        public void Car_SiglePass_Nine_thirty_2013_5_13_Monday_Fee_0()
        {
            //Arrang
            var emergency = new Emergency();
            var datetime = new DateTime(2013, 5, 13, 9, 30, 27);

            var tollCalculator = new TollCalculator();

            //Act
            var fee = tollCalculator.GetTollFee(datetime, emergency);

            //Assert
            Assert.AreEqual(0, fee);
        }

        [TestMethod]
        public void Car_SiglePass_Nine_thirty5_2013_5_12_Monday_Fee_18()
        {
            //Arrang
            var car = new Car();
            var datetime = new DateTime(2013, 5, 13, 7, 35, 27);

            var tollCalculator = new TollCalculator();

            //Act
            var fee = tollCalculator.GetTollFee(datetime, car);

            //Assert
            Assert.AreEqual(18, fee);
        }

        [TestMethod]
        public void Car_MultiPass_2013_5_13_Monday_Fee()
        {
            //Arrang
            var car = new Car();

            var datetimeAry = new List<DateTime>
            {
                new DateTime(2013, 5, 13, 6, 15, 27), //fee = 8
                new DateTime(2013, 5, 13, 7, 10, 27), //fee = 18 ** this one applies because its max fee in one hour timespan


                new DateTime(2013, 5, 13, 8, 5, 27), // fee = 13 ** this one applies because its max in one hour timespan
                new DateTime(2013, 5, 13, 8, 55, 27), // fee = 8 

                new DateTime(2013, 5, 13, 20, 35, 27), // fee = 0        
            }.ToArray();


            var tollCalculator = new TollCalculator();

            //Act
            var fee = tollCalculator.GetTollFee(car, datetimeAry);

            //Assert
            Assert.AreEqual(31, fee);
        }

    }
}
