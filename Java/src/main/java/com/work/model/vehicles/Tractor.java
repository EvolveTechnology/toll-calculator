package com.work.model.vehicles;

public class Tractor implements Vehicle {
  @Override
  public String getType() {
    return "Tractor";
  }

  @Override
  public boolean isTollFree() {
    return true;
  }


}
