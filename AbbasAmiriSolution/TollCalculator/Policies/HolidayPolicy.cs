namespace TollCalculator.Policies;

public class HolidayPolicy
{
    private readonly IReadOnlyList<DateOnly> _holidayTable;

    public HolidayPolicy(IReadOnlyList<DateOnly> holidayTable)
    {
        _holidayTable = holidayTable;
    }

    public bool IsHoliday(DateOnly date)
    {
        return _holidayTable.Any(c => c == date);
    }
}