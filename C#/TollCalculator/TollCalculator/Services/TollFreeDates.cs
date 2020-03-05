using System;
using System.Collections.Generic;
using System.Text;
using Nager.Date;
using TollFeeCalculator.Interfaces;

namespace TollFeeCalculator.Services
{
    public class TollFreeDates : ITollFreeDates
    {
        private readonly CountryCode _countryCode;
        private readonly List<DateTime> _additionalHoldiays = new List<DateTime>();

        public TollFreeDates(CountryCode countryCode)
        {
            this._countryCode = countryCode;
        }

        public bool IsTollFreeDate(DateTime date)
        {
            // Used an open source library called Nager.Date to look for holidays. Any additional holidays
            // should be  added to _additionalHoldiays  

            return DateSystem.IsPublicHoliday(date, _countryCode) ||
                   DateSystem.IsWeekend(date, _countryCode) ||
                   _additionalHoldiays.Contains(date);
        }
    }
}
