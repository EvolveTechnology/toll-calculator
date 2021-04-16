using System;

namespace TollFeeCalculator.Vehicles
{
    public static class VehicleFactory
    {
        public static IVehicle Create(VehicleType type)
        {
            return type switch
            {
                VehicleType.Car => new Car(),
                VehicleType.Truck => new Truck(),
                VehicleType.Motorbike => new Motorbike(),
                VehicleType.Tractor => new Tractor(),
                VehicleType.Emergency => new Emergency(),
                VehicleType.Diplomat => new Diplomat(),
                VehicleType.Foreign => new Foreign(),
                VehicleType.Military => new Military(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }

    public enum VehicleType
    {
        Car,
        Truck,
        Motorbike,
        Tractor,
        Emergency,
        Diplomat,
        Foreign,
        Military
    }
}