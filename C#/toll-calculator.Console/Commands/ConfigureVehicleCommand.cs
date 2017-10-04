using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.Console.Commands
{
    internal class ConfigureVehicleCommand : ICommand
    {
        public string Name => "Configure Vehicle";

        public void Execute(Context context)
        {
            try
            {
                System.Console.WriteLine(Name);
                System.Console.WriteLine();
                VehicleType? vehicleType = null;
                do
                {
                    System.Console.WriteLine($"Available Vehicle Types:");
                    System.Console.WriteLine($"{string.Join(", ", Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>())}\r\n");
                    System.Console.Write($"Vehicle Type [{context.Vehicle.VehicleType}]? ");
                }
                while ((vehicleType = CommandHelper.GetEnumInput<VehicleType>(context.Vehicle.VehicleType)) == null);

                System.Console.Write($"Country Code [{context.Vehicle.Iso3166Alpha2CountryCode}]? ");
                var countryCode = CommandHelper.GetStringInput() ?? context.Vehicle.Iso3166Alpha2CountryCode;
                System.Console.Write($"Vehicle Registration Identifier [{context.Vehicle.RegistrationIdentifier}]? ");
                var regId = CommandHelper.GetStringInput() ?? context.Vehicle.RegistrationIdentifier;
                context.Vehicle = new Vehicle(regId, countryCode, vehicleType.Value);
            }
            catch (Exception exception)
            {
                context.Logger.LogError(exception, "Error in {0}: ", Name);
                System.Console.WriteLine(exception.Message);
                System.Console.WriteLine();
            }

            CommandHelper.WaitForKeyPress();
        }
    }
}
