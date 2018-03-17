package com.evolvetechnology.timecostcalculators;

import org.junit.Test;

import java.time.LocalTime;
import java.util.function.Function;

import static org.junit.Assert.*;

public class TimeCostCalculatorTest {

  @Test
  public void getCostFor() {
    Function<LocalTime, Integer> timeCostCalculator = IntervalTimeCostFunction.create()
            .withCostInterval(LocalTime.of(8, 0), LocalTime.of(9, 0), 1)
            .withCostInterval(LocalTime.of(9, 0), LocalTime.of(9, 29), 2)
            .withCostInterval(LocalTime.of(10, 0), LocalTime.of(11, 0), 3);

    int eightThirty = timeCostCalculator.apply(LocalTime.of(8, 30));
    assertEquals(1, eightThirty);

    int nineAClock = timeCostCalculator.apply(LocalTime.of(9, 0));
    assertEquals(2, nineAClock);

    int nineFourtyFive = timeCostCalculator.apply(LocalTime.of(9, 45));
    assertEquals(0, nineFourtyFive);

    int tenAClock = timeCostCalculator.apply(LocalTime.of(10, 0));
    assertEquals(3, tenAClock);

    int elevenAClock = timeCostCalculator.apply(LocalTime.of(11, 0));
    assertEquals(0, elevenAClock);

  }

  @Test(expected = IllegalArgumentException.class)
  public void givenAnIllegalTimeInterval() {
    IntervalTimeCostFunction.create()
            .withCostInterval(LocalTime.of(8, 0), LocalTime.of(7, 0), 0);
  }

  @Test(expected = IllegalArgumentException.class)
  public void givenTwoOverlappingTimeIntervals() {
    IntervalTimeCostFunction.create()
            .withCostInterval(LocalTime.of(7, 0), LocalTime.of(8, 0), 0)
            .withCostInterval(LocalTime.of(7, 30), LocalTime.of(8, 30), 0);
  }

}