using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
   public interface IDailyTollCalculator
   {
      Money GetDailyTotal(List<DateTime> passageTimes);
   }
}