namespace TollFeeCalculator
{
   public class EvolvillageTollCalculatorFactory
   {
      public static TollCalculator GetEvolvillageTollFeeCalculator()
      {
         return new TollCalculator(new EvolvillageTollFreeVehicles(), new EvolvillageTollFreeDates(),
                                   new EvolvillageDailyTollCalculator(new TimeToTollFee(EvolvillageTollRates.GetRates())));
      }
   }
}