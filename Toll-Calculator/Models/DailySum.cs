using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_Calculator.Models
{
    public class DailySum
    {
        public DailySum(int value)
        {
            Sum = value > 60 ? 60 : value;
        }
        public int Sum { get; set; }
    }
}
