package com.work.model.vehicles;

public class Diplomat implements Vehicle {
  @Override
  public String getType() {
    return "Diplomat";
  }

  @Override
  public boolean isTollFree() {
    return true;
  }


}
