using System;
using System.Data.Linq.Mapping;

namespace DL.Tables
{
    [Table]
    public class CostPerDays
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int VehicleId { get; set; }
        [Column]
        public int CostThisDay { get; set; }
        [Column]
        public DateTime Date { get; set; }
    }
}
