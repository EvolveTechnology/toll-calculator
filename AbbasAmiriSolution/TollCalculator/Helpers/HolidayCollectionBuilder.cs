using System.Text.Json;

namespace TollCalculator.Helpers;

public class HolidayCollectionBuilder
{
    private readonly List<DateOnly> _holidays = new();

    /// <summary>
    /// Add an item into list.
    /// </summary>
    /// <param name="date">The item.</param>
    public void Add(DateOnly date)
    {
        ArgumentNullException.ThrowIfNull(date);

        if (_holidays.All(c => c != date))
        {
            _holidays.Add(date);
        }
    }

    /// <summary>
    /// Returns a read-only list.
    /// </summary>
    /// <returns>IReadOnlyList</returns>
    public IReadOnlyList<DateOnly> ToReadOnlyList()
    {
        return _holidays.ToList().AsReadOnly();
    }

    /// <summary>
    /// Returns list.
    /// </summary>
    /// <returns>IList</returns>
    public IList<DateOnly> ToList()
    {
        return _holidays.ToList();
    }

    /// <summary>
    /// Returns array.
    /// </summary>
    /// <returns>DateOnly[]</returns>
    public DateOnly[] ToArray()
    {
        return _holidays.ToArray();
    }

    /// <summary>
    /// Load data from a Json file.
    /// </summary>
    /// <param name="path">The Json filename.</param>
    /// <exception cref="ArgumentException">Throws when file cannot be opened or parsed.</exception>
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