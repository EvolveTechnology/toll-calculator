namespace Toll_calc.Models
{
    public interface Vehicle
    {
        string GetVehicleType();
        bool IsTollFree();
    }
}