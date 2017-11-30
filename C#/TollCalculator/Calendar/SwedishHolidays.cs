using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
   public enum Month
   {
      January = 1,
      February = 2,
      March = 3,
      April = 4,
      May = 5,
      June = 6,
      July = 7,
      August = 8,
      September = 9,
      October = 10,
      November = 11,
      December = 12
   }

   public class SwedishHolidays 
   {
      private readonly Dictionary<int, HashSet<DateTime>> _yearlyHolidays = new Dictionary<int, HashSet<DateTime>>();

      public bool IsHoliday(DateTime date)
      {
         var year = date.Year;
         if (!_yearlyHolidays.ContainsKey(year))
         {
            var holidays = GetFixedHolidays(year);
            holidays.UnionWith(GetVariableHolidays(year));
            _yearlyHolidays.Add(year, holidays);
         }

         return _yearlyHolidays[year].Contains(date);
      }

      private HashSet<DateTime> GetFixedHolidays(int year)
      {
         Func<Month, int, DateTime> makeDate = (month, day) => new DateTime(year, (int)month, day);
         return new HashSet<DateTime>
         {
            // Nyårsdagen
            makeDate(Month.January, 1),

            // Trettondagen 
            makeDate(Month.January, 6),

            // Första maj
            makeDate(Month.May, 1),

            // Nationaldagen
            makeDate(Month.June, 6),

            // Julafton
            makeDate(Month.December, 24),

            // Juldagen
            makeDate(Month.December, 25),

            // Annandag jul
            makeDate(Month.December, 26)
         };
      }

      private HashSet<DateTime> GetVariableHolidays(int year)
      {
         // TODO[Daniel]: Add Easter etc...
         return new HashSet<DateTime>();
      }
   }
}