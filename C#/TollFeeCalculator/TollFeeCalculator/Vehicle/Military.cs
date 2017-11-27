namespace TollFeeCalculator.Vehicle
{
    public class Military : IVehicle
    {
        public bool IsTollFree()
        {
            return true;
        }
    }
}
