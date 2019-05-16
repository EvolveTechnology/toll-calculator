using System;
using System.Collections.Generic;
using System.Text;
using TollCalculator.Models;

namespace TollCalculator.Repository
{
    public class TollRepository : ITollRepository
    {
        public Vehicle GetCar() 
        {
            return new Vehicle
            {
                vehicleType = VehicleType.Car
            };
        }

        public List<Vehicle> GetAllTollFreeVehicles()
        {
            var tollFreeVehicles = new List<Vehicle>();
            tollFreeVehicles.Add(new Vehicle{ vehicleType = VehicleType.Motorbike});
            tollFreeVehicles.Add(new Vehicle{ vehicleType = VehicleType.Tractor});
            tollFreeVehicles.Add(new Vehicle{ vehicleType = VehicleType.Emergency});
            tollFreeVehicles.Add(new Vehicle{ vehicleType = VehicleType.Diplomat});
            tollFreeVehicles.Add(new Vehicle{ vehicleType = VehicleType.Foreign});
            tollFreeVehicles.Add(new Vehicle{ vehicleType = VehicleType.Military});

            return tollFreeVehicles;
        
        }

        public List<FeePeriod> GetTollFeePeriods()
        {
            List<FeePeriod> feePeriods = new List<FeePeriod>()
            {
                new FeePeriod()
                {
                    Fee = Fee.Low,
                    Period = new Dictionary<TimeSpan, TimeSpan>(){
                        { new TimeSpan(06, 00, 00), new TimeSpan(06, 30, 00)},
                        { new TimeSpan(08, 30, 00), new TimeSpan(15, 00, 00)},
                        { new TimeSpan(18, 00, 00), new TimeSpan(18, 30, 00)},                      
                    },
                    Price = 8
                },
                new FeePeriod()
                {
                    Fee = Fee.Medium,
                    Period = new Dictionary<TimeSpan, TimeSpan>(){
                        { new TimeSpan(06, 30, 00) ,new TimeSpan(07, 00, 00)},
                        { new TimeSpan(08, 00, 00) ,new TimeSpan(08, 30, 00)},
                        { new TimeSpan(15, 00, 00) ,new TimeSpan(15, 30, 00)},
                        { new TimeSpan(17, 00, 00) ,new TimeSpan(18, 00, 00)},
                    },
                    Price = 13
                },
                new FeePeriod()
                {
                    Fee = Fee.High,
                    Period = new Dictionary<TimeSpan, TimeSpan> {
                        { new TimeSpan(07, 00, 00), new TimeSpan(08, 00, 00) },
                        { new TimeSpan(15, 00, 00), new TimeSpan(17, 00, 00) }
                    },
                    Price = 18
                },
            };
            return feePeriods;
        }


        public List<DateTime> GetDates()
        {
            return new List<DateTime>()
            {
                new DateTime(2013, 12, 2, 16, 30, 55),
                new DateTime(2013, 12, 2, 14, 00, 00),
                new DateTime(2013, 12, 2, 08, 00, 00),
            };
        }
        public List<DateTime> GetDatesWithinHour()
        {
            return new List<DateTime>()
            {
                new DateTime(2013, 12, 2, 06, 58, 00),
                new DateTime(2013, 12, 2, 07, 00, 00),
                new DateTime(2013, 12, 2, 07, 02, 00),
                new DateTime(2013, 12, 2, 07, 57, 00)

            };
        }

        public List<DateTime> GetMaximumTollHours()
        {
            return new List<DateTime>()
            {
                new DateTime(2013, 12, 2, 07, 00, 00),
                new DateTime(2013, 12, 2, 07, 59, 00),
                new DateTime(2013, 12, 2, 16, 59, 00),
            };
        }

        public List<DateTime> GetOverChargeDayToll()
        {
            return new List<DateTime>()
            {
                new DateTime(2013, 12, 2, 06, 58, 00),
                new DateTime(2013, 12, 2, 07, 00, 00),
                new DateTime(2013, 12, 2, 07, 02, 00),
                new DateTime(2013, 12, 2, 07, 57, 00),
                new DateTime(2013, 12, 2, 15, 57, 00),
                new DateTime(2013, 12, 2, 13, 57, 00),
                new DateTime(2013, 12, 2, 11, 57, 00),
                new DateTime(2013, 12, 2, 18, 00, 00),
            };
        }

        public List<DateTime> GetMinimumTollHours()
        {
            return new List<DateTime>()
            {
                new DateTime(2013, 12, 2, 06, 00, 00),
                new DateTime(2013, 12, 2, 06, 29, 00),
                new DateTime(2013, 12, 2, 8, 30, 00),
                new DateTime(2013, 12, 2, 14, 59, 00),
                new DateTime(2013, 12, 2, 18, 00, 00),
                new DateTime(2013, 12, 2, 14, 59, 00),
            };
        }

        public List<DateTime> GetWeekends()
        {
            return new List<DateTime>()
            {
                //Saturday
                new DateTime(2013, 12, 1, 17, 00, 00),
                new DateTime(2013, 12, 8, 06, 30, 00),
                //Sunday
                new DateTime(2013, 12, 7, 8, 30, 00),
                new DateTime(2013, 12, 14, 14, 59, 00)
            };
        }

        public List<DateTime> GetHolidays()
        {
            return new List<DateTime>()
            {
                new DateTime(2013, 12, 31, 17, 00, 00),
                new DateTime(2013, 1, 1, 06, 30, 00)
            };
        }



    }
}

