package com.toll.calculator;

import java.util.stream.Stream;

public enum TollFreeVehicles {
    MOTORBIKE("Motorbike"),
    TRACTOR("Tractor"),
    EMERGENCY("Emergency"),
    DIPLOMAT("Diplomat"),
    FOREIGN("Foreign"),
    MILITARY("Military");
    private final String type;

    TollFreeVehicles(String type) {
      this.type = type;
    }

    public static boolean contains(final String type) {
    	return Stream.of(TollFreeVehicles.values()).anyMatch(vehicleType -> vehicleType.type.equalsIgnoreCase(type));
    }
    
}