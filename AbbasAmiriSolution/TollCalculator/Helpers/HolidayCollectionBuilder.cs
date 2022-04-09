using System.Text.Json;

namespace TollCalculator.Helpers;

public class HolidayCollectionBuilder
{
    private readonly List<DateOnly> _holidays = new List<DateOnly>();

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
        options.Converters.Add(new DateOnlyConverter());
        var jsonData = JsonSerializer.Deserialize<IList<DateOnly>>(jsonString, options);
        if (jsonData == null)
        {
            throw new ArgumentException("There is something wrong in the json file.");
        }

        _holidays.AddRange(jsonData.ToArray());
    }
}