namespace TollFeeCalculator.Vehicles
{
    public class Emergency : IVehicle
    {
        public string GetVehicleType() => "Emergency";

        public bool IsTollFree() => true;
    }
}