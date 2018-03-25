using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_calculator {
    class Program {
        static void Main(string[] args) {
            IHolidayChecker holidayChecker = new Sweden2018HolidayChecker();
            Console.WriteLine("test...");
            Console.ReadLine();
            Console.WriteLine(holidayChecker.IsHoliday(new DateTime(2017, 1, 1)));
            Console.ReadLine();
            Console.WriteLine(holidayChecker.IsHoliday(new DateTime(2015, 1, 2)));
            Console.ReadLine();
            Console.WriteLine(holidayChecker.IsHoliday(new DateTime(2018, 11, 3)));
            Console.ReadLine();
        }
    }
}
