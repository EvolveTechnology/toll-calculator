namespace TollFeeCalculator.Vehicles
{
    public class Foreign : IVehicle
    {
        public string GetVehicleType() => "Foreign";

        public bool IsTollFree() => true;
    }
}