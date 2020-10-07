using System;
using BL;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            bool userWantsToContinue = true;

            while (userWantsToContinue)
            {
                Console.WriteLine("Enter registration number to simulate passing a Toll. Example: CAR001");
                string userInput = Console.ReadLine();
                var instance = new TollCalculator();
                string message = instance.ReturnTotalTollFeeForToday(userInput);

                Console.WriteLine(message);
                Console.WriteLine("If you want to simulate another passing, press the: 'Y' key, else press: Any key");
                ConsoleKeyInfo newPassingOrNot = Console.ReadKey();

                if (newPassingOrNot.Key == ConsoleKey.Y)
                {
                    //Continue
                    userWantsToContinue = true;
                    Console.WriteLine();
                }
                else
                {
                    //End
                    userWantsToContinue = false;
                }
            }
        }
    }
}
