using System;
using TollCalculator.Models;
using System.Collections.Generic;


namespace TollCalculator.Interfaces
{
    public interface ITollService
    {
        int CalculateFee(Vehicle vehicle, List<DateTime> dates);
    }
}
