using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TollFeeCalculator
{
    /**
     * Defines a toll fee period, with start time, non-inclusive end time, and associated fee.
    */
    class TollFeePeriod
    {
        private TimeSpan _StartTime;
        private TimeSpan _NonInclusiveEndTime;
        private int _TollFee;
        /**
         * The fee to associate with the Period
        */
        public int TollFee
        {
            get
            {
                return _TollFee;
            }
        }
        

        /**
         * Default constructor for a toll fee period
         * @param StartTime - the start time of the fee period
         * @param NonInclusiveEndTime - the end time of the fee period, non-inclusive
         * @param TollFee - the toll fee to associate with this period
        */
        public TollFeePeriod(TimeSpan StartTime, TimeSpan NonInclusiveEndTime, int TollFee)
        {
            _StartTime = StartTime;
            _NonInclusiveEndTime = NonInclusiveEndTime;
            _TollFee = TollFee;
        }



        /**
         * Checks if a given DateTime falls within the fee period defined. Is Concerned ONLY with time.
         * @param Date - The Date to check for bounding
         * @return - if the provided date falls within this fee period.
        */
        public bool IsWithinPeriod(DateTime Date)
        {
            return _StartTime >= Date.TimeOfDay && _NonInclusiveEndTime < Date.TimeOfDay;
        }

    }
}
