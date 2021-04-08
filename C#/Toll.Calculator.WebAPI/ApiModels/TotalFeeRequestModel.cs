using System;
using Toll.Calculator.Infrastructure.CustomExceptions;

namespace Toll.Calculator.WebAPI.ApiModels
{
    public class TotalFeeRequestModel
    {
        public Vehicle VehicleType { get; set; }
        public DateTime[] PassageDates { get; set; }

        public enum Vehicle
        {
            Motorbike = 0,
            Tractor = 1,
            Emergency = 2,
            Diplomat = 3,
            Foreign = 4,
            Military = 5,
            Car = 6
        }

        public Domain.Vehicle VehicleTypeToDomain()
        {
            try
            {
                var domainEnum = (Domain.Vehicle) Enum.Parse(typeof(Domain.Vehicle), VehicleType.ToString());

                return domainEnum;
            }
            catch (Exception)
            {
                throw new EnumCastException();
            }
        }
    }
}