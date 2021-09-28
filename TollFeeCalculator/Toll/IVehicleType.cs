namespace TollFeeCalculator.Toll
{
    public interface IVehicleType
    {
        bool IsFeeFree(object  t);
        Enums.TollFreeVehicle GetVehicleType(object t);
    }
}
