using Nager.Date;
using System;

namespace Toll.Calculator.Domain
{
    public class VehiclePassageEvent
    {
        private readonly DateTime _passageTime;
        private readonly bool _isTollFree;

        public VehiclePassageEvent(
            DateTime passageTime,
            bool isTollFree)
        {
            _passageTime = passageTime;
            _isTollFree = isTollFree;
        }

        public decimal GetTollFee()
        {
            if (_isTollFree || IsTollFreeDate())
                return 0;

            throw new NotImplementedException();
        }

        private bool IsTollFreeDate()
        {
            if (DateSystem.IsPublicHoliday(_passageTime, CountryCode.SE) ||
                DateSystem.IsWeekend(_passageTime, CountryCode.SE))
                return true;

            return false;
        }
    }
}
