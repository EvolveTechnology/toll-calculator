using System;
using System.Collections.Generic;

namespace TollCalculator.Contracts.Vehicles
{
    /// <summary>
    /// Represents unique vehicle.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Creates vehicle from vehicle registration identifier, country code and vehicle type.
        /// </summary>
        /// <param name="registrationIdentifier">Registration identifier for vehicle. Any non-null value is legal.</param>
        /// <param name="twoLetterCountryCode">Alpha-2 country code for vehicle according to ISO 3166.</param>
        /// <param name="vehicleType">Type of vehicle.</param>
        /// <seealso cref="https://www.iso.org/iso-3166-country-codes.html"/>
        public Vehicle(string registrationIdentifier, string twoLetterCountryCode, VehicleType vehicleType)
        {
            // If no identifier specified, throw exception
            RegistrationIdentifier = registrationIdentifier ?? throw new ArgumentNullException(nameof(registrationIdentifier));
            Iso3166Alpha2CountryCode = twoLetterCountryCode;
            VehicleType = vehicleType;
        }

        /// <summary>
        /// Gets type of the vehicle. See <see cref="VehicleType"/>.
        /// </summary>
        public VehicleType VehicleType { get; }

        /// <summary>
        /// Gets Vehicle Registration Identifier (i.e. registration number)
        /// </summary>
        public string RegistrationIdentifier { get; }

        /// <summary>
        /// Gets ISO 3166 Alpha-2 country code for vehicle.
        /// </summary>
        /// <seealso cref="https://www.iso.org/iso-3166-country-codes.html"/>
        public string Iso3166Alpha2CountryCode { get; }
    }
}
