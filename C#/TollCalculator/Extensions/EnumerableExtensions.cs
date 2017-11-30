using System.Collections.Generic;
using System.Linq;

namespace TollFeeCalculator.Extensions
{
   public static class EnumerableExtensions
   {
      public static bool None<T>(this IEnumerable<T> @this)
      {
         return !@this.Any();
      }
   }
}