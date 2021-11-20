package com.work.model.vehicles;

public class Car implements Vehicle {
  @Override
  public String getType() {
    return "Car";
  }

  @Override
  public boolean isTollFree() {
    return false;
  }
}
