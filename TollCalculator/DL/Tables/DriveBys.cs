using System;
using System.Data.Linq.Mapping;

namespace DL.Tables
{
    [Table]
    public class DriveBys
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int VehicleId { get; set; }
        [Column]
        public DateTime PassedAt { get; set; }
        [Column]
        public int PassageCost { get; set; }
    }
}
