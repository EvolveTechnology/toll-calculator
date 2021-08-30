using System;
using Nager.Date;

namespace Toll_calc.Services
{

    public interface IHolidayService
    {
        bool IsHoliday(DateTime date);
    }

    public class HolidayService : IHolidayService
    {
        public bool IsHoliday(DateTime date)
        {
            return DateSystem.IsPublicHoliday(date, CountryCode.SE);
        }
    }
}