namespace TollFeeCalculator.Vehicle
{
    public class Foreign : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}
