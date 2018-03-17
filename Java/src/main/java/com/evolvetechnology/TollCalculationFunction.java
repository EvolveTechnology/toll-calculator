package com.evolvetechnology;

import com.evolvetechnology.vehicles.Vehicle;

import java.time.*;
import java.util.function.BiFunction;
import java.util.function.Function;
import java.util.function.Predicate;

public class TollCalculationFunction implements BiFunction<Vehicle, LocalDateTime, Integer> {

  private final Predicate<LocalDateTime> freeDateMatcher;
  private final Function<LocalTime, Integer> timeCostCalculator;

  public TollCalculationFunction(Predicate<LocalDateTime> freeDateMatcher,
                                 Function<LocalTime, Integer> timeCostCalculator) {
    this.freeDateMatcher = freeDateMatcher;
    this.timeCostCalculator = timeCostCalculator;
  }

  @Override
  public Integer apply(Vehicle vehicle, LocalDateTime date) {
    if (vehicleShouldNotBeCharged(vehicle, date)) return 0;
    return timeCostCalculator.apply(date.toLocalTime());
  }

  private boolean vehicleShouldNotBeCharged(Vehicle vehicle, LocalDateTime date) {
    return vehicle.isTollFree() || freeDateMatcher.test(date);
  }

}

