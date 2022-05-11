using System;
using System.Collections.Generic;
using TollCalculator;
using TollCalculator.Enums;
using TollCalculator.Helpers;
using TollCalculator.Models;
using TollCalculator.Policies;
using Xunit;

namespace TollCalculatorTest;

public class TollFeeCalculatorTest : IClassFixture<TollFeeCalculatorFixture>
{
    private TollFeeCalculatorFixture _fixture;

    public TollFeeCalculatorTest(TollFeeCalculatorFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void CalculateTollFee_Should_Return_0_When_Traffic_Is_In_Holiday()
    {
        var traffic = new List<DateTime>
        {
            new DateTime(2022, 5, 29, 10, 25, 0),
            new DateTime(2022, 5, 29, 14, 10, 5),
            new DateTime(2022, 5, 29, 17, 0, 0),
            new DateTime(2022, 5, 29, 19, 21, 50),
            new DateTime(2022, 5, 29, 22, 10, 10),
        };

        var calculator = new TollFeeCalculator(_fixture.DayTimeFeeCollection, _fixture.HolidayCollection);
        Assert.Equal(0, calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Private));
    }

    [Fact]
    public void CalculateTollFee_Should_Return_0_When_Traffic_Is_In_Weekend()
    {
        var traffic = new List<DateTime>
        {
            new DateTime(2022, 4, 9, 10, 25, 0),
            new DateTime(2022, 4, 9, 14, 10, 5),
            new DateTime(2022, 4, 9, 17, 0, 0),
            new DateTime(2022, 4, 9, 19, 21, 50),
            new DateTime(2022, 4, 9, 22, 10, 10),
        };

        var calculator = new TollFeeCalculator(_fixture.DayTimeFeeCollection, _fixture.HolidayCollection);
        Assert.Equal(0, calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Private));
    }

    [Fact]
    public void CalculateTollFee_Should_Return_0_When_VehicleType_Is_Fee_Free()
    {
        var traffic = new List<DateTime>
        {
            new DateTime(2022, 4, 8, 10, 25, 0),
            new DateTime(2022, 4, 8, 14, 10, 5),
            new DateTime(2022, 4, 8, 17, 0, 0),
            new DateTime(2022, 4, 8, 19, 21, 50),
            new DateTime(2022, 4, 8, 22, 10, 10),
        };

        var calculator = new TollFeeCalculator(_fixture.DayTimeFeeCollection, _fixture.HolidayCollection);
        Assert.Equal(0, calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Emergency));
    }

    [Fact]
    public void CalculateTollFee_Should_Return_54_When_Traffic_Is_Chargeable()
    {
        var traffic = new List<DateTime>
        {
            new DateTime(2022, 4, 8, 2, 25, 0),
            new DateTime(2022, 4, 8, 9, 10, 5),
            new DateTime(2022, 4, 8, 10, 20, 0),
            new DateTime(2022, 4, 8, 10, 40, 50),
            new DateTime(2022, 4, 8, 11, 15, 10),
        };

        var calculator = new TollFeeCalculator(_fixture.DayTimeFeeCollection, _fixture.HolidayCollection);
        Assert.Equal(42, calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Private));
    }

    [Fact]
    public void CalculateTollFee_Should_Return_Maximum_Fee_When_Traffic_Fee_Is_Greater_Than_Maximum()
    {
        var traffic = new List<DateTime>
        {
            new DateTime(2022, 4, 8, 8, 25, 25),
            new DateTime(2022, 4, 8, 9, 25, 25),
            new DateTime(2022, 4, 8, 11, 25, 5),
            new DateTime(2022, 4, 8, 14, 20, 0),
            new DateTime(2022, 4, 8, 20, 25, 0),
            new DateTime(2022, 4, 8, 22, 25, 0),
            new DateTime(2022, 4, 8, 23, 40, 0),
        };

        var calculator = new TollFeeCalculator(_fixture.DayTimeFeeCollection, _fixture.HolidayCollection);
        Assert.Equal(Constants.MaximumFeeForOneDay,
            calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Private));
    }

    [Fact]
    public void CalculateTollFee_Should_Return_0_When_There_Is_No_Traffic()
    {
        var traffic = new List<DateTime>();

        var calculator = new TollFeeCalculator(_fixture.DayTimeFeeCollection, _fixture.HolidayCollection);
        Assert.Equal(0, calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Private));
    }

    [Fact]
    public void CalculateTollFee_Should_Throw_Exception_If_Traffic_Array_Has_Different_Dates()
    {
        var traffic = new List<DateTime>
        {
            new DateTime(2022, 4, 8, 8, 25, 25),
            new DateTime(2022, 5, 8, 9, 25, 25),
            new DateTime(2022, 4, 8, 11, 25, 5),
            new DateTime(2022, 2, 8, 14, 20, 0),
            new DateTime(2022, 1, 8, 20, 25, 0),
        };

        var calculator = new TollFeeCalculator(_fixture.DayTimeFeeCollection, _fixture.HolidayCollection);
        Assert.Throws<ArgumentException>(() => calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Private));
    }
}

public class TollFeeCalculatorFixture
{
    public IReadOnlyList<DayTimeFee> DayTimeFeeCollection { get; private set; }
    public IReadOnlyList<DateOnly> HolidayCollection { get; private set; }

    public TollFeeCalculatorFixture()
    {
        CreateHolidayCollection();
        CreateDayTimeFeeCollection();
    }

    private void CreateDayTimeFeeCollection()
    {
        var dayTimeFeeTableBuilder = new DayTimeFeeCollectionBuilder();


        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(0, 0, 0, 0),
            new TimeOnly(5, 59, 59, 59),
            8));

        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(6, 0, 0, 0),
            new TimeOnly(7, 59, 59, 59),
            10));

        dayTimeFeeTableBuilder.Add(DayTimeFee.CreateRushHour(
            new TimeOnly(8, 0, 0, 0),
            new TimeOnly(9, 59, 59, 59)));

        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(10, 0, 0, 0),
            new TimeOnly(10, 29, 59, 59),
            16));

        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(10, 30, 0, 0),
            new TimeOnly(10, 59, 59, 59),
            14));

        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(11, 0, 0, 0),
            new TimeOnly(11, 29, 59, 59),
            12));

        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(11, 30, 0, 0),
            new TimeOnly(11, 59, 59, 59),
            10));

        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(12, 0, 0, 0),
            new TimeOnly(16, 59, 59, 59),
            9));

        dayTimeFeeTableBuilder.Add(DayTimeFee.Create(
            new TimeOnly(17, 0, 0, 0),
            new TimeOnly(23, 59, 59, 59),
            8));

        DayTimeFeeCollection = dayTimeFeeTableBuilder.ToReadOnlyList();
    }

    private void CreateHolidayCollection()
    {
        var holidayBuilder = new HolidayCollectionBuilder();
        holidayBuilder.Add(new DateOnly(2022, 1, 1));
        holidayBuilder.Add(new DateOnly(2022, 1, 6));
        holidayBuilder.Add(new DateOnly(2022, 4, 15));
        holidayBuilder.Add(new DateOnly(2022, 4, 17));
        holidayBuilder.Add(new DateOnly(2022, 4, 18));
        holidayBuilder.Add(new DateOnly(2022, 4, 30));
        holidayBuilder.Add(new DateOnly(2022, 5, 1));
        holidayBuilder.Add(new DateOnly(2022, 5, 26));
        holidayBuilder.Add(new DateOnly(2022, 5, 27));
        holidayBuilder.Add(new DateOnly(2022, 5, 29));

        HolidayCollection = holidayBuilder.ToReadOnlyList();
    }
}