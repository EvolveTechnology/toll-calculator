using System;
using System.Collections.Generic;
using System.Linq;

namespace TollCalculator.Console.Commands
{
    internal static class CommandHelper
    {
        public static int? GetIntegerInput()
        {
            return int.TryParse(System.Console.ReadLine(), out int value)
                ? (int?)value
                : null;
        }

        public static DateTime? GetDateInput()
        {
            return DateTime.TryParse(System.Console.ReadLine(), out DateTime value)
                ? (DateTime?)value.Date
                : null;
        }

        public static T? GetEnumInput<T>(T ifEmpty) where T : struct
        {
            var input = System.Console.ReadLine();
            return string.IsNullOrWhiteSpace(input)
                ? ifEmpty
                : Enum.TryParse(typeof(T), input, out object value)
                    ? (T?)value
                    : null;
        }

        public static string GetStringInput()
        {
            var input = System.Console.ReadLine();
            return !string.IsNullOrWhiteSpace(input)
                ? input
                : null;
        }

        public static List<TimeSpan> GetTimesInput(IEnumerable<TimeSpan> previous)
        {
            System.Console.WriteLine("Enter times (hh:mm). End by pressing Enter.");
            System.Console.WriteLine((previous ?? Enumerable.Empty<TimeSpan>()).Any()
                ? $"Previous: [{string.Join(", ", previous)}]"
                : "[]");
            List<TimeSpan> result = new List<TimeSpan>();
            TimeSpan? time;
            string line;
            do
            {
                line = System.Console.ReadLine();
                time = TimeSpan.TryParse(line, out TimeSpan timeSpan)
                    ? (TimeSpan?)timeSpan
                    : null;

                if (time.HasValue && time.Value < TimeSpan.FromHours(24) && time.Value >= TimeSpan.Zero)
                {
                    result.Add(time.Value);
                }
                else if (line.Length > 0)
                {
                    System.Console.WriteLine($"Invalid time: {line}");
                }

            } while (time.HasValue || line.Length > 0);
            return result.Any() ? result : previous.ToList();
        }

        public static void WaitForKeyPress()
        {
            while (System.Console.KeyAvailable)
            {
                System.Console.ReadKey(intercept: true);
            }
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey(intercept: true);
        }
    }
}
