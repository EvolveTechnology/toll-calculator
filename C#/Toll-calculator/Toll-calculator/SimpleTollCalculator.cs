using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Vehicles;

namespace Toll_calculator {

    public class SimpleTollCalculator : ITollCalculator {

        private const int MAX_DAILY_FEE = 60;
        private const int MIN_FEE_FREQUENCY_IN_MINUTES = 60;

        public IDateTollPolicy DateTollPolicy { get; private set; }
        public IFeePolicy FeePolicy { get; private set; }
        public IVehicleTollPolicy VehicleTollPolicy { get; private set; }

        public SimpleTollCalculator(IDateTollPolicy datePolicy, IFeePolicy feePolicy, IVehicleTollPolicy vehiclePolicy) {
            DateTollPolicy = datePolicy;
            FeePolicy = feePolicy;
            VehicleTollPolicy = vehiclePolicy;
        }

        public int GetTollFee(IVehicle vehicle, DateTime[] times) {
            int totalFee = 0;

            if(!vehicle.IsTollable(VehicleTollPolicy)) {
                return 0;
            }

            /**
             * Groups times based on their date, and calculate the fee for each day.
             */
            DateTime[] dates = times.Select(time => time.Date).Distinct().ToArray();
            foreach (DateTime date in dates) {
                DateTime[] timesOfDate = times.Where(time => time.Date.Equals(date.Date)).ToArray();
                totalFee += GetDailyTollFee(vehicle, timesOfDate);
            }
            return totalFee;
        }

        private int GetDailyTollFee(IVehicle vehicle, DateTime[] times) {
            int totalFee = 0;
            DateTime lastFeeTime = DateTime.MinValue;

            /**
             * We know that times only contains timestamps of a single date, so
             * we can only check if the first element has a tollable date.
             */
            if (times.Length == 0 || !DateTollPolicy.IsTollable(times[0])) {
                return 0;
            }

            times = times.OrderBy(time => time).ToArray();
            foreach(DateTime time in times) {
                int timeSinceLastFee = Utils.TimeBetweenTimestampsInMinutes(lastFeeTime, time);
                if (timeSinceLastFee >= MIN_FEE_FREQUENCY_IN_MINUTES) {
                    totalFee += FeePolicy.GetFee(time);
                    lastFeeTime = time;
                }
                if(totalFee >= MAX_DAILY_FEE) {
                    break;
                }
            }

            return Math.Min(MAX_DAILY_FEE, totalFee);
        }

    }
}
