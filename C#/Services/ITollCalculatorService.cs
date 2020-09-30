using System;
using System.Collections.Generic;
using System.Text;
using TollCalculator.Data.Entities;

namespace TollCalculator.Data.Services
{
    public interface ITollCalculatorService
    {
        decimal GetTollFee(Vehicle vehicle, DateTime[] dates);
        decimal GetTollFee(DateTime date, Vehicle vehicle);
    }
}
