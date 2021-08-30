using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Toll_calc.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool FallsBetween(this DateTime date, TimeSpan start, TimeSpan end)
        {
            var time = new TimeSpan(date.Hour, date.Minute, date.Second);
            return start <= time && time < end;
        }
    }
}