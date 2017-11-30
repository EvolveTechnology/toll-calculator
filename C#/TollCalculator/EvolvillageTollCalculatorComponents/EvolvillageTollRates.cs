using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
   public class EvolvillageTollRates
   {
      public static List<TollRate> GetRates()
      {
         Func<int, int, int, TollRate> makeRate = (hours, minutes, cost) => new TollRate(new TimeSpan(hours, minutes, 0), new Money(cost));

         return new List<TollRate>
         {
            makeRate(0, 0, 0),
            makeRate(6, 0, 8),
            makeRate(6, 30, 13),
            makeRate(7, 00, 18),
            makeRate(8, 00, 13),
            makeRate(8, 30, 8),
            makeRate(15, 0, 13),
            makeRate(15, 30, 18),
            makeRate(17, 0, 13),
            makeRate(18, 0, 8),
            makeRate(18, 30, 0)
         };
      }
   }
}