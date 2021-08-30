namespace Toll_calc.Models
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
