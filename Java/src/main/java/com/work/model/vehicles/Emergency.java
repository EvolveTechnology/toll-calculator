package com.work.model.vehicles;

public class Emergency implements Vehicle {
  @Override
  public String getType() {
    return "Emergency";
  }

  @Override
  public boolean isTollFree() {
    return true;
  }


}
