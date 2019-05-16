using System;
using System.Collections.Generic;
using TollCalculator.Models;

namespace TollCalculator.Repository
{
    public interface ITollRepository
    {
        Vehicle GetCar();
        List<Vehicle> GetAllTollFreeVehicles();

        List<DateTime> GetDates();
        List<DateTime> GetDatesWithinHour();
        List<DateTime> GetMaximumTollHours();
        List<DateTime> GetOverChargeDayToll();
        List<DateTime> GetMinimumTollHours();
        List<DateTime> GetWeekends();
        List<DateTime> GetHolidays();
        List<FeePeriod> GetTollFeePeriods();

    }
}