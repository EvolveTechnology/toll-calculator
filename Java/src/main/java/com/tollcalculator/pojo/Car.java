package com.tollcalculator.pojo;

import com.tollcalculator.constants.TollCalculatorConstants;

public class Car implements Vehicle {
  @Override
  public String getType() {
    return TollCalculatorConstants.CAR;
  }
}
