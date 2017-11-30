using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Extensions;

namespace TollFeeCalculator
{
   public class TimeToTollFee : ITimeToTollFee
   {
      private readonly List<TollRate> _tollRates;

      public TimeToTollFee(List<TollRate> tollRates)
      {
         if (tollRates.None()) throw new ArgumentException("List of toll rates cannot be empty", nameof(tollRates));

         if (tollRates.First().Start != new TimeSpan(0, 0, 0))
            throw new ArgumentException("First toll rate must start at midnight", nameof(tollRates));

         _tollRates = tollRates;
      }

      public Money GetTollRate(DateTime passageTime)
      {
         return _tollRates.Last(r => r.Start <= new TimeSpan(passageTime.Hour, passageTime.Minute, passageTime.Second)).Fee;
      }
   }
}