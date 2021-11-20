package com.work.model.vehicles;

public class Foreign implements Vehicle {
  @Override
  public String getType() {
    return "Foreign";
  }

  @Override
  public boolean isTollFree() {
    return true;
  }


}
