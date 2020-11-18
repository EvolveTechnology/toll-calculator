using System;
using System.Collections;
using System.Collections.Generic;
using TollFeeCalculator;

namespace TollCalculationApp
{
    class Program
    {
        static void Main()
        {
            
            Console.WriteLine("Start basic test of TollCalculation:");

            List<DateTime> passes = new List<DateTime>() {  new DateTime(2020, 04, 03, 06, 45, 0),
                                                            new DateTime(2020, 04, 03, 07, 45, 0),
                                                            new DateTime(2020, 04, 03, 07, 47, 0),
                                                            new DateTime(2020, 04, 03, 07, 48, 0),
                                                            new DateTime(2020, 04, 03, 08, 15, 0),
                                                            //new DateTime(2020, 04, 03, 13, 45, 0),
                                                            //new DateTime(2020, 04, 03, 14, 15, 0),
                                                            new DateTime(2020, 04, 03, 16, 45, 0),
                                                            new DateTime(2020, 04, 03, 17, 15, 0),
                                                            //new DateTime(2020, 04, 03, 17, 16, 0),
                                                            new DateTime(2020, 04, 03, 18, 10, 0),
                                                            //new DateTime(2020, 04, 03, 18, 18, 0),
                                                            //new DateTime(2020, 04, 03, 18, 19, 0),
                                                            new DateTime(2020, 04, 03, 18, 20, 0)
                                                          };
            List<IVehicle> Vehicles = new List<IVehicle>() { new Car("ABC123") };
            //foreach()
            int Toll = TollCalculator.GetTollFee(Vehicles[0], passes.ToArray());

            Console.WriteLine("Toll is calculated to: {0} kr for date(s) {1} {2}.", 
                Toll.ToString(), 
                passes[0].Date.ToShortDateString(),
                passes[0].Date.Day == passes[^1].Date.Day ? "" : passes[^1].Date.ToShortDateString());
        }
    }
}
