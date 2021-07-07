namespace TollFeeCalculator.Models
{
    public class Car : IVehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Car;
        }
    }
}