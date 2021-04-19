namespace TollFeeCalculator.Vehicles
{
    public class Truck : IVehicle
    {
        public string GetVehicleType() => "Truck";

        public bool IsTollFree() => false;
    }
}