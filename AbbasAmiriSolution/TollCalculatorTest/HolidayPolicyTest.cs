using System;
using System.Collections.Generic;
using TollCalculator.Helpers;
using TollCalculator.Policies;
using Xunit;

namespace TollCalculatorTest;

public class HolidayPolicyTest: IClassFixture<HolidayCollectionFixture>
{
    private readonly HolidayCollectionFixture _collectionFixture;

    public HolidayPolicyTest(HolidayCollectionFixture collectionFixture)
    {
        _collectionFixture = collectionFixture;
    }
    
    [Fact]
    public void IsHoliday_Should_Return_True_When_Date_Is_2022_01_01()
    {
        var policy = new HolidayPolicy(_collectionFixture.HolidayCollection);
        Assert.True(policy.IsHoliday(new DateOnly(2022, 1, 1)));
    }

    [Fact]
    public void IsHoliday_Should_Return_True_When_Date_Is_2022_04_30()
    {
        var policy = new HolidayPolicy(_collectionFixture.HolidayCollection);
        Assert.True(policy.IsHoliday(new DateOnly(2022, 4, 30)));
    }
    [Fact]
    public void IsHoliday_Should_Return_True_When_Date_Is_2022_05_29()
    {
        var policy = new HolidayPolicy(_collectionFixture.HolidayCollection);
        Assert.True(policy.IsHoliday(new DateOnly(2022, 5, 29)));
    }

    [Fact]
    public void IsHoliday_Should_Return_False_When_Date_Is_2022_08_10()
    {
        var policy = new HolidayPolicy(_collectionFixture.HolidayCollection);
        Assert.False(policy.IsHoliday(new DateOnly(2022, 8, 10)));
    }

}

public class HolidayCollectionFixture
{
    public IReadOnlyList<DateOnly> HolidayCollection { get; }

    public HolidayCollectionFixture()
    {
        var holidayBuilder = new HolidayCollectionBuilder();
        holidayBuilder.Add(new DateOnly(2022,1,1));
        holidayBuilder.Add(new DateOnly(2022,1,6));
        holidayBuilder.Add(new DateOnly(2022,4,15));
        holidayBuilder.Add(new DateOnly(2022,4,17));
        holidayBuilder.Add(new DateOnly(2022,4,18));
        holidayBuilder.Add(new DateOnly(2022,4,30));
        holidayBuilder.Add(new DateOnly(2022,5,1));
        holidayBuilder.Add(new DateOnly(2022,5,26));
        holidayBuilder.Add(new DateOnly(2022,5,27));
        holidayBuilder.Add(new DateOnly(2022,5,29));

        HolidayCollection = holidayBuilder.ToReadOnlyList();
    }
}