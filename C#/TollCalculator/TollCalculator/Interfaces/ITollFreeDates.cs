using System;
using System.Collections.Generic;
using System.Text;

namespace TollFeeCalculator.Interfaces
{
    public interface ITollFreeDates
    {
        bool IsTollFreeDate(DateTime date);
    }
}
