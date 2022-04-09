using System.IO;
using TollCalculator.Helpers;
using Xunit;

namespace TollCalculatorTest;

public class ConstantsTest
{
    [Fact]
    public void ReadJsonFile_Should_Return_Valid_Data()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Data/Constants.json");
        Constants.ReadJsonFile(path);
        Assert.Equal(10, Constants.MaximumFeeForOneDay); 
        Assert.Equal(30, Constants.MaximumFeeDependingOnTheTimeOfDay); 
        Assert.Equal(20, Constants.MinimumFeeDependingOnTheTimeOfDay);
        Constants.SetDefaults();
    }
}