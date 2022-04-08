namespace TollCalculator.Helpers;

public class HolidayCollectionBuilder
{
    private readonly IList<DateOnly> _holidays = new List<DateOnly>();

    public void Add(DateOnly date)
    {
        ArgumentNullException.ThrowIfNull(date);

        if (_holidays.All(c => c != date))
        {
            _holidays.Add(date);
        }
    }

    public IReadOnlyList<DateOnly> ToReadOnlyList() => _holidays.ToList().AsReadOnly();

    public IList<DateOnly> ToList() => _holidays.ToList(); 

    public IReadOnlyList<DateOnly> ToArray() => _holidays.ToArray(); 
}