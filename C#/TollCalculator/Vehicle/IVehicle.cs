namespace TollFeeCalculator
{
   public enum VehicleType
   {
      Motorbike,
      Tractor,
      Emergency,
      Diplomat,
      Foreign,
      Military,
      Car
   }

   public interface IVehicle
   {
      VehicleType GetVehicleType();
   }
}