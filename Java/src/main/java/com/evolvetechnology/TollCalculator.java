package com.evolvetechnology;

import com.evolvetechnology.timecost_calculation.TimeCostCalculator;
import com.evolvetechnology.vehicles.Vehicle;

import java.time.*;
import java.time.temporal.ChronoUnit;
import java.util.Arrays;
import java.util.Iterator;
import java.util.function.Predicate;

public class TollCalculator extends TollCalculationStrategy {

  public TollCalculator(Predicate<LocalDateTime> freeDateMatcher,
                        TimeCostCalculator timeCostCalculator) {
    super(freeDateMatcher, timeCostCalculator);
  }

  public int calculate(Vehicle vehicle, LocalDateTime... dates) {
    if (dates.length == 0) return 0;

    int totalFee = 0;
    int intervalCost = 0;
    LocalDateTime intervalStart = dates[0];
    Iterator<LocalDateTime> iterator = Arrays.asList(dates).iterator();

    while (iterator.hasNext()) {
      LocalDateTime time = iterator.next();
      int currentTollFee = calculateToll(vehicle, time);
      if (hourHasPassed(intervalStart, time)) {
        intervalStart = time;
        totalFee += intervalCost;
        intervalCost = currentTollFee;
      } else {
        intervalCost = getMaxTollFee(intervalCost, currentTollFee);
      }
      if (!iterator.hasNext()) totalFee += intervalCost;
    }
    return trimTo60(totalFee);
  }

  private int trimTo60(int fee) {
    return Math.min(fee, 60);
  }

  private int getMaxTollFee(int intervalCost, int currentTollFee) {
    return Math.max(intervalCost, currentTollFee);
  }

  private boolean hourHasPassed(LocalDateTime start, LocalDateTime time) {
    return start.until(time, ChronoUnit.MINUTES) > 60;
  }

}

