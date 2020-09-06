namespace TollFeeCalculator.Vehicles
{
    public class Foreign : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}