package com.evolvetechnology;

import com.evolvetechnology.vehicle.Vehicle;

import java.time.*;
import java.time.temporal.ChronoUnit;

public class TollCalculator {

  final private UnchargedTimeResolver unchargedTimeResolver;
  final private TimeCostCalculator timeCostCalculator;

  public TollCalculator(UnchargedTimeResolver unchargedTimeResolver,
                        TimeCostCalculator timeCostCalculator) {
    this.timeCostCalculator = timeCostCalculator;
    this.unchargedTimeResolver = unchargedTimeResolver;
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

  public int getTollFee(Vehicle vehicle, final LocalDateTime date) {
    if (shouldBeUncharged(vehicle, date)) return 0;
    return timeCostCalculator.getCostFor(date.toLocalTime());
  }

  private boolean shouldBeUncharged(Vehicle vehicle, LocalDateTime date) {
    return unchargedTimeResolver.isTollFreeDate(date) || vehicle.isTollFree();
  }

}

