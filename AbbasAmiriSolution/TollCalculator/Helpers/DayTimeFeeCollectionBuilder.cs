using TollCalculator.Models;

namespace TollCalculator.Helpers;

public class DayTimeFeeCollectionBuilder
{
    private readonly IList<DayTimeFee> _dayTimeFees = new List<DayTimeFee>();

    public void Add(DayTimeFee dayTimeFee)
    {
        ArgumentNullException.ThrowIfNull(dayTimeFee);

        if (_dayTimeFees.Any(c => HasTimeConflict(dayTimeFee, c)))
        {
            throw new ArgumentException(null, nameof(dayTimeFee));
        }

        _dayTimeFees.Add(dayTimeFee);
    }

    private static bool HasTimeConflict(DayTimeFee first, DayTimeFee second)
    {
        return first.Start.IsBetween(second.Start, second.End) ||
               first.End.IsBetween(second.Start, second.End) ||
               second.Start.IsBetween(first.Start, first.End) ||
               second.End.IsBetween(first.Start, first.End);
    }

    public IReadOnlyList<DayTimeFee> ToReadOnlyList() => _dayTimeFees.ToList().AsReadOnly();

    public IList<DayTimeFee> ToList() => _dayTimeFees.ToList(); 

    public IReadOnlyList<DayTimeFee> ToArray() => _dayTimeFees.ToArray(); 
    
}