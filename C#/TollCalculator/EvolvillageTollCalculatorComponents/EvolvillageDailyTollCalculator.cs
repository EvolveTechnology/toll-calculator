using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{
   public class EvolvillageDailyTollCalculator : IDailyTollCalculator
   {
      private const int MaxDailyTotal = 60;
      private readonly ITimeToTollFee _tollRate;

      public EvolvillageDailyTollCalculator(ITimeToTollFee tollRate)
      {
         _tollRate = tollRate;
      }

      public Money GetDailyTotal(List<DateTime> passageTimes)
      {
         var dailyTotal = new TimeFilter().OncePerHour(passageTimes).Select(_tollRate.GetTollRate).Sum(m => m.Amount);
         return new Money(Math.Min(dailyTotal, MaxDailyTotal));
      }
   }
}