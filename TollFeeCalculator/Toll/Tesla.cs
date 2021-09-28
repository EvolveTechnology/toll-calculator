namespace TollFeeCalculator.Toll
{
    public class Tesla : VehicleType
    {
        public bool FeeFree => IsFeeFree(this);
        public Enums.TollFreeVehicle VehicleType => GetVehicleType(this);
    }
}
