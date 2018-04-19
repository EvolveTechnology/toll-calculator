namespace TollFeeCalculator
{
    public class Emergency : Vehicle
    {
        public string GetVehicleType()
        {
            return "Emergency";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
