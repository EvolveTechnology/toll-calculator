package com.evolvetechnology;

import org.junit.Test;

import java.time.LocalTime;

import static org.junit.Assert.*;

public class TimeCostManagerTest {

  @Test
  public void getCostFor() throws Exception {
    final TimeCostManager timeCostManager = new TimeCostManager();
    timeCostManager.addIntervalCost(LocalTime.of(8, 0), LocalTime.of(9, 0), 1);
    timeCostManager.addIntervalCost(LocalTime.of(9, 0), LocalTime.of(9, 29), 2);
    timeCostManager.addIntervalCost(LocalTime.of(10, 0), LocalTime.of(11, 0), 3);

    int eightThirty = timeCostManager.getCostFor(LocalTime.of(8, 30));
    assertEquals(1, eightThirty);

    int nineAClock = timeCostManager.getCostFor(LocalTime.of(9, 0));
    assertEquals(2, nineAClock);

    int nineFourtyFive = timeCostManager.getCostFor(LocalTime.of(9, 45));
    assertEquals(0, nineFourtyFive);

    int tenAClock = timeCostManager.getCostFor(LocalTime.of(10, 0));
    assertEquals(3, tenAClock);

    int elevenAClock = timeCostManager.getCostFor(LocalTime.of(11, 0));
    assertEquals(0, elevenAClock);

  }

  @Test(expected = IllegalArgumentException.class)
  public void givenAnIllegalTimeInterval() {
    TimeCostManager timeCostManager = new TimeCostManager();
    timeCostManager.addIntervalCost(LocalTime.of(8, 0), LocalTime.of(7, 0), 0);
  }

  @Test(expected = IllegalArgumentException.class)
  public void givenTwoOverlappingTimeIntervals() {
    TimeCostManager timeCostManager = new TimeCostManager();
    timeCostManager.addIntervalCost(LocalTime.of(7, 0), LocalTime.of(8, 0), 0);
    timeCostManager.addIntervalCost(LocalTime.of(7, 30), LocalTime.of(8, 30), 0);
  }

}