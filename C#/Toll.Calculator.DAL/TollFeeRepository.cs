using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Nager.Date;
using Toll.Calculator.DAL.Interface;
using Toll.Calculator.Domain;
using Toll.Calculator.Infrastructure;

namespace Toll.Calculator.DAL
{
    public class TollFeeRepository : ITollFeeRepository
    {
        private readonly List<PassageFee> _passageFees;

        public TollFeeRepository(
            IOptions<FeeTimeZonesOptions> feeTimeZoneOptions)
        {
            _passageFees = InitializePassageFees(feeTimeZoneOptions.Value.FeeTimeZones);
        }

        public decimal GetPassageFeeByTime(DateTime passageTime)
        {
            var passageTimeStamp = new TimeStamp(passageTime);

            var passageFees = _passageFees
                .Where(p => p.StartTime <= passageTimeStamp &&
                            p.EndTime >= passageTimeStamp)
                .ToList();

            if (!passageFees.Any())
                return 0;

            if (passageFees.Count > 1)
                throw new Exception("Error when fetching PassageFees");

            return passageFees.First().Fee;
        }

        public bool IsTollFreeDate(DateTime passageTime)
        {
            if (DateSystem.IsPublicHoliday(passageTime, CountryCode.SE) ||
                passageTime.DayOfWeek == DayOfWeek.Saturday ||
                passageTime.DayOfWeek == DayOfWeek.Sunday)
                return true;

            return false;
        }

        private List<PassageFee> InitializePassageFees(List<string> passageFeeStrings)
        {
            var passageFees = new List<PassageFee>();

            foreach (var passageFeeString in passageFeeStrings)
            {
                var times = passageFeeString.Split(';')[0];
                var fee = passageFeeString.Split(';')[1];

                var startString = times.Split('-')[0];
                var endString = times.Split('-')[1];

                var startStringHour = startString.Split(':')[0];
                var startStringMinute = startString.Split(':')[1];

                var endStringHour = endString.Split(':')[0];
                var endStringMinute = endString.Split(':')[1];

                var startTime = new DateTime(1, 1, 1, Convert.ToInt32(startStringHour), Convert.ToInt32(startStringMinute), 0);
                var endTime = new DateTime(1, 1, 1, Convert.ToInt32(endStringHour), Convert.ToInt32(endStringMinute), 0);

                passageFees.Add(new PassageFee
                {
                    Fee = Convert.ToDecimal(fee),
                    StartTime = new TimeStamp(startTime),
                    EndTime = new TimeStamp(endTime)
                });
            }

            return passageFees;
        }
    }
}