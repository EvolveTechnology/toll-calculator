using System;

namespace TollFeeCalculator
{
   public interface ITollFreeDates
   {
      bool IsTollFree(DateTime date);
   }
}