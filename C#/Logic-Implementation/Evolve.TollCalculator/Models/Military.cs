namespace Evolve.TollCalculator.Models
{
    public class Military : Vehicle
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
