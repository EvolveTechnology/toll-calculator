using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TollFeeCalculator;
using TollFeeCalculator.Converters;
using TollFeeCalculator.Enums;
using TollFeeCalculator.Models;

public class TollCalculator
{

    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */
    private IEnumerable<FreeDatesModel> freeDates;
    private IEnumerable<VehicleTypeEnum> freeVehicles;
    private IEnumerable<TimeDateFeeModel> feeTimes;

     public TollCalculator()
    {
        var configuration = GetConfiguration();
        freeDates = configuration.FreeDates;
        freeVehicles = configuration.FreeVehicles;
        feeTimes = configuration.FeeTimes;
    }

    private TollFeeConfigurationModel GetConfiguration()
    {
        var settings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter> { new FeeTimeConverter() },
            Formatting = Formatting.Indented
        };
        using (StreamReader reader = new StreamReader("Configuration.json"))
        {
            string json = reader.ReadToEnd();
            TollFeeConfigurationModel configuration = JsonConvert.DeserializeObject<TollFeeConfigurationModel>(json, settings);
            return configuration;
        }
    }

    public int GetTollFee(IVehicle vehicle, DateTime[] dates)
    {
        if (dates == null || !dates.Any())
        {
            return 0;
        }
        var totalFee = 0;
        var days = dates.GroupBy(x => x.DayOfYear);
        foreach (var day in days)
        {
            DateTime intervalStart = day.ElementAt(0);
            int dayFee = GetTollFee(intervalStart, vehicle);
            foreach (var date in day.Skip(1))
            {

                int currentFee = GetTollFee(date, vehicle);
                var diff = date - intervalStart;
                if (diff.TotalMinutes > 60)
                {
                    intervalStart = date;
                    dayFee += currentFee;
                }

            }

            if (dayFee > 60) dayFee = 60;
            totalFee += dayFee;
        }
        return totalFee;
    }

    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        return freeVehicles.Any(x => x == vehicle.GetVehicleType());
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;

        int hour = date.Hour;
        int minute = date.Minute;
        int fee = 0;

        var feeTime = feeTimes.FirstOrDefault(x => x.IsInTimeSpan(date));
        fee = feeTime != null ? feeTime.Fee : 0;

        return fee;
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        return freeDates.Any(x => x.Year == year && x.Dates.Any(y => y.Month == month && (y.Days == null || y.Days.Contains(day))));

    }
}