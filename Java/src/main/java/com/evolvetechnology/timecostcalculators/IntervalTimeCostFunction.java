package com.evolvetechnology.timecostcalculators;

import java.time.LocalTime;
import java.util.Collection;
import java.util.LinkedList;
import java.util.function.Function;

/**
 * This TimeCostCalculator implementation manages linear intervals during a day with associated costs
 * <p>
 * Example:
 * <p>
 * 6:00 - 7:00 = 8
 * 7:00 - 9:30 = 10
 * 9:30 - 14:30 = 21
 */
public class IntervalTimeCostFunction implements Function<LocalTime, Integer> {

  private Collection<CostInterval> costIntervals = new LinkedList<>();

  private IntervalTimeCostFunction() { }

  public static IntervalTimeCostFunction create() {
    return new IntervalTimeCostFunction();
  }

  public IntervalTimeCostFunction withCostIntervals(Collection<CostInterval> costIntervals) {
    costIntervals.forEach(costInterval ->
            addIntervalCost(costInterval.getStart(), costInterval.getEnd(), costInterval.getCost())
    );
    return this;
  }

  public IntervalTimeCostFunction withCostInterval(LocalTime start, LocalTime end, int cost) {
    addIntervalCost(start, end, cost);
    return this;
  }

  @Override
  public Integer apply(LocalTime localTime) {
    for (CostInterval costInterval : costIntervals) {
      if (isInTimeInterval(localTime, costInterval)) {
        return costInterval.getCost();
      }
    }
    return 0;
  }

  private boolean isInTimeInterval(LocalTime localTime, CostInterval costInterval) {
    return localTime.compareTo(costInterval.getStart()) >= 0 &&
            localTime.compareTo(costInterval.getEnd()) < 0;
  }

  private void addIntervalCost(LocalTime start, LocalTime end, int cost) {
    verifyInterval(start, end);
    costIntervals.add(new CostInterval(start, end, cost));
  }

  private void verifyInterval(LocalTime start, LocalTime end) {
    verifyLogicInterval(start, end);
    verifyNoOverlappingIntervals(start, end);
  }

  private void verifyLogicInterval(LocalTime start, LocalTime end) {
    if (start.compareTo(end) > 0) {
      throw new IllegalArgumentException("Start of interval (" + start + ") is later than end (" + end + ")");
    }
  }

  private void verifyNoOverlappingIntervals(LocalTime start, LocalTime end) {
    for (CostInterval costInterval : costIntervals) {
      if (isOverLapping(start, end, costInterval.getStart(), costInterval.getEnd())) {
        throw new IllegalArgumentException("Overlapping interval (" + start + " -- " + end + ") with existing (" +
                costInterval.getStart() + " -- " + costInterval.getEnd() + ")");
      }
    }
  }

  private boolean isOverLapping(LocalTime start1, LocalTime end1, LocalTime start2, LocalTime end2) {
    boolean endIsOverlapping = start1.compareTo(start2) < 0 && end1.compareTo(start2) > 0;
    boolean startIsOverlapping = start1.compareTo(end2) < 0 && end1.compareTo(end2) > 0;
    return endIsOverlapping || startIsOverlapping;
  }

}
