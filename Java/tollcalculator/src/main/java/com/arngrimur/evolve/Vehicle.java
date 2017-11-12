package com.arngrimur.evolve;

public enum Vehicle {
    MOTORBIKE("Motorbike"),
    TRACTOR("Tractor"),
    EMERGENCY("Emergency"),
    DIPLOMAT("Diplomat"),
    FOREIGN("Foreign"),
    MILITARY("Military"),
	CAR("Car");
    private final String type;

    Vehicle(String type) {
      this.type = type;
    }

    public String getType() {
      return type;
    }
    
    
    
  }