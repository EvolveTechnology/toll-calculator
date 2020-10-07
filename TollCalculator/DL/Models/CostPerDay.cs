using System;

namespace DL.Models
{
    public class CostPerDay
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int CostThisDay { get; set; }
        public DateTime Date { get; set; }
    }
}
