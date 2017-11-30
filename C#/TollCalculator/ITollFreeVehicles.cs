namespace TollFeeCalculator
{
   public interface ITollFreeVehicles
   {
      bool IsTollFree(IVehicle vehicle);
   }
}