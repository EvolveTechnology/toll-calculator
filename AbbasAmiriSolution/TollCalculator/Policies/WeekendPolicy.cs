namespace TollCalculator.Policies;

public class WeekendPolicy
{
    public bool IsWeekend(DateOnly date)
    {
        return date.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday;
    }
}
