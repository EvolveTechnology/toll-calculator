using System;
using System.Collections.Generic;
using System.Text;

namespace TollCalculator.Models
{
    public class FeePeriod
    {
        public Fee Fee { get; set; }
        public Dictionary<TimeSpan, TimeSpan> Period { get; set; }
        public int Price { get; set; }
    }

    public enum Fee
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
