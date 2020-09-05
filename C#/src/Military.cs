namespace TollFeeCalculator
{
    public class Military : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}