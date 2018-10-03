
using TollFeeCalculator.Vehicles;
using System;

namespace TollFeeCalculator.Tests
{
    public class TestVehicleFactory
    {
        public Vehicle Generate(VehicleFeeType vehicleFeeType)
        {
            switch (vehicleFeeType)
            {
                case VehicleFeeType.Motorbike:
                    return new Motorbike(
                        new VehicleFeeType[] 
                        { 
                            VehicleFeeType.Motorbike,
                            VehicleFeeType.Foreign,
                        }
                    );

                case VehicleFeeType.Tractor:
                    return new Tractor(
                        new VehicleFeeType[] 
                        { 
                            VehicleFeeType.Tractor,
                            VehicleFeeType.Military,
                        });

                case VehicleFeeType.Emergency:
                    return new Car(
                        new VehicleFeeType[] 
                        { 
                            VehicleFeeType.Car,
                            VehicleFeeType.Emergency,
                        }
                    );

                case VehicleFeeType.Diplomat:
                    return new Car(
                        new VehicleFeeType[] 
                        { 
                            VehicleFeeType.Car,
                            VehicleFeeType.Foreign,
                            VehicleFeeType.Diplomat,
                        }
                    );

                case VehicleFeeType.Foreign:
                    return new Motorbike(
                        new VehicleFeeType[] 
                        { 
                            VehicleFeeType.Motorbike,
                            VehicleFeeType.Foreign,
                        }
                    );

                case VehicleFeeType.Military:
                    return new Tractor(
                        new VehicleFeeType[] 
                        { 
                            VehicleFeeType.Tractor,
                            VehicleFeeType.Military,
                        });

                case VehicleFeeType.Car:
                    return new Car(
                        new VehicleFeeType[] 
                        { 
                            VehicleFeeType.Car,
                        }
                    );

                default:
                    throw new ApplicationException(
                        $"Could not create vehicle of type {vehicleFeeType.ToString()}");
            }
        }
    }
}