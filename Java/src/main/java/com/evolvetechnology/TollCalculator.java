package com.evolvetechnology;

import com.evolvetechnology.vehicles.Vehicle;

import java.time.LocalDateTime;

@FunctionalInterface
public interface TollCalculator {
  int calculate(Vehicle vehicle, LocalDateTime... dates);
}
