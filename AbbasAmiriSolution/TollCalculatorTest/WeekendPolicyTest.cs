using System;
using TollCalculator.Policies;
using Xunit;

namespace TollCalculatorTest;

public class WeekendPolicyTest
{
    [Fact]
    public void IsWeekend_Should_Return_True_When_Day_Is_Saturday()
    {
        var policy = new WeekendPolicy();
        Assert.True(policy.IsWeekend(new DateOnly(2022, 7, 9)));
    }
    
    [Fact]
    public void IsWeekend_Should_Return_True_When_Day_Is_Sunday()
    {
        var policy = new WeekendPolicy();
        Assert.True(policy.IsWeekend(new DateOnly(2022, 7, 10)));
    }
  
    [Fact]
    public void IsWeekend_Should_Return_False_When_Day_Is_Monday()
    {
        var policy = new WeekendPolicy();
        Assert.False(policy.IsWeekend(new DateOnly(2022, 7, 11)));
        
    }
   
    [Fact]
    public void IsWeekend_Should_Return_False_When_Day_Is_Tuesday()
    {
        var policy = new WeekendPolicy();
        Assert.False(policy.IsWeekend(new DateOnly(2022, 7, 12)));
    }

    [Fact]
    public void IsWeekend_Should_Return_False_When_Day_Is_Wednesday()
    {
        var policy = new WeekendPolicy();
        Assert.False(policy.IsWeekend(new DateOnly(2022, 7, 13)));
    }
 
    [Fact]
    public void IsWeekend_Should_Return_False_When_Day_Is_Thursday()
    {
        var policy = new WeekendPolicy();
        Assert.False(policy.IsWeekend(new DateOnly(2022, 7, 14)));
    }
   
    [Fact]
    public void IsWeekend_Should_Return_False_When_Day_Is_Friday()
    {
        var policy = new WeekendPolicy();
        Assert.False(policy.IsWeekend(new DateOnly(2022, 7, 15)));
    }
}