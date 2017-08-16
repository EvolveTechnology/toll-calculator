using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toll_Calculator.Models
{
    public class EligibleDate
    {
        public EligibleDate(DateTime date, int fee)
        {
            DateTime = date;
            Fee = fee;
        }

        public DateTime DateTime { get; set; }
        public int Fee { get; set; }
    }
}
