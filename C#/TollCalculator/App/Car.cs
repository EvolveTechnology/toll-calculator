using TollCalculatorApp;

namespace TollFeeCalculator
{
    public class Car : IVehicle
    {
        public VehicleType GetVehicleType()
        {
            return VehicleType.Car;
        }
    }
}