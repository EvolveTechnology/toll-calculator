using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TollCalculator.Console.Commands
{
    internal class PerformanceTestCommand : ICommand
    {
        public PerformanceTestCommand()
        {
            Iterations = 1000;
        }

        private int Iterations { get; set; }

        public string Name => "Performance Test";

        public void Execute(Context context)
        {
            try
            {
                System.Console.WriteLine(Name);
                System.Console.WriteLine();
                System.Console.Write($"Iterations [{Iterations}]? ");
                Iterations = CommandHelper.GetIntegerInput() ?? Iterations;
                RunTest(context);
            }
            catch (Exception exception)
            {
                context.Logger.LogError(exception, "Error in {0}: ", Name);
                System.Console.WriteLine(exception.Message);
                System.Console.WriteLine();
            }

            CommandHelper.WaitForKeyPress();
        }

        private void RunTest(Context context)
        {
            IEnumerable<DateTime> passages = Enumerable.Range(1, 30).SelectMany(GetPassagesForDay);

            TollFeeResult result;
            for (int i = 0; i < 101; i++)
            {
                result = context.Calculator.CalculateTollFee(context.Vehicle, passages);
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < Iterations; i++)
            {
                result = context.Calculator.CalculateTollFee(context.Vehicle, passages);
            }
            watch.Stop();

            System.Console.WriteLine($"Sequential execution of {Iterations} iterations: {watch.ElapsedMilliseconds} ms.");

            watch.Restart();
            Parallel.For(0, Iterations, i =>
            {
                result = context.Calculator.CalculateTollFee(context.Vehicle, passages);
            });
            watch.Stop();

            System.Console.WriteLine($"Parallel execution of {Iterations} iterations: {watch.ElapsedMilliseconds} ms.");
        }

        private static IEnumerable<DateTime> GetPassagesForDay(int day)
        {
            return new DateTime[]
            {
                new DateTime(2017, 09, day, 09, 05, 00),
                new DateTime(2017, 09, day, 09, 15, 00),
                new DateTime(2017, 09, day, 16, 05, 00),
                new DateTime(2017, 09, day, 16, 10, 00),
                new DateTime(2017, 09, day, 16, 45, 00),
            };
        }
    }
}
