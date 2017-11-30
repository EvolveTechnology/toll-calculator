using System;
using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator
{
   public class TimeFilter
   {
      public IEnumerable<DateTime> OncePerHour(IEnumerable<DateTime> passageTimes)
      {
         DateTime? prev = null;
         Func<DateTime, bool> oncePerHour = date =>
         {
            if (prev.HasValue && (date - prev.Value).Hours < 1) return false;

            prev = date;
            return true;
         };

         return passageTimes.Where(oncePerHour);
      }
   }
}