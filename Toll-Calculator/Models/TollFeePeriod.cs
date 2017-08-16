using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_Calculator.Models
{
    public class TollFeePeriod
    {
        public TollFeePeriod(int fee, TimeSpan startTime, TimeSpan endTime)
        {
            TollFee = fee;
            Start = startTime;
            End = endTime;
        }

        public int TollFee { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
