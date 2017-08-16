using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateLibrary
{
    public static class Extensions
    {
        public static bool IsHoliday(this DateTime @this)
        {
            return HolidayProvider.IsHoliday(@this);
        }
    }
}
