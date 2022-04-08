using TollCalculator.Models;

namespace TollCalculator.Policies;

public class DayTimePolicy
{
    private readonly IReadOnlyList<DayTimeFee> _dayTimeFeeTable;
    
    public DayTimePolicy(IReadOnlyList<DayTimeFee> dayTimeFeeTable)
    {
        _dayTimeFeeTable = dayTimeFeeTable;
    }

    public decimal? Calculate(TimeOnly time)
    {
        var timeFee = _dayTimeFeeTable.FirstOrDefault(c => time.IsBetween(c.Start, c.End));
        return timeFee?.Fee;
    }
}