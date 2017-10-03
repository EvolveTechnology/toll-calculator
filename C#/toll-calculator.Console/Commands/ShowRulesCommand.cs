using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using TollCalculator.Contracts.Rules;

namespace TollCalculator.Console.Commands
{
    internal class ShowRulesCommand : ICommand
    {
        public string Name { get; set; }

        public ITollFeeRules Rules { get; set; }

        public void Execute(Context context)
        {
            try
            {
                if (Rules == null)
                {
                    throw new InvalidOperationException("No rules to display.");
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Name);
                sb.AppendLine();
                sb.Append($"{"Domestic Country Codes".PadRight(32)}: ");
                sb.AppendLine($"{string.Join(", ", Rules.DomesticIso3166CountryCodes)}");
                sb.Append($"{"Non-Domestic Vehicles Toll Free?".PadRight(32)}: ");
                sb.AppendLine($"{Rules.AreNonDomesticVehiclesTollFree}");
                sb.AppendLine($"{"Toll Free Vehicle Types".PadRight(32)}:");
                sb.AppendLine($"{string.Join(", ", Rules.TollFreeVehicleTypes)}");
                sb.Append($"{"Maximum Daily Toll Fee".PadRight(32)}: ");
                sb.AppendLine($"{Rules?.DailyMaximumTollFee.ToString() ?? "<none>"}");
                sb.Append($"{"Single Charge Rule In Place".PadRight(32)}: ");
                var singleChargeMessage = Rules.NumberOfMinutesForSingleChargeRule.HasValue
                    ? "True (" + Rules.NumberOfMinutesForSingleChargeRule.Value.ToString() + " min)"
                    : "False";
                sb.AppendLine($"{singleChargeMessage}");
                var tollFees = Rules.TollFeeOrderedByStartTime.ToList();
                for (int i = 0; i < tollFees.Count(); i++)
                {
                    var tollFee = tollFees[i];
                    if (tollFee.Value == 0) continue;
                    var nextTollTime = i < tollFees.Count() - 1 ? tollFees[i + 1].Key : new TimeSpan(24, 00, 00);
                    sb.Append($"\t{tollFee.Key:hh\\:mm} - {(nextTollTime - TimeSpan.FromMinutes(1)):hh\\:mm}");
                    sb.AppendLine($"\t{tollFee.Value} {context.RulesRepo.Iso4217CurrencySymbol}");
                }

                System.Console.WriteLine(sb.ToString());
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
