using System;
using System.Globalization;
using TollFeeCalculator;
using ConsoleApp1;
using System.Linq;
using System.Collections.Generic;

public class TollCalculator
{    
    /// <summary>
    /// Gets the toll fee for a given vehicle and a number of passage-dates
    /// </summary>
    /// <param name="vehicle">The vehicle that the calculation is done for.</param>
    /// <param name="dateArray">An array of DateTimes of the passages</param>
    /// <returns>The fee for the given vehicle and passages.</returns>
    public double GetTollFee(Vehicle vehicle, DateTime[] dateArray)
    {
        List<DateTime> dateList = dateArray.ToList();
        dateList.Sort();

        DateTime intervalStart = dateList[0];
        double totalFee = 0;
        double maxDaylyFee = DummyDatabase.GetMaximumCostPerDay();
        double gracePeriod = DummyDatabase.GetGracePeriodMinutes();

        while (dateList.Any())
        {
            // Get all datetimes that are in the grace-period (if graceperiod is 0 or only 1 item in list only the current date will be found). 
            List<DateTime> datesInWindow = dateList.Where(d => d >= dateList[0] && d <= dateList[0].AddMinutes(gracePeriod)).ToList();

            double maximumFoundFee = 0;

            foreach (DateTime d in datesInWindow)
            {
                double foundFee = getTollFee(d, vehicle);

                maximumFoundFee = foundFee > maximumFoundFee ? foundFee : maximumFoundFee;
            }

            totalFee += maximumFoundFee;

            foreach (DateTime dateToRemove in datesInWindow)
            {
                dateList.Remove(dateToRemove);
            }            
        }

        if(totalFee > maxDaylyFee)
        {
            totalFee = maxDaylyFee;
        }

        return totalFee;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null)
        {
            throw new ArgumentNullException("vehicle", "IsTollFreeVehicle does not accept null as param vehicle");
        }
        if(vehicle.vehicleType == null)
        {
            throw new ArgumentNullException("vehicle.vehicleType", "IsTollFreeVehicle does not accept null as param vehicle.vehicleType");
        }

        return vehicle.vehicleType.VehicleTypeIsFreeToll();
    }

    private double getTollFee(DateTime date, Vehicle vehicle)
    {
        if (isTollFreeDate(date) || IsTollFreeVehicle(vehicle))
        {
            return 0;
        }

        return DummyDatabase.GetPriceOfPassageOnTime(date.Hour, date.Minute, date.Second);
    }

    private Boolean isTollFreeDate(DateTime date)
    {
        if (date == null)
        {
            throw new ArgumentNullException("date", "IsTollFreeDate does not accept null as date-parameter.");
        }

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
        {
            return true;
        }

        return DummyDatabase.DateIsTollFreeDate(date.Year, date.Month, date.Day);
    }
}