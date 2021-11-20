package com.work.model.vehicles;

public class Military implements Vehicle {
  @Override
  public String getType() {
    return "Military";
  }

  @Override
  public boolean isTollFree() {
    return true;
  }


}
