using System;

namespace TollFeeCalculator.Extensions
{
   public static class DateTimeExtensions
   {
      public static bool IsJuly(this DateTime @this)
      {
         return @this.Month == 7;
      }

      public static bool IsWeekend(this DateTime @this)
      {
         return @this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday;
      }
   }
}