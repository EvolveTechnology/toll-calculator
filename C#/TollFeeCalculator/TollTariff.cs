using System;
using System.Linq;
using Nager.Date;

public class TollTariff 
{
    public Boolean IsTollFreeDate(DateTime date)
    {
        var se = CountryCode.SE;
        if (date.Month == 7) return true;
        if (date.Month == 4 && date.Day == 30) return true;
        if (date.Month == 6 && date.Day == 05) return true;
        if (DateSystem.IsWeekend(date, se)) return true;
        if (DateSystem.IsPublicHoliday(date, se)) return true;

        var holidays = DateSystem.GetPublicHoliday(date.Year, se);

        if (DateSystem.IsPublicHoliday(date.AddDays(1), se))
        {
            var holiday = holidays.First(h => h.Date == date.AddDays(1));
            if (holiday.Name == "Ascension Day") return true;
            if (holiday.Name == "All Saints' Day") return true;
            if (holiday.Name == "Good Friday") return true;
        }
        return false;
    }
}