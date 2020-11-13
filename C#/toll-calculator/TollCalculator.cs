using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace toll_calculator
{
    public class TollCalculator
    {
        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total toll fee for that day
         */
        private IVehicleFactory _factory;
        private ISchemas _schemas;


        public TollCalculator(List<string> schemapaths)
        {
            SetupFactory();
            SetupSchemas(schemapaths);
        }

        public TollCalculator(ISchemas schemas, IVehicleFactory factory)
        {
            _factory = factory;
            //SetupFactory();
            _schemas = schemas;
        }

        public int GetTollFee(VehicleType vehicleType, DateTime[] dates)
        {
            if (dates.Length == 0)
                throw new InvalidDateRangeException("Date must be set");

            if (InvalidDates(dates))
                throw new InvalidDateRangeException("Dates out of range.");

            if (_schemas.GetSchemaForYear(dates[0].Year).IsAFreeDay(dates[0]))
                return 0;
            IVehicle currentVehicle = _factory.GetVehicle(vehicleType);

            return currentVehicle.GetTotalFee(dates);
        }

        private void SetupFactory()
        {
            _factory = new VehicleFactory();

            _factory.RegisterVehicle(new Vehicle(new FreeTollAggregator(), VehicleType.Diplomat));
            _factory.RegisterVehicle(new Vehicle(new FreeTollAggregator(), VehicleType.Emergency));
            _factory.RegisterVehicle(new Vehicle(new FreeTollAggregator(), VehicleType.Foreign));
            _factory.RegisterVehicle(new Vehicle(new FreeTollAggregator(), VehicleType.Military));
            _factory.RegisterVehicle(new Vehicle(new FreeTollAggregator(), VehicleType.Motorbike));
            _factory.RegisterVehicle(new Vehicle(new FreeTollAggregator(), VehicleType.Tractor));
        }

        private void SetupSchemas(List<string> schemapaths)
        {
            _schemas = new Schemas();
            foreach (string path in schemapaths)
            {
                _schemas.RegisterSchemaForYear(new YearSchema(path));
            }
        }

        private bool InvalidDates(DateTime[] dates)
        {
            return dates.Any(d => d.Date != dates[0].Date);
        }
    }
}