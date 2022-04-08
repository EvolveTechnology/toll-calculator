using TollCalculator.Helpers;

namespace TollCalculator.Models;

public class DayTimeFee
{
    public TimeOnly Start { get; }
    public TimeOnly End { get; }
    public decimal Fee { get; }

    private DayTimeFee(TimeOnly start, TimeOnly end, decimal fee)
    {
        Start = start;
        End = end;
        Fee = fee;
    }

    public static DayTimeFee Create(TimeOnly start, TimeOnly end, decimal fee)
    {
        ArgumentNullException.ThrowIfNull(start, nameof(start));
        ArgumentNullException.ThrowIfNull(end, nameof(end));
        if (start > end)
        {
            throw new ArgumentException($"The {nameof(start)} is greater than {nameof(end)}.");
        }

        if (fee is < Constants.MinimumFeeDependingOnTheTimeOfDay or > Constants.MaximumFeeDependingOnTheTimeOfDay)
        {
            throw new ArgumentException(
                $"The {nameof(fee)} must be between {Constants.MinimumFeeDependingOnTheTimeOfDay} and {Constants.MaximumFeeDependingOnTheTimeOfDay}.");
        }
        
        return new DayTimeFee(start, end, fee);
    }

    public static DayTimeFee CreateRushHour(TimeOnly start, TimeOnly end)
    {
        return Create(start, end, Constants.MaximumFeeDependingOnTheTimeOfDay);
    }

    public decimal? GetFee(TimeOnly time)
    {
        ArgumentNullException.ThrowIfNull(time, nameof(time));

        if (time >= Start && time <= End)
        {
            return Fee;
        }

        return null;
    }

}