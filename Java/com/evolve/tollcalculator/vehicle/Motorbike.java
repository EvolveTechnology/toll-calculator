package com.evolve.tollcalculator.vehicle;

import com.evolve.tollcalculator.vehicle.Vehicle;

public class Motorbike implements Vehicle {
  @Override
  public boolean isTollFree() {
    return true;
  }
}
