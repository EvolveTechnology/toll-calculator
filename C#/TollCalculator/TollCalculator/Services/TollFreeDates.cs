using System;
using System.Collections.Generic;
using System.Linq;
using Nager.Date;
using TollFeeCalculator.Interfaces;

namespace TollFeeCalculator.Services
{
    public class TollFreeDates : ITollFreeDates
    {
        private readonly CountryCode _countryCode;
        private readonly List<DateTime> _additionalHoldiays = new List<DateTime>();
        private readonly List<DateTime> _holidaysToRemove = new List<DateTime>();

        public TollFreeDates(CountryCode countryCode = CountryCode.SE)
        {
            this._countryCode = countryCode;
            _additionalHoldiays = new List<DateTime>() { new DateTime(2019, 07, 19) };
        }

        public TollFreeDates(List<DateTime> additionalHoldiays, List<DateTime> holidaysToRemove, CountryCode countryCode = CountryCode.SE)
        {
            this._countryCode = countryCode;
            this._additionalHoldiays = additionalHoldiays;
            this._holidaysToRemove = holidaysToRemove;
        }

        public bool IsTollFreeDate(DateTime date)
        {
            // Used an open source library called Nager.Date to look for holidays. Any additional holidays
            // should be  added to _additionalHoldiays  
            // any dates you need to remove from the library result should be added to _holidaysToRemove
            
            return (DateSystem.IsPublicHoliday(date, _countryCode) ||
                    DateSystem.IsWeekend(date, _countryCode) ||
                    _additionalHoldiays.Select(x => x.Date).Contains(date.Date)) &&
                   !_holidaysToRemove.Select(x => x.Date).Contains(date.Date);
        }
    }
}
