using System;
using System.Linq;

namespace toll_calculator
{
    public class Vehicle : IVehicle
    {
        ITollFeeAggregator _aggregator;

        public Vehicle(ITollFeeAggregator aggregator, VehicleType type)
        {
            _aggregator = aggregator;
            Type = type;
        }

        public VehicleType Type { get;}

        public int GetTotalFee(DateTime[] datetimes)
        {
            return _aggregator.GetTotalToll(datetimes.ToList());
        }

        public VehicleType GetVehicleType()
        {
            return Type;
        }
    }
}
