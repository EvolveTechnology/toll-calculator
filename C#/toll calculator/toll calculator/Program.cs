using System;
using System.Collections.Generic;
using toll_calculator_logic;

namespace toll_calculator_app
{
    class Program
    {
        static void Main(string[] args)
        {
            var datelist = new List<DateTime>();

            Console.WriteLine("Now I will help you to calculate your today Toll Fee here");
            Console.ReadKey();

            Console.WriteLine("Enter the number correponds to your vehicle:");
            Console.WriteLine("1 - Car ");
            Console.WriteLine("2 - Motorbike ");
            Console.WriteLine("3 - Tractor");
            Console.WriteLine("4 - Emergency ");
            Console.WriteLine("5 - Diplomat ");
            Console.WriteLine("6 - Foreign ");
            Console.WriteLine("7 - Military ");
            Console.WriteLine("8 - None ");

            int.TryParse(Console.ReadLine().ToString(), out int carType);
            var vehicle = GetVehicleByTypeCode(carType);

            Console.WriteLine("Enter Year:");
            int.TryParse(Console.ReadLine().ToString(), out int year);

            Console.WriteLine("Enter Month:");
            int.TryParse(Console.ReadLine().ToString(), out int month);

            Console.WriteLine("Enter Day:");
            int.TryParse(Console.ReadLine().ToString(), out int day);

            while (true)
            {
                Console.WriteLine("Enter hour:");
                int.TryParse(Console.ReadLine().ToString(), out int hour);

                Console.WriteLine("Enter minute:");
                int.TryParse(Console.ReadLine().ToString(), out int minute);

                datelist.Add(new DateTime(year, month, day, hour, minute, 0));
                Console.WriteLine("Do you want to add another passing time for today? (Y/N)");
                char.TryParse(Console.ReadLine(), out char yesNo);

                if (yesNo == 'n' || yesNo == 'N')
                    break;
            }

            var tollCalculator = new TollCalculator();

            var fee = tollCalculator.GetTollFee(vehicle, datelist.ToArray());
            Console.WriteLine();

            Console.WriteLine(new DateTime(year, month, day).ToShortDateString() + " and total toll fee is " + fee + "**");
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }

        private static IVehicle GetVehicleByTypeCode(int code) => code switch
        {
            1 => new Car(),
            2 => new Motorbike(),
            3 => new Tractor(),
            4 => new Emergency(),
            5 => new Diplomat(),
            6 => new Foreign(),
            7 => new Military(),

            _ => throw new Exception(message:"The Vehicle Type is not defined!")
        };
    }
}
