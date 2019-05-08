using System;
using System.Collections.Generic;
using TollFeeCalculator.TollFeeAmount;
using TollFeeCalculator.TollFeeTime;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //Lokal testing
            var tollCalculator = new TollCalculator(new TollFeeAmountService(new TollFeeTimeService()));
            var result = tollCalculator.GetTollFee(new Car(), new List<DateTime> { 
             new DateTime(2013, 3, 6, 6, 40, 00),
             new DateTime(2013, 3, 6, 7, 30, 00),
             new DateTime(2013, 3, 6, 17, 00, 00),
             new DateTime(2013, 3, 6, 18, 40, 00)});
            Console.WriteLine(result);
            Console.ReadKey();
        }
        
    }
}
