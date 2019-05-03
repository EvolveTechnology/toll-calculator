namespace TollFeeCalculator.Vehicles
{
    public class Emergency : IVehicle
    {
        public string GetVehicleType()
        {
            return "Emergency";
        }
    }
}