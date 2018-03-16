package com.evolvetechnology;

import java.time.LocalTime;

@FunctionalInterface
public interface TimeCostCalculator {
  int getCostFor(LocalTime localTime);
}
