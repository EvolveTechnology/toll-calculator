package com.evolvetechnology;

import com.evolvetechnology.timecost_calculation.TimeCostCalculator;
import com.evolvetechnology.vehicles.Vehicle;

import java.time.*;
import java.time.temporal.ChronoUnit;
import java.util.Arrays;
import java.util.Iterator;
import java.util.function.Predicate;

public class TollCalculator {

  final private Predicate<LocalDateTime> freeDateMatcher;
  final private TimeCostCalculator timeCostCalculator;

  public TollCalculator(Predicate<LocalDateTime> freeDateMatcher,
                        TimeCostCalculator timeCostCalculator) {
    this.timeCostCalculator = timeCostCalculator;
    this.freeDateMatcher = freeDateMatcher;
  }

  public int getTollFee(Vehicle vehicle, LocalDateTime... dates) {
    if (dates.length == 0) return 0;

    int totalFee = 0;
    int intervalCost = 0;

    LocalDateTime intervalStart = dates[0];
    Iterator<LocalDateTime> iterator = Arrays.asList(dates).iterator();

    while (iterator.hasNext()) {
      LocalDateTime time = iterator.next();
      int currentTollFee = getTollFee(vehicle, time);
      if (hourHasPassed(intervalStart, time)) {
        intervalStart = time;
        totalFee += intervalCost;
        intervalCost = currentTollFee;
      } else {
        intervalCost = Math.max(intervalCost, currentTollFee);
      }
      if (!iterator.hasNext()) totalFee += intervalCost;
    }

    return Math.min(totalFee, 60);
  }

  private boolean hourHasPassed(LocalDateTime start, LocalDateTime time) {
    return start.until(time, ChronoUnit.MINUTES) > 60;
  }

  public int getTollFee(Vehicle vehicle, LocalDateTime date) {
    if (vehicleShouldNotBeCharged(vehicle, date)) return 0;
    return timeCostCalculator.getCostFor(date.toLocalTime());
  }

  private boolean vehicleShouldNotBeCharged(Vehicle vehicle, LocalDateTime date) {
    return vehicle.isTollFree() || freeDateMatcher.test(date);
  }

}

