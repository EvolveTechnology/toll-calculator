using Evolve.TollCalculator.Core.Common;

namespace Evolve.TollCalculator.Core.Entities
{
    public class Tractor : Vehicle
    {
        public override bool VehicleTollFree { get { return true; } }
    }
}
