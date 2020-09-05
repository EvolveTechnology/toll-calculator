namespace TollFeeCalculator
{
    public class Emergency : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}