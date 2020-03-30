package com.evolve.tollcalculator.vehicle;

import com.evolve.tollcalculator.vehicle.Vehicle;

public class Car implements Vehicle {
  @Override
  public boolean isTollFree() {
    return false;
  }
}
