namespace TollFeeCalculator.Vehicles
{
    public class Tractor : IVehicle
    {
        public string GetVehicleType() => "Tractor";

        public bool IsTollFree() => true;
    }
}
