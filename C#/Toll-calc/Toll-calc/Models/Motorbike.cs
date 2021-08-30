namespace Toll_calc.Models
{
    public class Motorbike : Vehicle
    {
        public string GetVehicleType()
        {
            return "Motorbike";
        }

        public bool IsTollFree()
        {
            return true;
        }
    }
}
