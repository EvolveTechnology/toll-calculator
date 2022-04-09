using System.Text.Json;
using System.Text.Json.Serialization;
using TollCalculator.Models;

namespace TollCalculator.Helpers;

public class DayTimeFeeCollectionBuilder
{
    private readonly List<DayTimeFee> _dayTimeFees = new List<DayTimeFee>();

    public void Add(DayTimeFee dayTimeFee)
    {
        ArgumentNullException.ThrowIfNull(dayTimeFee);

        if (_dayTimeFees.Any(c => HasTimeConflict(dayTimeFee, c)))
        {
            throw new ArgumentException(null, nameof(dayTimeFee));
        }

        _dayTimeFees.Add(dayTimeFee);
    }

    public void Clear() => _dayTimeFees.Clear();

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

    public void ReadJsonFile(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentException("The file path is not specified.");
        }

        if (!File.Exists(path))
        {
            throw new ArgumentException("The does not exist.");
        }

        var jsonString = File.ReadAllText(path);

        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new TimeOnlyConverter());
        var jsonData = JsonSerializer.Deserialize<IList<JsonModel>>(jsonString, options);
        if (jsonData == null)
        {
            throw new ArgumentException("There is something wrong in the json file.");
        }

        foreach (var jsonModel in jsonData)
        {
            _dayTimeFees.Add(jsonModel.RushHour
                ? DayTimeFee.CreateRushHour(jsonModel.Start, jsonModel.End)
                : DayTimeFee.Create(jsonModel.Start, jsonModel.End, jsonModel.Fee));
        }
    }

    private class JsonModel
    {
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public decimal Fee { get; set; }
        public bool RushHour { get; set; }
    }
}