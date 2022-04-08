using TollCalculator.Enums;
using TollCalculator.Policies;
using Xunit;

namespace TollCalculatorTest;

public class VehicleTypePolicyTest
{
    [Fact]
    public void IsFeeFree_Should_Return_True_When_Vehicle_Type_Is_Diplomat()
    {
        var policy = new VehicleTypePolicy();
        Assert.True(policy.IsFeeFree(VehicleType.Diplomat));
    }

    [Fact]
    public void IsFeeFree_Should_Return_True_When_Vehicle_Type_Is_Emergency()
    {
        var policy = new VehicleTypePolicy();
        Assert.True(policy.IsFeeFree(VehicleType.Emergency));
    }

    [Fact]
    public void IsFeeFree_Should_Return_True_When_Vehicle_Type_Is_Foreign()
    {
        var policy = new VehicleTypePolicy();
        Assert.True(policy.IsFeeFree(VehicleType.Foreign));
    }

    [Fact]
    public void IsFeeFree_Should_Return_True_When_Vehicle_Type_Is_Military()
    {
        var policy = new VehicleTypePolicy();
        Assert.True(policy.IsFeeFree(VehicleType.Military));
    }

    [Fact]
    public void IsFeeFree_Should_Return_True_When_Vehicle_Type_Is_Motorbike()
    {
        var policy = new VehicleTypePolicy();
        Assert.True(policy.IsFeeFree(VehicleType.Motorbike));
    }

    [Fact]
    public void IsFeeFree_Should_Return_True_When_Vehicle_Type_Is_Tractor()
    {
        var policy = new VehicleTypePolicy();
        Assert.True(policy.IsFeeFree(VehicleType.Tractor));
    }

    [Fact]
    public void IsFeeFree_Should_Return_False_When_Vehicle_Type_Is_Tractor()
    {
        var policy = new VehicleTypePolicy();
        Assert.False(policy.IsFeeFree(VehicleType.Private));
    }
}