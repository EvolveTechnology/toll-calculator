using System;
using System.Collections.Generic;
using System.IO;
using TollCalculator;
using TollCalculator.Enums;
using TollCalculator.Helpers;
using Xunit;

namespace TollCalculatorTest;

public class DayTimeFeeCollectionBuilderTest
{
    [Fact]
    public void ReadJsonFile_Should_Return_Valid_Data()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/DayTimeFee.json");
        var builder = new DayTimeFeeCollectionBuilder();
        builder.ReadJsonFile(path);
        
        var traffic = new List<DateTime>
        {
            new DateTime(2022, 4, 8, 2, 25, 0),
            new DateTime(2022, 4, 8, 9, 10, 5),
            new DateTime(2022, 4, 8, 10, 20, 0),
            new DateTime(2022, 4, 8, 10, 40, 50),
            new DateTime(2022, 4, 8, 11, 15, 10),
        };
        
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

        var calculator = new TollFeeCalculator(builder.ToReadOnlyList(), holidayBuilder.ToReadOnlyList());
        Assert.Equal(54, calculator.CalculateTollFee(traffic.ToArray(), VehicleType.Private));
    }
}