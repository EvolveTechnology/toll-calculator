namespace TollFeeCalculator.Toll
{
    public class Motorbike:VehicleType
    {
        public bool FeeFree=>IsFeeFree(this);
        public Enums.TollFreeVehicle VehicleType => GetVehicleType(this);
    }
}
