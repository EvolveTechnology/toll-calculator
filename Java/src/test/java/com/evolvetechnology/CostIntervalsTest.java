package com.evolvetechnology;

import org.junit.Test;

import java.time.LocalTime;

import static org.junit.Assert.*;

public class CostIntervalsTest {

  @Test
  public void getCostFor() throws Exception {
    final CostIntervals costIntervals = new CostIntervals();
    costIntervals.add(LocalTime.of(8, 0), LocalTime.of(9, 0), 1);
    costIntervals.add(LocalTime.of(9, 0), LocalTime.of(9, 29), 2);
    costIntervals.add(LocalTime.of(10, 0), LocalTime.of(11, 0), 3);

    int eightThirty = costIntervals.getCostFor(LocalTime.of(8, 30));
    assertEquals(1, eightThirty);

    int nineAClock = costIntervals.getCostFor(LocalTime.of(9, 0));
    assertEquals(2, nineAClock);

    int nineFourtyFive = costIntervals.getCostFor(LocalTime.of(9, 45));
    assertEquals(0, nineFourtyFive);

    int tenAClock = costIntervals.getCostFor(LocalTime.of(10, 0));
    assertEquals(3, tenAClock);

    int elevenAClock = costIntervals.getCostFor(LocalTime.of(11, 0));
    assertEquals(0, elevenAClock);

  }

  @Test(expected = IllegalArgumentException.class)
  public void givenAnIllegalTimeInterval() {
    final CostIntervals costIntervals = new CostIntervals();
    costIntervals.add(LocalTime.of(8, 0), LocalTime.of(7, 0), 0);
  }

  @Test(expected = IllegalArgumentException.class)
  public void givenTwoOverlappingTimeIntervals() {
    final CostIntervals costIntervals = new CostIntervals();
    costIntervals.add(LocalTime.of(7, 0), LocalTime.of(8, 0), 0);
    costIntervals.add(LocalTime.of(7, 30), LocalTime.of(8, 30), 0);
  }

}