using System;
using System.Collections.Generic;
using TollFeeCalculator;

namespace TollCalculatorProgram
{
   public class Program
   {
      public static void Main()
      {
         var sum = EvolvillageTollCalculatorFactory.GetEvolvillageTollFeeCalculator().GetTollFee(
            new Car(),
            new List<DateTime>
            {
               new DateTime(2013, 1, 8, 6, 30, 0),
               new DateTime(2013, 1, 8, 7, 50, 0),
               new DateTime(2013, 1, 8, 9, 50, 0),
               new DateTime(2013, 1, 8, 16, 50, 0),
               new DateTime(2013, 1, 8, 17, 50, 0),
            });
         Console.WriteLine(sum);
         Console.Read();
      }
   }
}