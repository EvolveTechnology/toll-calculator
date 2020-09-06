namespace TollFeeCalculator.Vehicles
{
    public class Military : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}