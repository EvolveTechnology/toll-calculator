using System;
using System.Collections.Generic;
using System.Text;
using TollCalculator.Enums;

namespace TollCalculator.Models
{
    public class FeePeriod
    {
        public Fee Fee { get; set; }
        public Dictionary<TimeSpan, TimeSpan> Period { get; set; }
        public int Price { get; set; }
    }
}
