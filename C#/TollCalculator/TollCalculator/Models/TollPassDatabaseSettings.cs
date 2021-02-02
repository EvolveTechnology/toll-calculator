namespace TollFeeCalculator.Models
{
    public interface ITollPassDatabaseSettings
    {
        string TollPassCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
    public class TollPassDatabaseSettings : ITollPassDatabaseSettings
    {
        public string TollPassCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
