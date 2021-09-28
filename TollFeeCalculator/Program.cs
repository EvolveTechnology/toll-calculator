using System;

namespace TollFeeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // build holidays 
            Toll.Builder builder = new Toll.Build();
            builder.BuildHollidays();
           
            //Initialize vehicle type
            Toll.Motorbike motorbike1 = new Toll.Motorbike();
            Toll.Tesla tesla = new Toll.Tesla();

            Toll.TollCalculator calculatorTesla = new Toll.TollCalculator(motorbike1,12,00,13,00,DateTime.Now, builder.GetResult());
            Console.WriteLine($"Toll Fee: { calculatorTesla.GetTollFee()}  - Vehicle is FeeFree: {calculatorTesla.IsFeeFree(DateTime.Now)}");


            Toll.TollCalculator calculatorMotorBike = new Toll.TollCalculator(tesla, 12, 00, 13, 00, DateTime.Now, builder.GetResult());
            Console.WriteLine($"Toll Fee: { calculatorMotorBike.GetTollFee()} - Vehicle is eeFree: {calculatorMotorBike.IsFeeFree(DateTime.Now)}");

            Console.ReadLine(); 
        }
    }
}
