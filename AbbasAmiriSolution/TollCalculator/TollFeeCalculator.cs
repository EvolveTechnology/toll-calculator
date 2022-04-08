using TollCalculator.Enums;
using TollCalculator.Helpers;
using TollCalculator.Models;
using TollCalculator.Policies;

namespace TollCalculator;

public class TollFeeCalculator
{
    private readonly WeekendPolicy _weekendPolicy = new();
    private readonly VehicleTypePolicy _vehicleTypePolicy = new();
    private readonly DayTimePolicy _dayTimePolicy;
    private readonly HolidayPolicy _holidayPolicy;

    public TollFeeCalculator(IReadOnlyList<DayTimeFee> dayTimeFeeTable, IReadOnlyList<DateOnly> holidayTable)
    {
        _dayTimePolicy = new DayTimePolicy(dayTimeFeeTable);
        _holidayPolicy = new HolidayPolicy(holidayTable);
    }

    public decimal? CalculateTollFee(DateTime[] dateTimes, VehicleType vehicleType)
    {
        if (dateTimes.Length == 0)
            return 0;

        if (dateTimes.DistinctBy(DateOnly.FromDateTime).Count() != 1)
            throw new ArgumentException("Dates are not in a same day", nameof(dateTimes));


        if (_holidayPolicy.IsHoliday(DateOnly.FromDateTime(dateTimes[0])) ||
            _weekendPolicy.IsWeekend(DateOnly.FromDateTime(dateTimes[0])) ||
            _vehicleTypePolicy.IsFeeFree(vehicleType))
        {
            return 0;
        }

        var dateTimesGroupsWithOneHourInterval = dateTimes
            .GroupBy(g => Math.Truncate(TimeOnly.FromDateTime(g).ToTimeSpan() / TimeSpan.FromHours(1)))
            .Select(grp => grp.ToArray()).ToArray();

        var totalFee = dateTimesGroupsWithOneHourInterval
            .Aggregate<DateTime[]?, decimal>(0,
                (current, group) => current + CalculateGroupTollFee(group));

        return totalFee > Constants.MaximumFeeForOneDay ? Constants.MaximumFeeForOneDay : totalFee;
    }
    
    private decimal CalculateGroupTollFee(DateTime[]? dateTimes)
    {
        ArgumentNullException.ThrowIfNull(dateTimes);

        return dateTimes.Select(dateTime => _dayTimePolicy.Calculate(TimeOnly.FromDateTime(dateTime)))
            .Aggregate<decimal?, decimal>(0, (current, fee) => current < fee ? fee.Value : current);
    }
}