using System;
using System.Collections.Generic;
using Toll_calc.Models;
using Toll_calc.Services;

namespace Toll_calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var holidayService = new HolidayService();
            var calculator = new TollCalculator(holidayService);
            var vehicle = new Car();
            var passTimes = new DateTime[]
                {
                    new DateTime(2021, 9, 1,08,30,0),
                    new DateTime(2021, 9, 1, 06,15,00),
                    new DateTime(2021, 9, 1, 06,15,00),
                };

            var totalFee = calculator.GetTollFeeForDay(vehicle, passTimes);
            Console.WriteLine(totalFee);

            Console.ReadLine();
        }
    }
}
