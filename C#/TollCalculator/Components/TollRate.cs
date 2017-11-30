using System;

namespace TollFeeCalculator
{
   public class TollRate
   {
      public TollRate(TimeSpan start, Money fee)
      {
         Start = start;
         Fee = fee;
      }

      public TimeSpan Start { get; }
      public Money Fee { get; }
   }
}