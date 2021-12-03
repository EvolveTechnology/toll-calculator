package com.evolve.tollcalculator;
import static com.evolve.tollcalculator.Constants.CAR;
public class Car implements Vehicle {
  @Override
  public String getType() {
    return CAR;
  }
}
