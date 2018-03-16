package com.evolvetechnology;

import org.junit.Test;

import java.time.LocalTime;

import static org.junit.Assert.*;

public class TimeCostCalculatorTest {

  @Test
  public void getCostFor() throws Exception {
    TimeCostCalculator timeCostCalculator = IntervalTimeCostCalculator.create()
            .withCostInterval(LocalTime.of(8, 0), LocalTime.of(9, 0), 1)
            .withCostInterval(LocalTime.of(9, 0), LocalTime.of(9, 29), 2)
            .withCostInterval(LocalTime.of(10, 0), LocalTime.of(11, 0), 3);

    int eightThirty = timeCostCalculator.getCostFor(LocalTime.of(8, 30));
    assertEquals(1, eightThirty);

    int nineAClock = timeCostCalculator.getCostFor(LocalTime.of(9, 0));
    assertEquals(2, nineAClock);

    int nineFourtyFive = timeCostCalculator.getCostFor(LocalTime.of(9, 45));
    assertEquals(0, nineFourtyFive);

    int tenAClock = timeCostCalculator.getCostFor(LocalTime.of(10, 0));
    assertEquals(3, tenAClock);

    int elevenAClock = timeCostCalculator.getCostFor(LocalTime.of(11, 0));
    assertEquals(0, elevenAClock);

  }

  @Test(expected = IllegalArgumentException.class)
  public void givenAnIllegalTimeInterval() {
    IntervalTimeCostCalculator.create()
            .withCostInterval(LocalTime.of(8, 0), LocalTime.of(7, 0), 0);
  }

  @Test(expected = IllegalArgumentException.class)
  public void givenTwoOverlappingTimeIntervals() {
    IntervalTimeCostCalculator.create()
            .withCostInterval(LocalTime.of(7, 0), LocalTime.of(8, 0), 0)
            .withCostInterval(LocalTime.of(7, 30), LocalTime.of(8, 30), 0);
  }

}