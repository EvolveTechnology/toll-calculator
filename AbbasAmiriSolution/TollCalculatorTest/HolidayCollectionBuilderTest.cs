using System;
using System.IO;
using TollCalculator.Helpers;
using Xunit;

namespace TollCalculatorTest;

public class HolidayCollectionBuilderTest
{
    [Fact]
    public void ReadJsonFile_Should_Return_Valid_Data()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/Holidays.json");
        var builder = new HolidayCollectionBuilder();
        builder.ReadJsonFile(path);
        Assert.Equal(new DateOnly(2022,4,30), builder.ToArray()[5]); 
    }
}