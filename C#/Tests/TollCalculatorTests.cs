using System;
using TollFeeCalculator;
using TollFeeCalculator.Tests;
using TollFeeCalculator.Vehicles;

//TODO: Test no more than 60 in fee
public class CarTollCalculatorTests : ITest
{
    private TestVehicleFactory TestVehicleFactory { get; }

    public CarTollCalculatorTests()
    {
        this.TestVehicleFactory = new TestVehicleFactory();
    }

    [TollUnitTest]
    public void Car_toll_valid_week_dates()
    {
        //Arrange
        var calculateCarToll = new TollCalculator();
        var car = this.TestVehicleFactory.Generate(VehicleFeeType.Car);
        var dates = new DateTime[] 
        {
            new DateTime(2018, 09, 18, 10, 32, 47),
            new DateTime(2018, 09, 19, 11, 32, 47),
            new DateTime(2018, 09, 20, 12, 32, 47),
        };

        //Act
        var result = calculateCarToll.GetTollFee(car, dates, false);

        //Assert
        Assert.Instance.AreEqual(8*3, result);
    }

    [TollUnitTest]
    public void Car_toll_dates_under_one_hour()
    {
        //Arrange
        var calculateCarToll = new TollCalculator();
        var car = this.TestVehicleFactory.Generate(VehicleFeeType.Car);
        var dates = new DateTime[] 
        {
            new DateTime(2018, 09, 18, 10, 32, 47),
            new DateTime(2018, 09, 18, 10, 33, 47),
            new DateTime(2018, 09, 18, 10, 34, 47),
        };

        //Act
        var result = calculateCarToll.GetTollFee(car, dates);

        //Assert
        Assert.Instance.AreEqual(8, result);
    }

    [TollUnitTest]
    public void Car_toll_dates_more_then_one_hour()
    {
        //Arrange
        var calculateCarToll = new TollCalculator();
        var car = this.TestVehicleFactory.Generate(VehicleFeeType.Car);
        var dates = new DateTime[] 
        {
            new DateTime(2018, 09, 18, 10, 32, 47),
            new DateTime(2018, 09, 18, 10, 33, 47),
            new DateTime(2018, 09, 18, 10, 34, 47),
            new DateTime(2018, 09, 18, 11, 34, 47),
        };

        //Act
        var result = calculateCarToll.GetTollFee(car, dates);

        //Assert
        Assert.Instance.AreEqual(8 + 8, result);
    }

    [TollUnitTest]
    public void Car_toll_dates_different_day_and_fee()
    {
        //Arrange
        var calculateCarToll = new TollCalculator();
        var car = this.TestVehicleFactory.Generate(VehicleFeeType.Car);
        var dates = new DateTime[] 
        {
            new DateTime(2018, 09, 18, 06, 29, 47),
            new DateTime(2018, 09, 19, 10, 33, 47),
            new DateTime(2018, 09, 20, 17, 34, 47),
        };
        var date1 = new DateTime[] { new DateTime(2018, 09, 18, 06, 29, 47) };
        var date2 = new DateTime[] { new DateTime(2018, 09, 19, 10, 33, 47) };
        var date3 = new DateTime[] { new DateTime(2018, 09, 20, 17, 34, 47) };

        //Act
        var result = calculateCarToll.GetTollFee(car, dates, false);
        var result1 = calculateCarToll.GetTollFee(car, date1);
        var result2 = calculateCarToll.GetTollFee(car, date2);
        var result3 = calculateCarToll.GetTollFee(car, date3);

        //Assert
        Assert.Instance.AreEqual(8 + 8 + 13, result);
        Assert.Instance.AreEqual(8 , result1);
        Assert.Instance.AreEqual(8, result2);
        Assert.Instance.AreEqual(13, result3);
    }

    [TollUnitTest]
    public void Car_toll_dates_all_different_fees()
    {
        //Arrange
        var calculateCarToll = new TollCalculator();
        var car = this.TestVehicleFactory.Generate(VehicleFeeType.Car);

        var dates0501 = new DateTime[] { new DateTime(2018, 09, 18, 5, 01, 00) };
        var dates0601 = new DateTime[] { new DateTime(2018, 09, 18, 6, 01, 00) };
        var dates0631 = new DateTime[] { new DateTime(2018, 09, 18, 6, 31, 00) };
        var dates0701 = new DateTime[] { new DateTime(2018, 09, 18, 7, 01, 00) };
        var dates0801 = new DateTime[] { new DateTime(2018, 09, 18, 8, 01, 00) };
        var dates0831 = new DateTime[] { new DateTime(2018, 09, 18, 8, 31, 00) };
        var dates1501 = new DateTime[] { new DateTime(2018, 09, 18, 15, 01, 00) };
        var dates1531 = new DateTime[] { new DateTime(2018, 09, 18, 15, 31, 00) };
        var dates1631 = new DateTime[] { new DateTime(2018, 09, 18, 16, 31, 00) };
        var dates1701 = new DateTime[] { new DateTime(2018, 09, 18, 17, 01, 00) };
        var dates1801 = new DateTime[] { new DateTime(2018, 09, 18, 18, 01, 00) };
        var dates1901 = new DateTime[] { new DateTime(2018, 09, 18, 19, 01, 00) };

        //Act
        var result0501 = calculateCarToll.GetTollFee(car, dates0501);
        var result0601 = calculateCarToll.GetTollFee(car, dates0601);
        var result0631 = calculateCarToll.GetTollFee(car, dates0631);
        var result0701 = calculateCarToll.GetTollFee(car, dates0701);
        var result0801 = calculateCarToll.GetTollFee(car, dates0801);
        var result0831 = calculateCarToll.GetTollFee(car, dates0831);
        var result1501 = calculateCarToll.GetTollFee(car, dates1501);
        var result1531 = calculateCarToll.GetTollFee(car, dates1531);
        var result1631 = calculateCarToll.GetTollFee(car, dates1631);
        var result1701 = calculateCarToll.GetTollFee(car, dates1701);
        var result1801 = calculateCarToll.GetTollFee(car, dates1801);
        var result1901 = calculateCarToll.GetTollFee(car, dates1901);

        //Assert
        Assert.Instance.AreEqual(0, result0501);
        Assert.Instance.AreEqual(8, result0601);
        Assert.Instance.AreEqual(13, result0631);
        Assert.Instance.AreEqual(18, result0701);
        Assert.Instance.AreEqual(13, result0801);
        Assert.Instance.AreEqual(8, result0831);
        Assert.Instance.AreEqual(13, result1501);
        Assert.Instance.AreEqual(18, result1531);
        Assert.Instance.AreEqual(18, result1631);
        Assert.Instance.AreEqual(13, result1701);
        Assert.Instance.AreEqual(8, result1801);
        Assert.Instance.AreEqual(0, result1901);
    }

    [TollUnitTest]
    public void Is_toll_free_vechile()
    {
        // Arrange
        var calculateCarToll = new TollCalculator();
        var car = this.TestVehicleFactory.Generate(VehicleFeeType.Car);
        var motorbike = this.TestVehicleFactory.Generate(VehicleFeeType.Motorbike);
        var tractor = this.TestVehicleFactory.Generate(VehicleFeeType.Tractor);
        var emergency = this.TestVehicleFactory.Generate(VehicleFeeType.Emergency);
        var diplomat = this.TestVehicleFactory.Generate(VehicleFeeType.Diplomat);
        var foreign = this.TestVehicleFactory.Generate(VehicleFeeType.Foreign);
        var military = this.TestVehicleFactory.Generate(VehicleFeeType.Military);

        var dates0601 = new DateTime[] { new DateTime(2018, 09, 18, 6, 01, 00) };

        // Act
        var resultShouldHaveFee = calculateCarToll.GetTollFee(car, dates0601);
        var resultNoFee1 = calculateCarToll.GetTollFee(motorbike, dates0601);
        var resultNoFee2 = calculateCarToll.GetTollFee(tractor, dates0601);
        var resultNoFee3 = calculateCarToll.GetTollFee(emergency, dates0601);
        var resultNoFee4 = calculateCarToll.GetTollFee(diplomat, dates0601);
        var resultNoFee5 = calculateCarToll.GetTollFee(foreign, dates0601);
        var resultNoFee6 = calculateCarToll.GetTollFee(military, dates0601);

        // Assert
        Assert.Instance.AreEqual(8, resultShouldHaveFee);
        Assert.Instance.AreEqual(0, resultNoFee1);
        Assert.Instance.AreEqual(0, resultNoFee2);
        Assert.Instance.AreEqual(0, resultNoFee3);
        Assert.Instance.AreEqual(0, resultNoFee4);
        Assert.Instance.AreEqual(0, resultNoFee5);
        Assert.Instance.AreEqual(0, resultNoFee6);
    }

    [TollUnitTest]
    public void Is_toll_free_date()
    {
        // Arrange
        var test = new DateTime(2000, 09, 18, 23, 59, 59);//60, -1);
        var calculateCarToll = new TollCalculator();
        var car = this.TestVehicleFactory.Generate(VehicleFeeType.Car);

        var dates0918 = new DateTime[] { new DateTime(2013, 09, 18, 6, 01, 00) };

        var dates0101 = new DateTime[] { new DateTime(2013, 01, 01, 6, 01, 00) };

        var dates0328 = new DateTime[] { new DateTime(2013, 03, 28, 6, 01, 00) };
        var dates0329 = new DateTime[] { new DateTime(2013, 03, 29, 6, 01, 00) };

        var dates0401 = new DateTime[] { new DateTime(2013, 04, 01, 6, 01, 00) };
        var dates0430 = new DateTime[] { new DateTime(2013, 04, 30, 6, 01, 00) };

        var dates0501 = new DateTime[] { new DateTime(2013, 05, 01, 6, 01, 00) };
        var dates0508 = new DateTime[] { new DateTime(2013, 05, 08, 6, 01, 00) };
        var dates0509 = new DateTime[] { new DateTime(2013, 05, 09, 6, 01, 00) };

        var dates0605 = new DateTime[] { new DateTime(2013, 06, 05, 6, 01, 00) };
        var dates0606 = new DateTime[] { new DateTime(2013, 06, 06, 6, 01, 00) };
        var dates0621 = new DateTime[] { new DateTime(2013, 06, 21, 6, 01, 00) };

        var dates0701 = new DateTime[] { new DateTime(2013, 07, 01, 6, 01, 00) };
        var dates0731 = new DateTime[] { new DateTime(2013, 07, 31, 6, 01, 00) };

        var dates1101 = new DateTime[] { new DateTime(2013, 11, 01, 6, 01, 00) };

        var dates1224 = new DateTime[] { new DateTime(2013, 12, 24, 6, 01, 00) };
        var dates1225 = new DateTime[] { new DateTime(2013, 12, 25, 6, 01, 00) };
        var dates1226 = new DateTime[] { new DateTime(2013, 12, 26, 6, 01, 00) };
        var dates1231 = new DateTime[] { new DateTime(2013, 12, 31, 6, 01, 00) };

        var dates180605 = new DateTime[] { new DateTime(2018, 06, 05, 6, 01, 00) };
        var dates180606 = new DateTime[] { new DateTime(2018, 06, 06, 6, 01, 00) };

        // Act
        var resultShouldHaveFee = calculateCarToll.GetTollFee(car, dates0918);

        var resultNoFee1 = calculateCarToll.GetTollFee(car, dates0101);

        var resultNoFee2 = calculateCarToll.GetTollFee(car, dates0328);
        var resultNoFee3 = calculateCarToll.GetTollFee(car, dates0329);

        var resultNoFee4 = calculateCarToll.GetTollFee(car, dates0401);
        var resultNoFee5 = calculateCarToll.GetTollFee(car, dates0430);

        var resultNoFee6 = calculateCarToll.GetTollFee(car, dates0501);
        var resultNoFee7 = calculateCarToll.GetTollFee(car, dates0508);
        var resultNoFee8 = calculateCarToll.GetTollFee(car, dates0509);

        var resultNoFee9 = calculateCarToll.GetTollFee(car, dates0605);
        var resultNoFee10 = calculateCarToll.GetTollFee(car, dates0606);
        var resultNoFee11 = calculateCarToll.GetTollFee(car, dates0621);

        var resultNoFee12 = calculateCarToll.GetTollFee(car, dates0701);
        var resultNoFee13 = calculateCarToll.GetTollFee(car, dates0731);

        var resultNoFee14 = calculateCarToll.GetTollFee(car, dates1101);

        var resultNoFee15 = calculateCarToll.GetTollFee(car, dates1224);
        var resultNoFee16 = calculateCarToll.GetTollFee(car, dates1225);
        var resultNoFee17 = calculateCarToll.GetTollFee(car, dates1226);
        var resultNoFee18 = calculateCarToll.GetTollFee(car, dates1231);

        var resultNoFee19 = calculateCarToll.GetTollFee(car, dates180605);
        var resultNoFee20 = calculateCarToll.GetTollFee(car, dates180606);

        // Assert
        Assert.Instance.AreEqual(8, resultShouldHaveFee);

        Assert.Instance.AreEqual(0, resultNoFee1);

        Assert.Instance.AreEqual(0, resultNoFee2);
        Assert.Instance.AreEqual(0, resultNoFee3);

        Assert.Instance.AreEqual(0, resultNoFee4);
        Assert.Instance.AreEqual(0, resultNoFee5);

        Assert.Instance.AreEqual(0, resultNoFee6);
        Assert.Instance.AreEqual(0, resultNoFee7);
        Assert.Instance.AreEqual(0, resultNoFee8);

        Assert.Instance.AreEqual(0, resultNoFee9);
        Assert.Instance.AreEqual(0, resultNoFee10);
        Assert.Instance.AreEqual(0, resultNoFee11);

        Assert.Instance.AreEqual(0, resultNoFee12);
        Assert.Instance.AreEqual(0, resultNoFee13);

        Assert.Instance.AreEqual(0, resultNoFee14);

        Assert.Instance.AreEqual(0, resultNoFee15);
        Assert.Instance.AreEqual(0, resultNoFee16);
        Assert.Instance.AreEqual(0, resultNoFee17);
        Assert.Instance.AreEqual(0, resultNoFee18);

        Assert.Instance.AreEqual(0, resultNoFee19);
        Assert.Instance.AreEqual(0, resultNoFee20);
    }
}