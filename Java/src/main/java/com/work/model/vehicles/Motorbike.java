package com.work.model.vehicles;

public class Motorbike implements Vehicle {
  @Override
  public String getType() {
    return "Motorbike";
  }

  @Override
  public boolean isTollFree() {
    return true;
  }
}
