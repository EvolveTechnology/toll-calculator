namespace TollFeeCalculator.Vehicles
{
    public class Emergency : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}