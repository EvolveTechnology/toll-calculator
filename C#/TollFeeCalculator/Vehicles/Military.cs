namespace TollFeeCalculator.Vehicles
{
    public class Military : IVehicle
    {
        public string GetVehicleType() => "Military";

        public bool IsTollFree() => true;
    }
}