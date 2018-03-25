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
            IVehicle car = new Car();
            IVehicle motorbike = new Motorbike();
            IVehicle tractor = new Tractor();
            Console.WriteLine(car.IsTollable(vehicleTollPolicy));
            Console.ReadLine();
            Console.WriteLine(motorbike.IsTollable(vehicleTollPolicy));
            Console.ReadLine();
            Console.WriteLine(tractor.IsTollable(vehicleTollPolicy));
            Console.ReadLine();
        }
    }
}
