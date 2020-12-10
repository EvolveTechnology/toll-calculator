namespace TollFeeCalculator
{
    public enum VehicleType
    {
        Car,
        Motorbike,
        Tractor,
        Emergency,
        Diplomat,
        Foreign,
        Military
    }

    public interface IVehicle
    {
        VehicleType VehicleType { get; }
    }

}