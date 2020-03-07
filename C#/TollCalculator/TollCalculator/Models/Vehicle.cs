namespace TollFeeCalculator.Models
{
    public class Vehicle : IVehicle
    {
        public VehicleType VehicleType { get; }

        public Vehicle(VehicleType vehicleType)
        {
            VehicleType = vehicleType;
        }

        public VehicleType GetVehicleType()
        {
            return this.VehicleType;
        }
    }
}
