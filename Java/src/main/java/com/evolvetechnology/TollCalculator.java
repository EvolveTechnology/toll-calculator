package com.evolvetechnology;

import com.evolvetechnology.vehicles.Vehicle;

import java.time.LocalDateTime;

@FunctionalInterface
public interface TollCalculator<V extends Vehicle> {
  int calculate(V vehicle, LocalDateTime... dates);
}
