namespace Evolve.TollCalculator.Models
{
    public class Tractor : Vehicle
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
