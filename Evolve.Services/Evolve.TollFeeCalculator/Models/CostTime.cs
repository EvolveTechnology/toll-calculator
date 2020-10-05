using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Models
{
    /// <summary>
    /// klass tid (timme, minut) för  kostnad
    /// </summary>
    public class CostTime
    {
        /// <summary>
        /// timme
        /// </summary>
        public int Hour { get; }
        /// <summary>
        /// minuter
        /// </summary>
        public int Minute { get; }
        /// <summary>
        /// konstruct
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        public CostTime(int hour, int minute) => (Hour, Minute) = (hour, minute);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        public void Deconstruct(out int hour, out int minute) => (hour, minute) = (Hour, Minute);
    }
}
