using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    /// Class time (hour, minute) for  cost
    /// </summary>
    public class CostTime
    {       
        public int Hour { get; }
      
        public int Minute { get; }
        
        public CostTime(int hour, int minute) => (Hour, Minute) = (hour, minute);
        
        public void Deconstruct(out int hour, out int minute) => (hour, minute) = (Hour, Minute);
    }
}
