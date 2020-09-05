using System;

namespace TollFeeCalculator
{
    public class Car : Vehicle
    {
        public bool IsTollFree()
        {
            return false;
        }
    }
}