using Microsoft.Extensions.Logging.Abstractions;
using System;
using TollCalculator.Contracts.Rules;
using TollCalculator.Contracts.Vehicles;

namespace TollCalculator.Console
{
    internal class Context
    {
        public Context(
            TollFeeCalculator calculator,
            ITollFeeRulesRepository rules,
            Microsoft.Extensions.Logging.ILogger logger)
        {
            Calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
            RulesRepo = rules?? throw new ArgumentNullException(nameof(rules));
            Logger = logger ?? NullLogger.Instance;
        }

        public Vehicle Vehicle { get; set; }
        public Microsoft.Extensions.Logging.ILogger Logger { get; }
        public TollFeeCalculator Calculator { get; }
        public ITollFeeRulesRepository RulesRepo { get; }
    }
}
