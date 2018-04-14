namespace TollFeeCalculator
{
    public class Diplomat : Vehicle
    {
        public string GetVehicleType()
        {
            return "Diplomat";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
