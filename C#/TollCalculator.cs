using System;
using TollFeeCalculator;
using Toll_calculator.Adapters;

public class TollCalculator
{
    /**
     * Calculate the total toll fee for one day
     *
     * @param vehicle - the vehicle
     * @param dates   - date and time of all passes on one day
     * @return - the total toll fee for that day
     */

    private readonly int ChargeSpanInMinutes = 60;
    private readonly decimal MaximumFeePerDay = 60;

    public decimal GetTollFee(Vehicle vehicle, DateTime[] passages)
    {
        var intervalFee = 0;
        var intervalStart = passages[0];
        var singlePassage = passages.Length == 1;
        var totalFee = 0m;

        foreach (DateTime passage in passages)
        {
            var passageFee = GetTollFee(passage, vehicle);

            if (singlePassage) return passageFee;

            var timeSpan = passage - intervalStart;
            var diffInMillies = (int)timeSpan.TotalMilliseconds;
            var spanInMinutes = diffInMillies / 1000 / 60;

            if (spanInMinutes <= ChargeSpanInMinutes)
            {
                if (totalFee > 0 || passageFee < intervalFee) continue;

                intervalFee = passageFee;

                if (totalFee == 0) totalFee = passageFee;
                
                else if (passageFee > intervalFee)
                {
                    totalFee -= intervalFee;
                    totalFee += passageFee;
                }
            }
            else
            {
                totalFee += passageFee;
                intervalStart = passage;
                intervalFee = passageFee;
            }
        }
        if (totalFee > MaximumFeePerDay) totalFee = MaximumFeePerDay;
        return totalFee;
    }


    public int GetTollFee(DateTime passage, Vehicle vehicle)
    {
        if (IsWeekendOrHoliday(passage) || IsTollFreeVehicle(vehicle)) return 0;
        
        var hour = passage.Hour;
        var minute = passage.Minute;

        if (IsInTimeSpan(hour, minute, 6, 0, 29)) return 8; // 6:00-6:29
        if (IsInTimeSpan(hour,minute, 6, 30, 59)) return 13; // 6:30-6:59
        if (IsInTimeSpan(hour, minute, 7, 0, 59)) return 18; // 7:00-7:59
        if (IsInTimeSpan(hour, minute, 8, 0, 29)) return 13; // 8:00.8:29
        if (hour >= 8 && hour <= 14 && minute >= 30 && minute <= 59) return 8; // 8:30-14:59
        if (IsInTimeSpan(hour, minute, 15, 0, 29)) return 13; // 15:00-15:29
        if (hour == 15 && minute >= 30 || hour == 16 && minute <= 59) return 18; // 15:30-16:59
        if (IsInTimeSpan(hour, minute, 17, 0, 59)) return 13; // 17:00-17:59
        if (IsInTimeSpan(hour, minute, 18, 0, 29)) return 8; // 18:00-18:29
        return 0;
    }

    private bool IsWeekendOrHoliday(DateTime passage)
    {
        if (passage.DayOfWeek == DayOfWeek.Saturday || passage.DayOfWeek == DayOfWeek.Sunday) return true;

        var dateInfo = DateInfoAdapter.GetDateInfo(passage);
        return dateInfo.helgdag != null;
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        var vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorbike.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    private bool IsInTimeSpan(int passageHour, int passageMinute, int equalHour, int minMin, int maxMin)
    {
        return (passageHour == equalHour && passageMinute >= minMin && passageMinute <= maxMin);
    }

    private enum TollFreeVehicles
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}