namespace TollFeeCalculator.Vehicle
{
    public class Motorbike : IVehicle
    {
        /// <inheritdoc />
        public bool IsTollFree()
        {
            return true;
        }
    }
}
