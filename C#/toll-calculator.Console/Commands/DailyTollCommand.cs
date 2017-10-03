using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TollCalculator.Console.Commands
{
    internal class DailyTollCommand : ICommand
    {
        public DailyTollCommand()
        {
            Date = DateTime.Now.Date;
            Times = new List<TimeSpan>();
        }

        public string Name => "Calculate Daily Toll";

        private DateTime Date { get; set; }
        private List<TimeSpan> Times { get; set; }

        public void Execute(Context context)
        {
            try
            {
                System.Console.WriteLine(Name);
                System.Console.WriteLine();
                System.Console.Write($"Date [{Date:d}]? ");
                Date = CommandHelper.GetDateInput() ?? Date;
                Times = CommandHelper.GetTimesInput(Times);

                var result = context.Calculator
                    .CalculateDailyTollFee(Date, context.Vehicle, Times.Select(t => Date + t));

                ShowResult(context, result);
            }
            catch (Exception exception)
            {
                context.Logger.LogError(exception, "Error in {0}: ", Name);
                System.Console.WriteLine(exception.Message);
                System.Console.WriteLine();
            }

            CommandHelper.WaitForKeyPress();
        }

        private void ShowResult(Context context, DailyTollFee result)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"Result for {Name}");
            sb.AppendLine();
            sb.AppendLine($"{"Date".PadRight(18)}: {result.Date:d}");
            sb.AppendLine($"{"Vehicle".PadRight(18)}: {context.Vehicle.VehicleType} " +
                $"({context.Vehicle.RegistrationIdentifier}, {context.Vehicle.Iso3166Alpha2CountryCode})");
            sb.AppendLine($"{"Number of passages".PadRight(18)}: {result.NumberOfPassages}");
            sb.AppendLine($"{"Total amount".PadRight(18)}: " +
                $"{result.TotalAmount} {context.RulesRepo.Iso4217CurrencySymbol}");
            sb.AppendLine($"{"Taxable amount".PadRight(18)}: " +
                $"{result.TaxableAmount} {context.RulesRepo.Iso4217CurrencySymbol}");

            System.Console.WriteLine(sb.ToString());
        }
    }
}
