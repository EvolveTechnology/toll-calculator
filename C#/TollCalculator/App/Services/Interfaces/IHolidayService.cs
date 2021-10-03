using System;
using System.Collections.Generic;
using System.Text;

namespace TollCalculatorApp.Services.Interfaces
{
    public interface IHolidayService
    {
        bool IsHoliday(DateTime date);
    }
}
