
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
        public VehicleType VehicleType { get; }

        VehicleType GetVehicleType();
    }
}