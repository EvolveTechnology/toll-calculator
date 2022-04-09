using System.Text.Json;

namespace TollCalculator.Helpers;

public class Constants
{
    public static decimal MaximumFeeForOneDay;
    public static decimal MinimumFeeDependingOnTheTimeOfDay;
    public static decimal MaximumFeeDependingOnTheTimeOfDay;

    static Constants()
    {
        SetDefaults();
    }

    public static void ReadJsonFile(string path)
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

        var jsonData = JsonSerializer.Deserialize<JsonModel>(jsonString, options);
        if (jsonData == null)
        {
            throw new ArgumentException("There is something wrong in the json file.");
        }

        MaximumFeeForOneDay = jsonData.MaximumFeeForOneDay;
        MinimumFeeDependingOnTheTimeOfDay = jsonData.MinimumFeeDependingOnTheTimeOfDay;
        MaximumFeeDependingOnTheTimeOfDay = jsonData.MaximumFeeDependingOnTheTimeOfDay;
    }

    public static void SetDefaults()
    {
        MaximumFeeForOneDay = 60;
        MinimumFeeDependingOnTheTimeOfDay = 8;
        MaximumFeeDependingOnTheTimeOfDay = 18;
    }

    private class JsonModel
    {
        public decimal MaximumFeeForOneDay { get; set; }
        public decimal MinimumFeeDependingOnTheTimeOfDay { get; set; }
        public decimal MaximumFeeDependingOnTheTimeOfDay { get; set; }
    }
}