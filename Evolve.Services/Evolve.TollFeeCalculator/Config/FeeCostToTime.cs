using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.TollFeeCalculator.Config
{
    /// <summary>
    ///Avgift vid tidpunkten
    /// </summary>
    public class FeeCostToTime
    {
        /// <summary>
        /// time  06:00–06:29
        /// </summary>
        public int ZoneTime6a { get; set; }
        /// <summary>
        /// time 06:30–06:59
        /// </summary>
        public int ZoneTime6b { get; set; }
        /// <summary>
        /// time 07:00–07:59
        /// </summary>
        public int ZoneTime7 { get; set; }
        /// <summary>
        /// time  08:00–08:29
        /// </summary>
        public int ZoneTime8a { get; set; }
        /// <summary>
        /// time  08:30–14:59
        /// </summary>
        public int ZoneTime8b { get; set; }
        /// <summary>
        /// time  15:00–15:29
        /// </summary>
        public int ZoneTime15a { get; set; }
        /// <summary>
        /// time  15:30–16:59
        /// </summary>
        public int ZoneTime15b { get; set; }
        /// <summary>
        /// time  17:00–17:59
        /// </summary>
        public int ZoneTime17 { get; set; }
        /// <summary>
        /// time 18:00–18:29
        /// </summary>
        public int ZoneTime18 { get; set; }
        /// <summary>
        /// time 18:30–05:59
        /// </summary>
        public int ZoneTimefree { get; set; }
    }
}
