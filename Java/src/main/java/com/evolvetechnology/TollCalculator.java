package com.evolvetechnology;

import com.evolvetechnology.vehicle.Vehicle;

import java.time.*;
import java.time.temporal.ChronoUnit;
import java.util.function.Predicate;

public class TollCalculator {

  final private Predicate<LocalDateTime> isDateFreeOfCharge;
  final private TimeCostCalculator timeCostCalculator;

  public TollCalculator(Predicate<LocalDateTime> isDateFreeOfCharge,
                        TimeCostCalculator timeCostCalculator) {
    this.timeCostCalculator = timeCostCalculator;
    this.isDateFreeOfCharge = isDateFreeOfCharge;
  }

  public int getTollFee(Vehicle vehicle, LocalDateTime... dates) {
    LocalDateTime intervalStart = dates[0];

    int totalFee = 0;
    for (LocalDateTime date : dates) {
      int nextFee = getTollFee(vehicle, date);
      int tempFee = getTollFee(vehicle, intervalStart);

      long minutes = intervalStart.until(date, ChronoUnit.MINUTES);

      if (minutes <= 60) {
        if (totalFee > 0) continue;
        if (nextFee >= tempFee) tempFee = nextFee;
        totalFee += tempFee;
      } else {
        totalFee += nextFee;
      }
    }
    if (totalFee > 60) totalFee = 60;
    return totalFee;
  }

  public int getTollFee(Vehicle vehicle, LocalDateTime date) {
    if (shouldNotBeCharged(vehicle, date)) return 0;
    return timeCostCalculator.getCostFor(date.toLocalTime());
  }

  private boolean shouldNotBeCharged(Vehicle vehicle, LocalDateTime date) {
    return isDateFreeOfCharge.test(date) || vehicle.isTollFree();
  }

}

