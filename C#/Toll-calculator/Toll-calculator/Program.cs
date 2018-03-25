using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toll_calculator.Holidays;
using Toll_calculator.Vehicles;

namespace Toll_calculator {
    class Program {
        static void Main(string[] args) {
            IVehicleTollPolicy vehicleTollPolicy = new StandardVehicleTollPolicy();
            IHolidayChecker holidayChecker = new Sweden2018HolidayChecker();
            IDateTollPolicy dateTollPolicy = new StandardDateTollPolicy(holidayChecker);
            IFeePolicy feePolicy = new StandardFeePolicy();
            ITollCalculator tollCalculator = new SimpleTollCalculator(dateTollPolicy, feePolicy, vehicleTollPolicy);

            IVehicle car = new Car();
            DateTime[] times = new DateTime[5] {
                new DateTime(2018, 3, 25, 9, 31, 0),
                new DateTime(2018, 3, 26, 9, 31, 0),
                new DateTime(2018, 3, 26, 9, 30, 0),
                new DateTime(2018, 3, 26, 11, 00, 0),
                new DateTime(2018, 3, 27, 8, 0, 0)
            };

            int fee = tollCalculator.GetTollFee(car, times);
            Console.Write("Total fee: " + fee);

            Console.ReadLine();
        }
    }
}
