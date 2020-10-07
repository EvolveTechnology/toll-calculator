using System.Data.Linq.Mapping;

namespace DL.Tables
{
    [Table]
    public class Vehicles
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string RegistrationNumber { get; set; }
        [Column]
        public string VehicleType { get; set; }
    }
}
