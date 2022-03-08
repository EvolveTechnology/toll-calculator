namespace Evolve.TollCalculator.Models
{
    public class Car : Vehicle
    {
        public override bool VehicleTollFree
        {
            get
            {
                return false;
            }
        }
    }
}