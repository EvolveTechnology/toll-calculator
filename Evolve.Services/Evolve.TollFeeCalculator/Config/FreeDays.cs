using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Config
{
    /// <summary>
    /// Config calss for free days in the year-month
    /// </summary>
    public class FreeDays
    {
        /// <summary>
        /// 
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// Free month of days for JANUARY
        /// </summary>
        public List<int> JANUARY { get; set; }
        /// <summary>
        /// Free month of days for MARCH
        /// </summary>
        public List<int> MARCH { get; set; }
        /// <summary>
        /// Free month of days for APRIL
        /// </summary>
        public List<int> APRIL { get; set; }

        /// <summary>
        /// Free month of days for MAY
        /// </summary>
        public List<int> MAY { get; set; }

        /// <summary>
        /// Free month of days for JUNE
        /// </summary>
        public List<int> JUNE { get; set; }

        /// <summary>
        /// Free month of days for JULY
        /// </summary>
        public List<int> JULY { get; set; }

        /// <summary>
        ///Free month of days for NOVEMBER
        /// </summary>
        public List<int> NOVEMBER { get; set; }


        /// <summary>
        /// Free month of days for DECEMBER
        /// </summary>
        public List<int> DECEMBER { get; set; }


        

    }
}

