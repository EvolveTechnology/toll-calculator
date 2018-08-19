using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TollFeeCalculator;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test of the TollCalculator...");

            DateTime[] testValues =
            {
                new DateTime(2018, 08, 17, 08,01,00,000),
                new DateTime(2018, 08, 17, 06,00,00,000),
                new DateTime(2018, 08, 17, 07,00,01,000),
                new DateTime(2018, 08, 17, 08,02,00,000),
                new DateTime(2018, 08, 17, 09,00,00,000),
                new DateTime(2018, 08, 17, 09,03,00,000),
                new DateTime(2018, 08, 17, 10,02,00,000),
                new DateTime(2018, 08, 17, 10,02,00,000),
                new DateTime(2018, 08, 17, 12,02,00,000),
                new DateTime(2018, 08, 17, 13,03,00,000),
                new DateTime(2018, 08, 17, 15,02,00,000),
            };

            TollCalculator calculator = new TollCalculator();

            Console.WriteLine($"Total price: {calculator.GetTollFee(new Car(), testValues)}");
            Console.ReadLine();

        }
    }
}
