using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TollCalculator.Contracts.Calendar;

namespace TollCalculator.Console.Commands
{
    internal class TollCalendarCommand : ICommand
    {
        public TollCalendarCommand()
        {
            Year = DateTime.Now.Year;
        }

        public string Name => "Swedish Toll Calendar";

        public ITollFeeCalendar TollCalendar { get; set; }

        public int Year { get; set; }

        public void Execute(Context context)
        {
            try
            {
                if (TollCalendar == null)
                {
                    throw new InvalidOperationException("No calendar to display.");
                }

                System.Console.WriteLine(Name);
                System.Console.WriteLine();
                System.Console.Write($"Year [{Year}]? ");
                Year = CommandHelper.GetIntegerInput() ?? Year;
                System.Console.WriteLine();
                System.Console.WriteLine("Toll Free Days (not including weekends and July):");

                foreach (var item in TollCalendar.TollFreeDaysForYear(Year)
                    .Where(d => TollCalendar.Calendar.GetDayOfWeek(d) != DayOfWeek.Saturday &&
                        TollCalendar.Calendar.GetDayOfWeek(d) != DayOfWeek.Sunday &&
                        d.Month != 7))
                {
                    System.Console.WriteLine($"\t{item:D}");
                }
            }
            catch (Exception exception)
            {
                context.Logger.LogError(exception, "Error in {0}: ", Name);
                System.Console.WriteLine(exception.Message);
                System.Console.WriteLine();
            }

            CommandHelper.WaitForKeyPress();
        }
    }
}
