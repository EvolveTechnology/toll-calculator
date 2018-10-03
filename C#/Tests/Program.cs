
using System;
using TollFeeCalculator.Tests;

public class Program
{
    public static void Main(string[] args)
    {
        TestEngine.Instance.InitializeAllTests();
        TestEngine.Instance.ExecuteTests();
        
        Console.ReadLine();
    }
}