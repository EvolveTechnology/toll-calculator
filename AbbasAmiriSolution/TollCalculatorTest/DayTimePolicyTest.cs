using System;
using System.Collections.Generic;
using TollCalculator.Helpers;
using TollCalculator.Models;
using TollCalculator.Policies;
using Xunit;

namespace TollCalculatorTest;

public class DayTimePolicyTest : IClassFixture<DayTimeFeeCollectionFixture>
{
    private DayTimeFeeCollectionFixture _collectionFixture;

    public DayTimePolicyTest(DayTimeFeeCollectionFixture collectionFixture)
    {
        _collectionFixture = collectionFixture;
    }

    [Fact]
    public void Calculate_Should_Return_10_When_Time_Is_9_10_30()
    {
        var policy = new DayTimePolicy(_collectionFixture.DayTimeFeeCollection);
        Assert.Equal(10, policy.Calculate(new TimeOnly(9, 10, 30)));
    }

    [Fact]
    public void Calculate_Should_Return_Highest_Fee_When_Time_Is_10_20_15()
    {
        var policy = new DayTimePolicy(_collectionFixture.DayTimeFeeCollection);
        Assert.Equal(Constants.MaximumFeeDependingOnTheTimeOfDay, policy.Calculate(new TimeOnly(10, 20, 15)));
    }

    [Fact]
    public void Calculate_Should_Return_15_When_Time_Is_14_00_00()
    {
        var policy = new DayTimePolicy(_collectionFixture.DayTimeFeeCollection);
        Assert.Equal(15, policy.Calculate(new TimeOnly(14, 0, 0)));
    }

    [Fact]
    public void Calculate_Should_Return_Null_When_Time_Is_20_00_00()
    {
        var policy = new DayTimePolicy(_collectionFixture.DayTimeFeeCollection);
        Assert.Null(policy.Calculate(new TimeOnly(20, 0, 0)));
    }

    [Fact]
    public void Calculate_Should_Return_8_When_Time_Is_01_00_00()
    {
        var policy = new DayTimePolicy(_collectionFixture.DayTimeFeeCollection);
        Assert.Equal(8, policy.Calculate(new TimeOnly(1, 0, 0)));
    }

    [Fact]
    public void Calculate_Should_Return_8_When_Time_Is_00_00_00()
    {
        var policy = new DayTimePolicy(_collectionFixture.DayTimeFeeCollection);
        Assert.Equal(8, policy.Calculate(new TimeOnly(0, 0, 0)));
    }
    
    [Fact]
    public void Calculate_Should_Return_8_When_Time_Is_07_59_59()
    {
        var policy = new DayTimePolicy(_collectionFixture.DayTimeFeeCollection);
        Assert.Equal(8, policy.Calculate(new TimeOnly(7, 59, 59)));
    }

}

public class DayTimeFeeCollectionFixture
{
    public IReadOnlyList<DayTimeFee> DayTimeFeeCollection { get; }

    public DayTimeFeeCollectionFixture()
    {
        var dayTimeFeeTableBuilder = new DayTimeFeeCollectionBuilder();
        dayTimeFeeTableBuilder.Add(
            DayTimeFee.Create(new TimeOnly(8, 0, 0, 0), new TimeOnly(9, 59, 59, 59), 10));
        dayTimeFeeTableBuilder.Add(
            DayTimeFee.CreateRushHour(new TimeOnly(10, 0, 0, 0), new TimeOnly(11, 59, 59, 59)));
        dayTimeFeeTableBuilder.Add(
            DayTimeFee.Create(new TimeOnly(12, 0, 0, 0), new TimeOnly(17, 59, 59, 59), 15));
        // dayTimeFeeTableBuilder.Add(DayTimeFee.Create(new TimeOnly(18, 0, 0,0), new TimeOnly(23, 59, 59, 59), 10));
        dayTimeFeeTableBuilder.Add(
            DayTimeFee.Create(new TimeOnly(0, 0, 0, 0), new TimeOnly(7, 59, 59, 59), 8));
        DayTimeFeeCollection = dayTimeFeeTableBuilder.ToReadOnlyList();
    }
}