using System;

namespace TollFeeCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var calculator = new TollCalculator();
            var vehicle = new Car();
            var timestamps = new [] { DateTime.Now, DateTime.Now.AddMinutes(30), DateTime.Now.AddHours(-3) };
            calculator.GetTotalTollFee(vehicle, timestamps);
        }
    }
}
