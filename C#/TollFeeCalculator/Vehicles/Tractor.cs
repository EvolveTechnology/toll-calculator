namespace TollFeeCalculator
{
    public class Tractor : Vehicle
    {
        public string GetVehicleType()
        {
            return "Tractor";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
