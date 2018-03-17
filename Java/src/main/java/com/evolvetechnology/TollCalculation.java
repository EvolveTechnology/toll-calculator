package com.evolvetechnology;

import com.evolvetechnology.vehicles.Vehicle;

import java.time.LocalDateTime;

@FunctionalInterface
public interface TollCalculation {
  int calculate(Vehicle vehicle, LocalDateTime... dates);
}
