package com.tollcalculator.pojo;

import com.tollcalculator.constants.TollCalculatorConstants;

public class Motorbike implements Vehicle {
  @Override
  public String getType() {
    return TollCalculatorConstants.MOTORBIKE;
  }
}
