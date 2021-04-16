namespace TollFeeCalculator.Vehicles
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType() => "Motorbike";

        public bool IsTollFree() => true;
    }
}
