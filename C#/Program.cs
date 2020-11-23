using System;
using System.Collections.Generic;

namespace TollFeeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Toll Fee! This is a test");
            List<Vehicle> vehicleList = GetVehicles();

            foreach (Vehicle vehicle in vehicleList)
            {
                for (int loops = 3; loops < 25; loops++)
                {
                    DateTime[] dtList = CreateDates(19, loops);
                    TollCalculator tCalc = new TollCalculator();
                    int myFee = tCalc.GetTollFee(vehicle, dtList);
                    Console.WriteLine("Fee was " + myFee.ToString() + " based on " + "Offset: 19 and times " + loops);
                }
            }
        }

        static List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            for (int i = 0; i < 1; i++)
            {
                Vehicle vehicle = new Vehicle("OrdinaryCar");
                vehicleList.Add(vehicle);
            }

            return vehicleList;
        }

        static DateTime[] CreateDates(int offsetMinutes, int ticks)
        {
            DateTime[] dtArray = new DateTime[ticks];
            for (int i = 0; i < ticks; i++)
            {
                DateTime fixTime = new DateTime(2020, 12, 23, 5, 30, 0);
                dtArray[i] = fixTime.AddMinutes(i * offsetMinutes);
            }
            return dtArray;
        }
    }
}
