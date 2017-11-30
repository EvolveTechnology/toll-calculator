using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
   public class DateComparer : IEqualityComparer<DateTime>
   {
      public bool Equals(DateTime x, DateTime y)
      {
         return x.Date.Equals(y.Date);
      }

      public int GetHashCode(DateTime obj)
      {
         return obj.GetHashCode();
      }
   }
}