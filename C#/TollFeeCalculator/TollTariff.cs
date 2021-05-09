using System;
using Nager.Date;

public class TollTariff 
{
    public Boolean IsTollFreeDate(DateTime date)
    {
        var se = CountryCode.SE;
        return DateSystem.IsPublicHoliday(date, se) || DateSystem.IsWeekend(date, se);        
    }
}