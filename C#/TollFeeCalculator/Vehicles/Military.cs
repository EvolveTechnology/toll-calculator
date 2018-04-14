namespace TollFeeCalculator
{
    public class Military : Vehicle
    {
        public string GetVehicleType()
        {
            return "Military";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
