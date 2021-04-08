using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Nager.Date;
using Toll.Calculator.DAL.Repositories.Interface;
using Toll.Calculator.Domain;
using Toll.Calculator.Infrastructure.Options;

namespace Toll.Calculator.DAL.Repositories
{
    public class TollFeeRepository : ITollFeeRepository
    {
        private readonly List<PassageFee> _passageFees;

        public TollFeeRepository(
            IOptions<FeeTimeZonesOptions> feeTimeZoneOptions)
        {
            _passageFees = InitializePassageFees(feeTimeZoneOptions.Value.FeeTimeZones);
        }

        public async Task<decimal> GetPassageFeeByTimeAsync(DateTime passageTime)
        {
            //Simulate db access
            await Task.Delay(TimeSpan.FromMilliseconds(5));

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

        public async Task<bool> IsTollFreeDateAsync(DateTime passageTime)
        {
            //Simulate db access
            await Task.Delay(TimeSpan.FromMilliseconds(5));

            if (DateSystem.IsPublicHoliday(passageTime, CountryCode.SE) ||
                passageTime.DayOfWeek == DayOfWeek.Saturday ||
                passageTime.DayOfWeek == DayOfWeek.Sunday)
                return true;

            return false;
        }

        public async Task<decimal> GetMaximumDailyFeeAsync()
        {
            //Simulate db access
            await Task.Delay(TimeSpan.FromMilliseconds(5));

            return 60;
        }

        public async Task<TimeSpan> GetPassageLeewayInterval()
        {
            //Simulate db access
            await Task.Delay(TimeSpan.FromMilliseconds(5));

            return TimeSpan.FromMinutes(60);
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

                var startTime = new DateTime(1, 1, 1, Convert.ToInt32(startStringHour),
                    Convert.ToInt32(startStringMinute), 0);
                var endTime = new DateTime(1, 1, 1, Convert.ToInt32(endStringHour), Convert.ToInt32(endStringMinute),
                    0);

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