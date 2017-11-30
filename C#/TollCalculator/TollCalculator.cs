using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Extensions;

namespace TollFeeCalculator
{
   public class TollCalculator
   {
      private readonly ITollFreeDates _tollFreeDates;
      private readonly ITollFreeVehicles _tollFreeVehicles;
      private readonly IDailyTollCalculator _dailyToll;
      private readonly Money _defaultFee = new Money(0);

      public TollCalculator(ITollFreeVehicles tollFreeVehicles, ITollFreeDates tollFreeDates, IDailyTollCalculator dailyToll)
      {
         _tollFreeVehicles = tollFreeVehicles;
         _tollFreeDates = tollFreeDates;
         _dailyToll = dailyToll;
      }

      /**
       * Calculate the total toll fee for one day
       *
       * @param vehicle - the vehicle
       * @param dates   - date and time of all passes on one day (sorted!)
       * @return - the total toll fee for that day
       */
      public int GetTollFee(IVehicle vehicle, List<DateTime> passageTimes)
      {
         // TODO[Daniel]: Speak with user(s) of API to figure out strategy for nulls
         if (vehicle == null) throw new ArgumentNullException(nameof(vehicle));
         if (passageTimes == null) throw new ArgumentNullException(nameof(passageTimes));

         if (_tollFreeVehicles.IsTollFree(vehicle) || passageTimes.None() || _tollFreeDates.IsTollFree(passageTimes.First())) return _defaultFee.Amount;

         // TODO[Daniel]: Can the list be assumed to be sorted?
         passageTimes.Sort();

         return _dailyToll.GetDailyTotal(passageTimes).Amount;
      }
   }
}