namespace TollFeeCalculator.Vehicle
{
    public class Emergency : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}
