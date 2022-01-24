package com.evolve.tollCalculator.model;

import static com.evolve.tollCalculator.util.Constants.VEHICLE_TYPE_CAR;

public class Car implements Vehicle {
  @Override
  public String getType() {
    return VEHICLE_TYPE_CAR;
  }
}
