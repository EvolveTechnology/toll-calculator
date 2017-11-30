using System;

namespace TollFeeCalculator
{
   public interface ITimeToTollFee
   {
      Money GetTollRate(DateTime passageTime);
   }
}