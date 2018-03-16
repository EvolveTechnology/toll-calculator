package com.evolvetechnology.timecost_calculation;

import java.time.LocalTime;

@FunctionalInterface
public interface TimeCostCalculator {
  int getCostFor(LocalTime localTime);
}
