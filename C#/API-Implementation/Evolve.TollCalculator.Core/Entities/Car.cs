using Evolve.TollCalculator.Core.Common;

namespace Evolve.TollCalculator.Core.Entities
{
    public class Car : Vehicle
    {
        public override bool VehicleTollFree { get { return false; } }
    }
}