using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Vehicles;
using System.Linq;

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

            /**
             * Groups times based on their date, and calculate the fee for each day.
             */
            DateTime[] dates = times.Select(time => time.Date).Distinct().ToArray();
            foreach (DateTime date in dates) {
                DateTime[] timesOfDate = times.Where(time => time.Date.Equals(date.Date)).ToArray();
                int dailyFee = GetDailyTollFee(vehicle, timesOfDate);
                totalFee += dailyFee;
            }
            return totalFee;
        }

        private int GetDailyTollFee(IVehicle vehicle, DateTime[] times) {
            int totalFee = 0;
            DateTime lastFeeTime = DateTime.MinValue;

            times = times.OrderBy(time => time).ToArray();
            foreach(DateTime time in times) {
                int timeSinceLastFee = Utils.TimeBetweenTimestampsInMinutes(lastFeeTime, time);
                if (timeSinceLastFee >= MIN_FEE_FREQUENCY_IN_MINUTES) {
                    totalFee += GetSingleTollFee(vehicle, time);
                    lastFeeTime = time;
                }
                if(totalFee >= MAX_DAILY_FEE) {
                    break;
                }
            }

            return Math.Min(MAX_DAILY_FEE, totalFee);
        }

        private int GetSingleTollFee(IVehicle vehicle, DateTime time) {
            if(!DateTollPolicy.IsTollable(time) || !vehicle.IsTollable(VehicleTollPolicy)) {
                return 0;
            }
            return FeePolicy.GetFee(time);
        }

    }
}
