using System;

namespace DL.Models
{
    public class DriveBy
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public DateTime PassedAt { get; set; }
        public int PassageCost { get; set; }
    }
}
