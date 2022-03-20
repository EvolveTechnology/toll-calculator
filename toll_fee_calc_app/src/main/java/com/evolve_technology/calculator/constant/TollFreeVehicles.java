package com.evolve_technology.calculator.constant;

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

		    public String getType() {
		      return type;
		    }
}
