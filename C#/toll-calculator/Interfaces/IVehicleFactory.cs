namespace toll_calculator
{
    public interface IVehicleFactory
    {
        IVehicle GetVehicle(VehicleType vehicle);
        void RegisterVehicle(Vehicle vehicle);
    }
}
