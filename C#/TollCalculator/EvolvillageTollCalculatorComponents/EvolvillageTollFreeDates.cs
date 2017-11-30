using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Extensions;

namespace TollFeeCalculator
{
   public class EvolvillageTollFreeDates : ITollFreeDates
   {
      private readonly SwedishHolidays _swedishHolidays = new SwedishHolidays();
      private readonly List<DateTime> _tollFreeDates = new List<DateTime>
      {
         new DateTime(2013, 4, 30),
         new DateTime(2013, 5, 8),
         new DateTime(2013, 5, 9),
         new DateTime(2013, 6, 5),
         new DateTime(2013, 6, 21),
         new DateTime(2013, 11, 1),
      };

      public bool IsTollFree(DateTime date)
      {
         return date.IsWeekend() || date.IsJuly() || _swedishHolidays.IsHoliday(date) || _tollFreeDates.Contains(date, new DateComparer());
      }
   }
}