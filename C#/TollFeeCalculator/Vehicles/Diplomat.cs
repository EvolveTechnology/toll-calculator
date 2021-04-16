namespace TollFeeCalculator.Vehicles
{
    public class Diplomat : IVehicle
    {
        public string GetVehicleType() => "Diplomat";

        public bool IsTollFree() => true;
    }
}