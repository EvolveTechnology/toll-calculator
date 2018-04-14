namespace TollFeeCalculator
{
    public class Foreign : Vehicle
    {
        public string GetVehicleType()
        {
            return "Foreign";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
