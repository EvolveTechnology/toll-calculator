package com.evolvetechnology;

import com.evolvetechnology.timecost_calculation.TimeCostCalculator;
import com.evolvetechnology.vehicles.Vehicle;

import java.time.LocalDateTime;
import java.util.function.Predicate;

public abstract class TollCalculationStrategy implements TollCalculation {

  private final Predicate<LocalDateTime> freeDateMatcher;
  private final TimeCostCalculator timeCostCalculator;

  public TollCalculationStrategy(Predicate<LocalDateTime> freeDateMatcher, TimeCostCalculator timeCostCalculator) {
    this.freeDateMatcher = freeDateMatcher;
    this.timeCostCalculator = timeCostCalculator;
  }

  protected int calculateToll(Vehicle vehicle, LocalDateTime date) {
    if (vehicleShouldNotBeCharged(vehicle, date)) return 0;
    return timeCostCalculator.getCostFor(date.toLocalTime());
  }

  private boolean vehicleShouldNotBeCharged(Vehicle vehicle, LocalDateTime date) {
    return vehicle.isTollFree() || freeDateMatcher.test(date);
  }
}
