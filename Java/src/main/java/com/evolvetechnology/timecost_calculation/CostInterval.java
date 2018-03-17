package com.evolvetechnology.timecost_calculation;

import java.time.LocalTime;

public class CostInterval {

  private final LocalTime start;
  private final LocalTime end;
  private final int cost;

  public CostInterval(LocalTime start, LocalTime end, int cost) {
    this.start = start;
    this.end = end;
    this.cost = cost;
  }

  public LocalTime getStart() {
    return start;
  }

  public LocalTime getEnd() {
    return end;
  }

  public int getCost() {
    return cost;
  }

}
