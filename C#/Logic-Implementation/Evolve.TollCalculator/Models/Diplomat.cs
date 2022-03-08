namespace Evolve.TollCalculator.Models
{
    public class Diplomat : Vehicle
    {
        public override bool VehicleTollFree
        {
            get
            {
                return true;
            }
        }
    }
}
