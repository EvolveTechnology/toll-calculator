package com.toll.calculator;
import static com.toll.calculator.Constants.CAR;

public class Car implements Vehicle {
  @Override
  public String getType() {
    return CAR;
  }
}
